using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class EqualNode : BinaryOperationNode
{
   public override string Symbol => "=";
   public EqualNode(ParserRuleContext context) :base(context){} 
   
   public override object? Execute(RuntimeEnvironment env)
   {
         // Execute the operand
         object? LeftValue = LeftOperand?.Execute(env);
         object? RightValue = RightOperand?.Execute(env);
   
         // TODO check for same type needed?
         
         // Check if operand is a boolean
         return LeftValue == RightValue;
      
   }
}