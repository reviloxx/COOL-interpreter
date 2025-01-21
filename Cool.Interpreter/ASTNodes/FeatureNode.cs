using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class FeatureNode : AstNode
{
    public required IdNode FeatureName { get; set; }
    public FeatureNode(ParserRuleContext context) : base(context)
    {
    }

    public FeatureNode(int line, int column) : base(line, column)
    {
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}