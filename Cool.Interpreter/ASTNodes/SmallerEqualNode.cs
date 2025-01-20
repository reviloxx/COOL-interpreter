using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class SmallerEqualNode : BinaryOperationNode
{
    public override string Symbol => "<="; 
    
    public SmallerEqualNode(ParserRuleContext context):base(context){}
    
    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = LeftOperand?.Execute(env);
        object? rightValue = RightOperand?.Execute(env);
    
        // Check if both operands are integers
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            return leftInt <= rightInt;
        }
    
        throw new Exception($"Type error: '<=' operation expects two Ints but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {Line}, column {Column}");
    }
}