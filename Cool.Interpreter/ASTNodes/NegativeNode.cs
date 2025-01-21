using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class NegativeNode : ExpressionNode
{
    public ExpressionNode? Expression { get; set; }

    public NegativeNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        var result = Expression?.Execute(env);
        if (result is int intValue)
        {
            return -intValue;
        }
        throw new Exception($"Cannot negate non-integer value at line {Line}, column {Column}");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Negative Expression");
        if (Expression != null)
        {
            IncreaseIndent();
            sb.Append(Expression.ToString());
            DecreaseIndent();
        }
        return sb.ToString();
    }
}