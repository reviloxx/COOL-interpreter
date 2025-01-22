namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class EqualNode(ParserRuleContext? context, ExpressionNode leftOperand, ExpressionNode rightOperand)
    : BinaryOperationNode(context, leftOperand, rightOperand)
{
    public override string Symbol => "=";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute the operands and check for null values
        object? LeftValue = _leftOperand?.Execute(env);
        object? RightValue = _rightOperand?.Execute(env);

        if (LeftValue == null || RightValue == null)
        {
            throw new ArgumentNullException("Operands must not be null.");
        }
        else if (LeftValue is int leftInt && RightValue is int rightInt)
        {
            return leftInt == rightInt;
        }
        else if (LeftValue is string leftStr && RightValue is string rightStr)
        {
            return leftStr == rightStr;
        }
        else if (LeftValue is bool leftBool && RightValue is bool rightBool)
        {
            return leftBool == rightBool;
        }

        throw new InvalidCastException($"Invalid operand types: {LeftValue.GetType()} and {RightValue.GetType()}");
    }
}