using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ClassDefineNode : AstNode
{
        public required IdNode ClassName { get; set; } // ClassName
        public IdNode? BaseClassName { get; set; } // Optional: BaseClass
    
        public List<FeatureNode> FeatureNodes { get; set; } = new();
    
        public ClassDefineNode(ParserRuleContext context) : base(context)
        {
        }
    
        public ClassDefineNode(int line, int column) : base(line, column)
        {
        }
}