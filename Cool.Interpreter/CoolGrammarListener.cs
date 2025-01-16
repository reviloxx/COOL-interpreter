namespace Cool.Interpreter;

public class CoolGrammarListener : CoolGrammarBaseListener
{
    private readonly Dictionary<string, string?> classHierarchy = [];
    private readonly Dictionary<string, Dictionary<string, CoolMethod>> classMethods = [];
    private readonly Stack<object?> expressionStack = new();
    private readonly Dictionary<string, object?> variables = [];
    private string currentClass = "";

    public record CoolMethod(string Name, List<string> Parameters, Action<List<object?>> Body);

    public override void EnterProgram(CoolGrammarParser.ProgramContext context)
    {
        variables.Add("self", null);
    }

    public override void EnterClassDefine(CoolGrammarParser.ClassDefineContext context)
    {
        var className = context.TYPE(0).GetText();
        string? parentClass = context.TYPE(1)?.GetText();
        classHierarchy[className] = parentClass;

        if (!classMethods.ContainsKey(className))
            classMethods[className] = [];

        currentClass = className;
    }

    public override void EnterMethod(CoolGrammarParser.MethodContext context)
    {
        var methodName = context.ID().GetText();
        var parameters = context.formal().Select(f => f.ID().GetText()).ToList();
        var methodExpression = context.expression();

        classMethods[currentClass][methodName] = new CoolMethod(methodName, parameters, args =>
        {
            // Bind method arguments to local variables.
            for (int i = 0; i < parameters.Count; i++)
            {
                variables[parameters[i]] = args[i];
            }

            // Execute the method body.
            ExecuteExpression(methodExpression);

            // Clear local variables after execution.
            foreach (var param in parameters)
            {
                variables.Remove(param);
            }
        });
    }

    public override void EnterDispatchImplicit(CoolGrammarParser.DispatchImplicitContext context)
    {
        var methodName = context.ID().GetText();
        var arguments = context.expression().Select(EvaluateExpression).ToList();

        var method = ResolveMethod(currentClass, methodName);
        if (method.Parameters.Count != arguments.Count)
        {
            throw new Exception($"Method {methodName} expects {method.Parameters.Count} arguments but got {arguments.Count}.");
        }

        method.Body(arguments);
    }

    public override void EnterAssignment(CoolGrammarParser.AssignmentContext context)
    {
        var variableName = context.ID().GetText();
        var value = EvaluateExpression(context.expression());
        variables[variableName] = value;
        expressionStack.Push(value);
    }

    public override void EnterId(CoolGrammarParser.IdContext context)
    {
        var variableName = context.ID().GetText();

        if (!variables.TryGetValue(variableName, out object? value))
            throw new Exception($"Undefined variable: {variableName}");

        expressionStack.Push(value);
    }

    public override void EnterString(CoolGrammarParser.StringContext context)
    {
        expressionStack.Push(context.GetText().Trim('"'));
    }

    public override void EnterInt(CoolGrammarParser.IntContext context)
    {
        expressionStack.Push(int.Parse(context.GetText()));
    }

    public override void EnterBlock(CoolGrammarParser.BlockContext context)
    {
        foreach (var expr in context.expression())
        {
            ExecuteExpression(expr);
        }
    }

    private CoolMethod ResolveMethod(string className, string methodName)
    {
        string? currentClass = className;

        while (currentClass != null)
        {
            if (classMethods.TryGetValue(currentClass, out var methods) && methods.ContainsKey(methodName))
            {
                return methods[methodName];
            }
            classHierarchy.TryGetValue(currentClass, out currentClass);
        }

        throw new Exception($"Undefined method: {methodName}");
    }

    private void ExecuteExpression(CoolGrammarParser.ExpressionContext context)
    {
        if (context is CoolGrammarParser.DispatchImplicitContext implicitDispatch)
        {
            ExitDispatchImplicit(implicitDispatch);
        }
        else if (context is CoolGrammarParser.DispatchExplicitContext explicitDispatch)
        {
            ExitDispatchExplicit(explicitDispatch);
        }
        else if (context is CoolGrammarParser.IntContext intExpr)
        {
            expressionStack.Push(int.Parse(intExpr.INT().GetText()));
        }
        else if (context is CoolGrammarParser.StringContext stringExpr)
        {
            expressionStack.Push(stringExpr.STRING().GetText().Trim('"'));
        }
        else if (context is CoolGrammarParser.BooleanContext boolExpr)
        {
            expressionStack.Push(boolExpr.TRUE() != null);
        }
        else if (context is CoolGrammarParser.AssignmentContext assignExpr)
        {
            ExecuteExpression(assignExpr.expression());
            variables.Add(assignExpr.ID().GetText(), expressionStack.Pop());
        }
        else if (context is CoolGrammarParser.IdContext idExpr)
        {
            string varName = idExpr.ID().GetText();
            if (!variables.TryGetValue(varName, out object? value))
                throw new Exception($"Undefined variable: {varName}");

            expressionStack.Push(value);
        }
        else
        {
            throw new NotImplementedException($"Expression type not implemented: {context.GetType().Name}");
        }
    }

    private object? EvaluateExpression(CoolGrammarParser.ExpressionContext context)
    {
        ExecuteExpression(context);
        return expressionStack.Pop();
    }
}