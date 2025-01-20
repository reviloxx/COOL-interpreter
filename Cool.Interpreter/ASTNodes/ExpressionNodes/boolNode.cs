using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BoolNode : ExpressionNode
{
    public bool Value { get; set; }

    public BoolNode(ParserRuleContext context) : base(context)
    {
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Boolean: {Value}");
        return sb.ToString();
    }
}