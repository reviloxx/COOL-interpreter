namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class AddNode(ExpressionNode leftOperand, ExpressionNode rightOperand, ParserRuleContext? context = null) : BinaryOperationNode(leftOperand, rightOperand, context)
{
    public override string Symbol => "+";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = _leftOperand?.Execute(env);
        object? rightValue = _rightOperand?.Execute(env);

        // Check if both operands are integers
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            return leftInt + rightInt;
        }
        // In COOL, strings can also be concatenated with +
        else if (leftValue is string leftStr && rightValue is string rightStr)
        {
            return leftStr + rightStr;
        }

        throw new Exception($"Type error: '+' operation expects either two Ints or two Strings but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {_line}, column {_column}");
    }
}