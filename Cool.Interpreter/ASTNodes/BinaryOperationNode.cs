using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public abstract class BinaryOperationNode : ExpressionNode
{
    public ExpressionNode LeftOperand { get; set; }
    public ExpressionNode RightOperand { get; set; }

    public BinaryOperationNode(ParserRuleContext context) : base(context)
    {
    }

    public abstract string Symbol { get; }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}{Symbol}");
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Left: {LeftOperand}");
        sb.AppendLine($"{GetIndentation()}Right: {RightOperand}");
        DecreaseIndent();
        return sb.ToString();
    }
}