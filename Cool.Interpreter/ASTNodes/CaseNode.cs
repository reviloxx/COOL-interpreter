namespace Cool.Interpreter.ASTNodes;

public class CaseNode(ExpressionNode caseEpression, List<Tuple<ParameterNode, ExpressionNode>> caseBranches, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private ExpressionNode _caseExpression = caseEpression;
    private List<Tuple<ParameterNode, ExpressionNode>> _caseBranches = caseBranches;

    public override object? Execute(RuntimeEnvironment env)
    {           
        var value = _caseExpression.Execute(env) ?? throw new InvalidOperationException("Runtime error: case expression is void.");
        var valueType = value.GetType();

        foreach (var branch in _caseBranches)
        {
            if (valueType == typeof(int) && branch.Item1.TypeName == "Int")
            {
                env.DefineVariable(branch.Item1.ParameterName, value);
                return branch.Item2.Execute(env);
            }

            if (valueType == typeof(string) && branch.Item1.TypeName == "String")
            {
                env.DefineVariable(branch.Item1.ParameterName, value);
                return branch.Item2.Execute(env);
            }
        }

        throw new InvalidOperationException("No matching case branch found.");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"case {_caseExpression} of");
        foreach (var expression in _caseBranches)
        {
            sb.AppendLine($"  {expression};");
        }
        sb.Append("esac");
        return sb.ToString();
    }
}