using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DivNode : BinaryOperationNode
{
    public override string Symbol => "+";

    public DivNode(ParserRuleContext context) : base(context)
    {
    }

    
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    
    // public override void Accept(IVisitor visitor)
    // {
    //     visitor.Visit(this);
    // }

    // public override string ToString()
    // {
    //     StringBuilder sb = new StringBuilder();
    //
    //     int saveIdent = ident;
    //     string identString = emptyString.Substring(emptyString.Length - ident);
    //
    //     sb.Append(identString);
    //     sb.Append("Operator Div: (");
    //     sb.Append(Symbol);
    //     sb.Append(")");
    //     sb.Append("\n");
    //
    //     ident += 2;
    //     sb.Append(LeftOperand.ToString());
    //     sb.Append("\n");
    //     sb.Append(RightOperand.ToString());
    //     sb.Append("\n");
    //
    //     ident = saveIdent;
    //
    //     return sb.ToString();
    // }

}