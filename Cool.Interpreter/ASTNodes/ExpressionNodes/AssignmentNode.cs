using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class AssignmentNode : ExpressionNode
{
    public required IdNode Target { get; set; } // Zielvariable
    public ExpressionNode? Value { get; set; } // Zuweisungswert // kann auch null sein?

    public AssignmentNode(ParserRuleContext context) : base(context)
    {
    }
}