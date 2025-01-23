namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public abstract class FeatureNode(string featureName, ParserRuleContext? context = null) : AstNode(context)
{
    public string FeatureName => _featureIdNode.Name;
    protected readonly IdNode _featureIdNode = new(featureName, context);
}