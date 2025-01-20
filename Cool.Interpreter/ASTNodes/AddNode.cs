using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class AddNode : BinaryOperationNode
{
    public override string Symbol => "+";

    public AddNode(ParserRuleContext context) : base(context)
    {
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
    //     sb.Append("Operator Add: (");
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
    
    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = LeftOperand?.Execute(env);
        object? rightValue = RightOperand?.Execute(env);
    
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
    
        throw new Exception($"Type error: '+' operation expects either two Ints or two Strings but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {Line}, column {Column}");
    }
}