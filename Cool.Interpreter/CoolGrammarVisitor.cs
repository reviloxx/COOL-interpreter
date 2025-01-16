using Antlr4.Runtime.Misc;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitor : CoolGrammarBaseVisitor<object?>
{
    private readonly Dictionary<string, object?> _variables = [];
    private bool _invertNextAssignment = false;

    public override object? VisitAssignment(AssignmentContext context)
    {
        var varname = context.ID().GetText();
        var value = Visit(context.expression());

        if (value is BoolNotContext)
        {
            _invertNextAssignment = true;
            return null;
        }

        _variables[varname] = _invertNextAssignment? value : !(bool)value!;
        _invertNextAssignment = false;

        return null;
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
        var left = Visit(context.expression(0))!;
        var right = Visit(context.expression(1))!;

        if (left.GetType() != right.GetType())
            throw new Exception("Only matching types are supported for arithmetic expressions!");

        if (left is int)
        {
            var leftInt = int.Parse(left.ToString()!);
            var rightInt = int.Parse(right.ToString()!);

            return context.op.Text switch
            {
                "+" => leftInt + rightInt,
                "-" => leftInt - rightInt,
                "*" => leftInt * rightInt,
                "/" => rightInt != 0 ? leftInt / rightInt : throw new DivideByZeroException(),
                _ => throw new Exception($"Operator {context.op.Text} not supported for int arithmetics!")
            };
        }

        if (left is string)
        {
            var leftStr = left.ToString();
            var rightStr = right.ToString();

            return context.op.Text switch
            {
                "+" => leftStr + rightStr,
                _ => throw new Exception($"Operator {context.op.Text} not supported for string arithmetics!")
            };
        }

        throw new Exception($"Type {left.GetType().Name} not supported for arithmetic expressions!");
    }

    public override object? VisitComparisson([NotNull] ComparissonContext context)
    {
        var left = Visit(context.expression(0))!;
        var right = Visit(context.expression(1))!;

        if (left.GetType() != right.GetType())
            throw new Exception("Only matching types are supported for comparisson expressions!");

        if (left is int)
        {
            var leftInt = int.Parse(left.ToString()!);
            var rightInt = int.Parse(right.ToString()!);

            return context.op.Text switch
            {
                "<=" => leftInt <= rightInt,
                "<" => leftInt < rightInt,
                "=" => leftInt == rightInt,
                _ => throw new Exception($"Operator {context.op.Text} not supported for int comparisson!")
            };
        }

        if (left is string)
        {
            var leftStr = left.ToString();
            var rightStr = right.ToString();

            return context.op.Text switch
            {
                "=" => leftStr == rightStr,
                _ => throw new Exception($"Operator {context.op.Text} not supported for string comparisson!")
            };
        }

        if (left is bool)
        {
            var leftBool = string.Equals(left.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            var rightBool = string.Equals(right.ToString(), "true", StringComparison.OrdinalIgnoreCase);

            return context.op.Text switch
            {
                "=" => leftBool == rightBool,
                _ => throw new Exception($"Operator {context.op.Text} not supported for boolean comparisson!")
            };
        }

        throw new Exception($"Type {left.GetType().Name} not supported for comparisson expressions!");
    }

    public override object? VisitId([NotNull] IdContext context)
    {
        var varName = context.ID().GetText();

        if (!_variables.TryGetValue(varName, out object? value))
            throw new Exception($"Variable {varName} is not defined");

        return value;
    }

    public override object? VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    {
        var methodName = context.ID().GetText();
        var args = context.expression().Select(Visit).ToList();

        if (methodName == "out_string") 
            Console.WriteLine(args[0]?.ToString());

        return null;
    }
}