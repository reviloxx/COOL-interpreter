using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BoolNode(ParserRuleContext context) : ExpressionNode(context)
{
    public bool Value { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Boolean: {Value}");
        return sb.ToString();
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        return Value;
    }
}