using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ParameterNode : AstNode
{
    public required IdNode ParameterName { get; set; }
    public required TypeNode ParameterType { get; set; }

    public ParameterNode(ParserRuleContext context) : base(context) { }
}