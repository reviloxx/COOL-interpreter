using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class FeatureNode : AstNode
{
    public FeatureNode(ParserRuleContext context) : base(context)
    {
    }
}