namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class EqualNode(ExpressionNode leftOperand, ExpressionNode rightOperand, ParserRuleContext? context = null) : BinaryOperationNode(leftOperand, rightOperand, context)
{
    public override string Symbol => "=";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute the operands and check for null values
        object? leftValue = _leftOperand?.Execute(env);
        object? rightValue = _rightOperand?.Execute(env);

        if (leftValue == null || rightValue == null)
            throw new Exception("Operands must not be null.");

        if (leftValue is int leftInt && rightValue is int rightInt)
            return leftInt == rightInt;

        if (leftValue is string leftStr && rightValue is string rightStr)
            return leftStr == rightStr;

        if (leftValue is bool leftBool && rightValue is bool rightBool)
            return leftBool == rightBool;

        throw new InvalidCastException($"Invalid operand types: {leftValue.GetType()} and {rightValue.GetType()}");
    }
}