using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;


public class BoolNotNode : UnaryNode 
{
    public override string Symbol => "!";

    public BoolNotNode(ParserRuleContext context) : base(context)
    {
    }

    // public override void Accept(IVisitor visitor)
    // {
    //     visitor.Visit(this);
    // }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}NOT");
        
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Operand:");
        IncreaseIndent();
        if (Operand != null)
        {
            sb.AppendLine(Operand.ToString());
        }
        DecreaseIndent();
        DecreaseIndent();
        
        return sb.ToString();
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }
}