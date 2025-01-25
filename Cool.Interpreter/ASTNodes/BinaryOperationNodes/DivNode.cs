namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class DivNode(ExpressionNode leftOperand, ExpressionNode rightOperand, ParserRuleContext? context = null) : BinaryOperationNode(leftOperand, rightOperand, context)
{
    public override string Symbol => "/";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = _leftOperand?.Execute(env);
        object? rightValue = _rightOperand?.Execute(env);

        // Check if both operands are integers
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            if (rightInt == 0)
            {
                throw new DivideByZeroException($"Division by 0 at line {_line}, column {_column}");
            }

            return leftInt / rightInt;
        }

        throw new Exception($"Type error: '/' operation expects two Ints but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {_line}, column {_column}");
    }
}