using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class AssignmentNode : ExpressionNode
{
    public AssignmentNode(ParserRuleContext context) : base(context)
    {
    }
}