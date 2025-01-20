using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BlockSequenceNode : ExpressionNode
{
    public List<ExpressionNode?> Expressions { get; set; } = new();

    public BlockSequenceNode(ParserRuleContext context) : base(context)
    {
    }
    

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Block {{");
        
        IncreaseIndent();
        foreach (var expr in Expressions)
        {
            if (expr != null)
            {
                sb.Append(expr.ToString());
                sb.AppendLine(";");
            }
        }
        DecreaseIndent();
        
        sb.Append($"{GetIndentation()}}}");
        return sb.ToString();
    }
}