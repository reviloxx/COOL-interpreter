namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class EqualNode(ParserRuleContext? context, ExpressionNode leftOperand, ExpressionNode rightOperand) : BinaryOperationNode(context, leftOperand, rightOperand)
{
    public override string Symbol => "=";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute the operand
        object? LeftValue = _leftOperand?.Execute(env);
        object? RightValue = _rightOperand?.Execute(env);

        // TODO check for same type needed?

        // Check if operand is a boolean
        return LeftValue == RightValue;
    }
}