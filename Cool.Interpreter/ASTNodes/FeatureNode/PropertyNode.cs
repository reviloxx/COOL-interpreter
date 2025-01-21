using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class PropertyNode : FeatureNode
{
    public TypeNode Type { get; set; }
    public ExpressionNode? InitialValue { get; set; } // Optionaler Initialwert

    public PropertyNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Property {FeatureName} : {Type}");
        
        if (InitialValue != null)
        {
            sb.AppendLine(" =");
            IncreaseIndent();
            sb.Append(InitialValue.ToString());
            DecreaseIndent();
        }
        
        return sb.ToString();
    }
}
