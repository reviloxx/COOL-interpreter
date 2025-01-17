using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class MethodNode : FeatureNode
{
    public MethodNode(ParserRuleContext context) : base(context)
    {
    }
}