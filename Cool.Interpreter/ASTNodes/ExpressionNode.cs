using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public abstract class ExpressionNode : AstNode
{
    List<ExpressionNode> Children { get; set; } = new();

    public ExpressionNode(ParserRuleContext context) : base(context)
    {
    }
    // public override void Execute(RuntimeEnvironment env)
    // {
    //     throw new NotImplementedException();
    // }
    
}