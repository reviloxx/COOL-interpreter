using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DispatchNode : ExpressionNode
{
    public ExpressionNode? Object { get; set; }  // Will be null for implicit dispatch
    public TypeNode? StaticType { get; set; }    // For @TYPE syntax
    public IdNode MethodName { get; set; }
    public List<ExpressionNode> Arguments { get; set; } = new();

    public DispatchNode(ParserRuleContext context) : base(context)
    {
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Dispatch");
        
        IncreaseIndent();
        
        if (Object != null)
        {
            sb.AppendLine($"{GetIndentation()}Object:");
            IncreaseIndent();
            sb.AppendLine(Object.ToString());
            DecreaseIndent();
        }
        
        if (StaticType != null)
        {
            sb.AppendLine($"{GetIndentation()}Static Type: {StaticType}");
        }
        
        sb.AppendLine($"{GetIndentation()}Method: {MethodName}");
        
        if (Arguments.Any())
        {
            sb.AppendLine($"{GetIndentation()}Arguments:");
            IncreaseIndent();
            foreach (var arg in Arguments)
            {
                sb.AppendLine(arg.ToString());
            }
            DecreaseIndent();
        }
        
        DecreaseIndent();
        return sb.ToString();
    }
}