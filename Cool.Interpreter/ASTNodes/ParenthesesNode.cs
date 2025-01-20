using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ParenthesesNode : ExpressionNode
{
    public ExpressionNode? Expression { get; set; }

    public ParenthesesNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        return Expression?.Execute(env);
    }

    public override string ToString()
    {
        return $"{GetIndentation()}({Expression})";
    }
}