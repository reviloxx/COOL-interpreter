using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class MethodNode : FeatureNode
{
    public List<ParameterNode> Parameters { get; set; } = new();
    public TypeNode ReturnType { get; set; }
    public ExpressionNode Body { get; set; }

    public MethodNode(ParserRuleContext context) : base(context)
    {
    }

}
