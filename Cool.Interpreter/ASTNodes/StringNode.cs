using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class StringNode : ExpressionNode
{
    public string Value { get; set; }

    public StringNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        return Value;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}String: \"{Value}\""); // Added quotes to make string boundaries clear
        return sb.ToString();
    }
}