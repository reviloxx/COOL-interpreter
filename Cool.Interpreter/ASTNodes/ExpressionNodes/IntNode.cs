using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IntNode : ExpressionNode
{
    public int Value { get; set; }

    public IntNode(ParserRuleContext context) : base(context)
    {
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Integer: {Value}");
        return sb.ToString();
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}