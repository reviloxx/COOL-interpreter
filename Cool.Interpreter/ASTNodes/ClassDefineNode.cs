using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ClassDefineNode : AstNode
{
    
    
    public ClassDefineNode(ParserRuleContext context) : base(context) { }

    public ClassDefineNode(int line, int column) : base(line, column) { }
}
