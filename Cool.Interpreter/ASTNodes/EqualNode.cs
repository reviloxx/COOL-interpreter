using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class EqualNode : BinaryOperationNode
{
   public override string Symbol => "=";
   public EqualNode(ParserRuleContext context) :base(context){} 
   
   public override object? Execute(RuntimeEnvironment env)
   {
      throw new NotImplementedException();
   }
}