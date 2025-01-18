using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ProgramNode : AstNode
{
    public List<ClassDefineNode?> ClassDefineNodes = new();

    public ProgramNode(ParserRuleContext context) : base(context)
    {
    }
    
    // public void Accept(ICoolGrammarVisitor<ASTNodes> visitor)
    // {
        // visitor.Visit(this);
    // }
    
    // public override string ToString()
    // {
    //     StringBuilder sb = new StringBuilder();
    //
    //     int saveIdent = ident;
    //     string identString = emptyString.Substring(emptyString.Length - ident);
    //
    //     sb.Append(identString);
    //     sb.Append("Function definition: function name ");
    //     sb.Append(Id.ToString());
    //     sb.Append(" returns type ");
    //     sb.Append(TypeReturn.ToString());
    //     sb.Append("\n");
    //
    //     sb.Append(identString);
    //     sb.Append("  (");
    //     string sep = "";
    //     foreach (var arg in Arguments)
    //     {
    //         sb.Append(sep);
    //         sb.Append(arg.Id.ToString());
    //         sb.Append(":");
    //         sb.Append(arg.Type.ToString());
    //         sep = ", ";
    //     }
    //     sb.Append(")\n");
    //     ident += 2;
    //
    //     sb.Append(Body.ToString());
    //
    //     sb.Append("\n");
    //
    //     ident = saveIdent;
    //
    //     return sb.ToString();
    // }
}