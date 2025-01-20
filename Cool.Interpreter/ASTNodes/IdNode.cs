using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IdNode : ExpressionNode 
{
    public String Name { get; set; }

    public IdNode(string name, ParserRuleContext context) : base(context)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"{GetIndentation()}Identifier: {Name}";
    }
        
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    
}