using System.ComponentModel.Design;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Cool.Interpreter.ASTNodes;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitorAstBuilder : CoolGrammarBaseVisitor<object?>
{
    public override object? VisitProgram([NotNull] ProgramContext context)
    {
        ProgramNode programNode = new ProgramNode(context);

        programNode.ClassDefineNodes =
            context.classDefine().Select(x => VisitClassDefine(x) as ClassDefineNode).ToList();

        return programNode;
    }

    public override object? VisitClassDefine([NotNull] ClassDefineContext context)
    {
        var className = new IdNode(context.TYPE(0).GetText(), context);
        ClassDefineNode classDefineNode = new ClassDefineNode(context)
        {
            ClassName = className
        };

        if (context.TYPE(1) != null)
        {
            classDefineNode.BaseClassName = new IdNode(context.TYPE(1).GetText(), context);
        }

        // Features (Methoden, Properties) verarbeiten
        classDefineNode.FeatureNodes = context.feature()
            .Select(VisitFeature)
            .OfType<FeatureNode>() // Nur gültige FeatureNodes behalten
            .ToList();

        return classDefineNode;
    }


    public override object? VisitFeature([NotNull] FeatureContext context)
    {
        if (context.method() != null)
        {
            return VisitMethod(context.method());
        }
        else if (context.property() != null)
        {
            return VisitProperty(context.property());
        }

        return null; // Unbekannter Feature-Typ
    }

    public override object? VisitMethod([NotNull] MethodContext context)
    {
        var methodNode = new MethodNode(context)
        {
            FeatureName = new IdNode(context.ID().GetText(), context), // Methodenname
            ReturnType = new TypeNode(context, context.TYPE().GetText()) // Rückgabetyp
        };

        methodNode.Parameters = context.formal()
            .Select(param => VisitFormal(param) as ParameterNode)
            .Where(param => param != null)
            .ToList()!;

        
        //TODO - ALLE Kontexte abarbeiten - Erstes Problem: ich komme hier mit einem methodcontext rein, das sollte nicht möglich sein?
        methodNode.Body = context.expression() switch
        {
            ArithmeticContext arithmetic => VisitArithmetic(arithmetic) as ExpressionNode,
            AssignmentContext assignment => VisitAssignment(assignment) as ExpressionNode,
            BlockContext block => VisitBlock(block) as ExpressionNode,
            BoolNotContext boolNot => VisitBoolNot(boolNot) as ExpressionNode,
            BooleanContext boolean => VisitBoolean(boolean) as ExpressionNode,
            CaseContext caseContext => VisitCase(caseContext) as ExpressionNode,
            // ComparisonContext comparison => VisitComparison(comparison) as ExpressionNode,
            DispatchExplicitContext dispatchExplicit => VisitDispatchExplicit(dispatchExplicit) as ExpressionNode,
            DispatchImplicitContext dispatchImplicit => VisitDispatchImplicit(dispatchImplicit) as ExpressionNode,
            IdContext id => VisitId(id) as ExpressionNode,
            IfContext @if => VisitIf(@if) as ExpressionNode,
            IntContext @int => VisitInt(@int) as ExpressionNode,
            IsvoidContext isvoid => VisitIsvoid(isvoid) as ExpressionNode,
            LetInContext letIn => VisitLetIn(letIn) as ExpressionNode,
            NegativeContext negative => VisitNegative(negative) as ExpressionNode,
            StringContext str => VisitString(str) as ExpressionNode,
            WhileContext @while => VisitWhile(@while) as ExpressionNode,
            _ => throw new NotSupportedException("Unknown expression type.")
        };

        return methodNode;
    }


    public override object? VisitProperty([NotNull] PropertyContext context)
    {
        var formal = context.formal();

        return new PropertyNode(context)
        {
            FeatureName = new IdNode(formal.ID().GetText(), context), // Name der Eigenschaft
            InitialValue = context.expression() != null
                ? Visit(context.expression()) as ExpressionNode // Optionaler Initialwert
                : null
        };
    }

    public override object? VisitAssignment([NotNull] AssignmentContext context)
    {
        return new AssignmentNode(context)
        {
            Target = new IdNode(context.ID().GetText(), context), // Zielvariable
            Value = Visit(context.expression()) as ExpressionNode // Zuweisungswert
        };
    }

    public override object? VisitBlock([NotNull] BlockContext context)
    {
        return new BlockSequenceNode(context)
        {
            Expressions = context.expression().Select(x => Visit(x) as ExpressionNode).ToList()
        };
    }

    public override object? VisitFormal([NotNull] FormalContext context)
    {
        return new ParameterNode(context)
        {
            ParameterName = new IdNode(context.ID().GetText(), context),
            ParameterType = new TypeNode(context, context.TYPE().GetText())
        };
    }

    public override object? VisitBoolean([NotNull] BooleanContext context)
        => context.TRUE() != null;

    public override object? VisitBoolNot([NotNull] BoolNotContext context)
        => context;

    public override object? VisitInt([NotNull] IntContext context)
        => int.Parse(context.INT().GetText());

    public override object? VisitString([NotNull] StringContext context)
        => context.STRING().GetText();

    public override object? VisitArithmetic([NotNull] ArithmeticContext context)
    {
        throw new NotImplementedException();
        // var left = Visit(context.expression(0))!;
        // var right = Visit(context.expression(1))!;
        //
        // if (left.GetType() != right.GetType())
        //     throw new Exception("Only matching types are supported for arithmetic expressions!");
        //
        // if (left is int)
        // {
        //     var leftInt = int.Parse(left.ToString()!);
        //     var rightInt = int.Parse(right.ToString()!);
        //
        //     return context.op.Text switch
        //     {
        //         "+" => leftInt + rightInt,
        //         "-" => leftInt - rightInt,
        //         "*" => leftInt * rightInt,
        //         "/" => rightInt != 0 ? leftInt / rightInt : throw new DivideByZeroException(),
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for int arithmetics!")
        //     };
        // }
        //
        // if (left is string)
        // {
        //     var leftStr = left.ToString();
        //     var rightStr = right.ToString();
        //
        //     return context.op.Text switch
        //     {
        //         "+" => leftStr + rightStr,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for string arithmetics!")
        //     };
        // }
        //
        // throw new Exception($"Type {left.GetType().Name} not supported for arithmetic expressions!");
    }

    public override object? VisitComparisson([NotNull] ComparissonContext context)
    {
        throw new NotImplementedException();

        // var left = Visit(context.expression(0))!;
        // var right = Visit(context.expression(1))!;
        //
        // if (left.GetType() != right.GetType())
        //     throw new Exception("Only matching types are supported for comparisson expressions!");
        //
        // if (left is int)
        // {
        //     var leftInt = int.Parse(left.ToString()!);
        //     var rightInt = int.Parse(right.ToString()!);
        //
        //     return context.op.Text switch
        //     {
        //         "<=" => leftInt <= rightInt,
        //         "<" => leftInt < rightInt,
        //         "=" => leftInt == rightInt,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for int comparisson!")
        //     };
        // }
        //
        // if (left is string)
        // {
        //     var leftStr = left.ToString();
        //     var rightStr = right.ToString();
        //
        //     return context.op.Text switch
        //     {
        //         "=" => leftStr == rightStr,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for string comparisson!")
        //     };
        // }
        //
        // if (left is bool)
        // {
        //     var leftBool = string.Equals(left.ToString(), "true", StringComparison.OrdinalIgnoreCase);
        //     var rightBool = string.Equals(right.ToString(), "true", StringComparison.OrdinalIgnoreCase);
        //
        //     return context.op.Text switch
        //     {
        //         "=" => leftBool == rightBool,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for boolean comparisson!")
        //     };
        // }
        //
        // throw new Exception($"Type {left.GetType().Name} not supported for comparisson expressions!");
    }

    public override object? VisitId([NotNull] IdContext context)
    {
        throw new NotImplementedException();

        // var varName = context.ID().GetText();
        //
        // if (!_variables.TryGetValue(varName, out object? value))
        //     throw new Exception($"Variable {varName} is not defined");
        //
        // return value;
    }

    public override object? VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    {
        Console.WriteLine("in VisitDispatchImplicit");
        // throw new NotImplementedException();

        // var methodName = context.ID().GetText();
        // var args = context.expression().Select(Visit).ToList();
        //
        // if (methodName == "out_string")
        // {
        //     Console.WriteLine(args[0]?.ToString());
        //     return null;
        // }
        //
        // foreach (var expression in context.expression())
        // {
        //     // TODO: how to call other methods?
        //     Visit(expression);
        // }        
        //
        return null;
    }

    public override object? VisitNew([NotNull] NewContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitParentheses([NotNull] ParenthesesContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitLetIn([NotNull] LetInContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitIsvoid([NotNull] IsvoidContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitWhile([NotNull] WhileContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitNegative([NotNull] NegativeContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitIf([NotNull] IfContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitCase([NotNull] CaseContext context)
    {
        throw new NotImplementedException();
    }

    public override object? VisitDispatchExplicit([NotNull] DispatchExplicitContext context)
    {
        throw new NotImplementedException();
    }
}