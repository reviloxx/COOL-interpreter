namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class FeatureNode(ParserRuleContext? context) : AstNode(context)
{
    public required IdNode FeatureName { get; set; }

    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}