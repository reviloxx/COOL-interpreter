using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class LetInNode : ExpressionNode
{
    public LetInNode(ParserRuleContext context) : base(context)
    {
    }
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}