using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class PropertyNode : FeatureNode
{
    public PropertyNode(ParserRuleContext context) : base(context)
    {
    }
}