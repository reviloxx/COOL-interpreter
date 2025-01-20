using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class AddNode : BinaryOperationNode
{
    public override string Symbol => "+";

    public AddNode(ParserRuleContext context) : base(context)
    {
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
    //     sb.Append("Operator Add: (");
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
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}