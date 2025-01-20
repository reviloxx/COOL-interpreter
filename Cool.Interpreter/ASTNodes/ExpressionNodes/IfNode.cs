using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IfNode : ExpressionNode
{
    public ExpressionNode? Condition { get; set; }
    public ExpressionNode? ThenBranch { get; set; }
    public ExpressionNode? ElseBranch { get; set; }

    public IfNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        var conditionResult = Condition?.Execute(env);
        if (conditionResult is bool boolResult && boolResult)
        {
            return ThenBranch?.Execute(env);
        }
        else
        {
            return ElseBranch?.Execute(env);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}If Expression");
        IncreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Condition:");
        IncreaseIndent();
        if (Condition != null)
            sb.Append(Condition.ToString());
        DecreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Then Branch:");
        IncreaseIndent();
        if (ThenBranch != null)
            sb.Append(ThenBranch.ToString());
        DecreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Else Branch:");
        IncreaseIndent();
        if (ElseBranch != null)
            sb.Append(ElseBranch.ToString());
        DecreaseIndent();
        
        DecreaseIndent();
        return sb.ToString();
    }
}