using Antlr4.Runtime.Misc;

namespace Cool;

public class CoolGrammarListener : CoolGrammarBaseListener
{
    private readonly Dictionary<string, object?> _symbolTable = [];
    private readonly Dictionary<string, CoolClass> _classDefinitions = [];

    private object? _currentValue;

    public override void EnterProgram([NotNull] CoolGrammarParser.ProgramContext context)
    {
        Console.WriteLine("Executing COOL Program...");
    }

    public override void EnterClassDefine([NotNull] CoolGrammarParser.ClassDefineContext context)
    {
        var name = context.TYPE(0).GetText();
        var parentName = context.TYPE(1)?.GetText();

        var coolClass = new CoolClass(name, parentName);
        _classDefinitions[name] = coolClass;

        Console.WriteLine($"Defined Class: {name}");
    }

    public override void EnterMethod([NotNull] CoolGrammarParser.MethodContext context)
    {
        var methodName = context.ID().GetText();

        _currentValue = null;

        Console.WriteLine($"Executing Method: {methodName}");
        ExecuteExpression(context.expression());
    }

    public override void EnterProperty([NotNull] CoolGrammarParser.PropertyContext context)
    {
        var propertyName = context.formal().ID().GetText();
        var propertyType = context.formal().TYPE().GetText();

        object? value = null;

        if (context.expression() != null)
        {
            ExecuteExpression(context.expression());
            value = _currentValue;
        }

        _symbolTable[propertyName] = value;
        Console.WriteLine($"Property {propertyName} of type {propertyType} initialized with: {value}");
    }

    public override void EnterAssignment([NotNull] CoolGrammarParser.AssignmentContext context)
    {
        var variableName = context.ID().GetText();
        ExecuteExpression(context.expression());
        _symbolTable[variableName] = _currentValue;

        Console.WriteLine($"Assigned {variableName} = {_currentValue}");
    }

    public override void EnterArithmetic([NotNull] CoolGrammarParser.ArithmeticContext context)
    {
        ExecuteExpression(context.expression(0));
        var leftValue = Convert.ToInt32(_currentValue);

        ExecuteExpression(context.expression(1));
        var rightValue = Convert.ToInt32(_currentValue);

        _currentValue = context.op.Text switch
        {
            "+" => leftValue + rightValue,
            "-" => leftValue - rightValue,
            "*" => leftValue * rightValue,
            "/" => rightValue != 0 ? leftValue / rightValue : throw new DivideByZeroException(),
            _ => throw new NotSupportedException($"Operator {context.op.Text} is not supported.")
        };

        Console.WriteLine($"Arithmetic Result: {_currentValue}");
    }

    public override void EnterIf([NotNull] CoolGrammarParser.IfContext context)
    {
        ExecuteExpression(context.expression(0));
        var condition = Convert.ToBoolean(_currentValue);

        if (condition)
            ExecuteExpression(context.expression(1));
        else
            ExecuteExpression(context.expression(2));

        Console.WriteLine($"If Result: {_currentValue}");
    }

    public override void EnterWhile([NotNull] CoolGrammarParser.WhileContext context)
    {
        while (true)
        {
            ExecuteExpression(context.expression(0));
            var condition = Convert.ToBoolean(_currentValue);

            if (!condition) break;

            ExecuteExpression(context.expression(1));
        }

        Console.WriteLine("While Loop Completed");
    }

    private void ExecuteExpression(CoolGrammarParser.ExpressionContext context)
    {
        if (context == null) return;

        if (context is CoolGrammarParser.ArithmeticContext arithmeticContext)
        {
            EnterArithmetic(arithmeticContext);
        }
        else if (context is CoolGrammarParser.AssignmentContext assignmentContext)
        {
            EnterAssignment(assignmentContext);
        }
        else if (context is CoolGrammarParser.BooleanContext booleanContext)
        {
            _currentValue = booleanContext.value.Type == CoolGrammarParser.TRUE ? true : false;
        }
        else if (context is CoolGrammarParser.IntContext intContext)
        {
            _currentValue = int.Parse(intContext.GetText());
        }
        else if (context is CoolGrammarParser.StringContext stringContext)
        {
            _currentValue = stringContext.GetText().Trim('"');
        }
        else if (context is CoolGrammarParser.IdContext idContext)
        {
            var variableName = idContext.GetText();

            if (_symbolTable.ContainsKey(variableName))
                _currentValue = _symbolTable[variableName];
            else
                throw new Exception($"Variable {variableName} is not defined.");
        }
        else if (context is CoolGrammarParser.IfContext ifContext)
        {
            EnterIf(ifContext);
        }
        else if (context is CoolGrammarParser.WhileContext whileContext)
        {
            EnterWhile(whileContext);
        }
        else
        {
            throw new Exception($"Unsupported expression type: {context.GetType().Name}");
        }
    }

    public override void ExitProgram([NotNull] CoolGrammarParser.ProgramContext context)
    {
        Console.WriteLine("Execution Completed.");
    }
}
