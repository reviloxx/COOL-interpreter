using System.Text;
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
        
        public override string ToString()
        {
                var sb = new StringBuilder();
                sb.Append($"{GetIndentation()}Class {ClassName}");
        
                if (BaseClassName != null)
                {
                        sb.Append($" inherits {BaseClassName}");
                }
                sb.AppendLine();
        
                IncreaseIndent();
                foreach (var feature in FeatureNodes)
                {
                        sb.AppendLine(feature.ToString());
                }
                DecreaseIndent();
        
                return sb.ToString();
        }
        
        public override void Execute()
        {
                throw new NotImplementedException();
        }
}