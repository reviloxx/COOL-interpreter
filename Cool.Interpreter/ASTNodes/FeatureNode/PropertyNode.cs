using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class PropertyNode : FeatureNode
{
    public ExpressionNode? InitialValue { get; set; } // Optionaler Initialwert

    public PropertyNode(ParserRuleContext context) : base(context)
    {
    }
}
