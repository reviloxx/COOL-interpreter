using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class FeatureNode : AstNode
{
    public IdNode Id { get; set; }

    public FeatureNode(ParserRuleContext context) : base(context)
    {
    }
}