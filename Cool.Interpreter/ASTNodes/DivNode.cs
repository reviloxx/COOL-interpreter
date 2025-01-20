using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DivNode : BinaryOperationNode
{
    public override string Symbol => "+";

    public DivNode(ParserRuleContext context) : base(context)
    {
    }

    
    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = LeftOperand?.Execute(env);
        object? rightValue = RightOperand?.Execute(env);
    
        // Check if both operands are integers
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            if (rightInt == 0)
            {
                throw new Exception($"Division by 0 at line {Line}, column {Column}");
 
            }
            return leftInt / rightInt;
        }
        
    
        throw new Exception($"Type error: '/' operation expects two Ints but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {Line}, column {Column}");
    }
    
    // public override void Accept(IVisitor visitor)
    // {
    //     visitor.Visit(this);
    // }

    // public override string ToString()
    // {
    //     StringBuilder sb = new StringBuilder();
    //
    //     int saveIdent = ident;
    //     string identString = emptyString.Substring(emptyString.Length - ident);
    //
    //     sb.Append(identString);
    //     sb.Append("Operator Div: (");
    //     sb.Append(Symbol);
    //     sb.Append(")");
    //     sb.Append("\n");
    //
    //     ident += 2;
    //     sb.Append(LeftOperand.ToString());
    //     sb.Append("\n");
    //     sb.Append(RightOperand.ToString());
    //     sb.Append("\n");
    //
    //     ident = saveIdent;
    //
    //     return sb.ToString();
    // }

}