using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BlockSequenceNode : ExpressionNode
{
    public List<ExpressionNode?> Expressions { get; set; } = new();

    public BlockSequenceNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        env.PushScope(); // Create new scope for block
        try
        {
            object? lastResult = null;
            // Execute each expression in sequence
            foreach (var expr in Expressions)
            {
                if (expr != null)
                {
                    lastResult = expr.Execute(env);
                }
            }
            // The value of a block is the value of its last expression
            return lastResult;
        }
        finally
        {
            env.PopScope();
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Block {{");
        
        IncreaseIndent();
        foreach (var expr in Expressions)
        {
            if (expr != null)
            {
                sb.Append(expr.ToString());
                sb.AppendLine(";");
            }
        }
        DecreaseIndent();
        
        sb.Append($"{GetIndentation()}}}");
        return sb.ToString();
    }
}