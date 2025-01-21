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
        // TODO scope here?
        if (Body is BlockSequenceNode blockNode)
        {
            blockNode.CreateNewScope = false;
        }
        
        object? result = null;
        while (true)
        {
            var conditionResult = Condition?.Execute(env);
        
            // Handle numeric comparisons
            if (conditionResult is bool boolResult)
            {
                if (!boolResult) break;
            }
            else if (conditionResult is int intResult)
            {
                if (intResult == 0) break;  // In COOL, 0 is false
            }
            else
            {
                // Handle other types or throw appropriate exception
                throw new Exception($"Invalid condition result type: {conditionResult?.GetType()}");
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