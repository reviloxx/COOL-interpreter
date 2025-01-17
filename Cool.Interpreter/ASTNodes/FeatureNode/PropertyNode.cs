using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class PropertyNode : FeatureNode
{
    // public TypeNode Type { get; set; }
    public ExpressionNode? InitialValue { get; set; } // Optional

    public PropertyNode(ParserRuleContext context) : base(context)
    {
    }
}
