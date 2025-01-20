using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class WhileNode : ExpressionNode
{
    public ExpressionNode? Condition { get; set; }
    public ExpressionNode? Body { get; set; }

    public WhileNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        object? result = null;
        while (true)
        {
            var conditionResult = Condition?.Execute(env);
            if (conditionResult is bool boolResult && !boolResult)
            {
                break;
            }
            result = Body?.Execute(env);
        }
        return result;
    }

    public override string ToString()
    {
        return $"{GetIndentation()}while {Condition} loop\n{Body}\n{GetIndentation()}pool";
    }
}