using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IdNode : AstNode
{
    String Name { get; set; }

    public IdNode(string name, ParserRuleContext context) : base(context)
    {
        Name = name;
    }
}