using Antlr4.Runtime;
using System.Collections.Generic;
using System.Text;

namespace Cool.Interpreter.ASTNodes
{
    public class CaseNode : ExpressionNode
    {
        public ExpressionNode CaseExpression { get; }  // Ausdruck für das case-Statement
        public List<ExpressionNode> CaseBranches { get; }  // Liste der Zweige

        public CaseNode(ExpressionNode expression, List<ExpressionNode> caseBranches, ParserRuleContext? context = null)
            : base(context)
        {
            CaseExpression = expression;
            CaseBranches = caseBranches;
        }

        public override object? Execute(RuntimeEnvironment env)
        {
           
            var value = CaseExpression.Execute(env);
            if (value == null)
            {
                throw new InvalidOperationException("Runtime error: case expression is void.");
            }

            foreach (var expression in CaseBranches)
            {

                if (CaseExpression == expression)
                {
                    value = expression.Execute(env);
                }
                else
                {
                    Console.WriteLine("Runtime error: case expression is case.");
                }
                //TODO irgendwas hier noch machen
                // if (env.IsInstanceOf(value, formal.Type))  // Annahme: FormalNode enthält Typ-Information
                // {
                return Execute(env);
                // }
            }

            throw new InvalidOperationException("No matching case branch found.");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"case {CaseExpression} of");
            foreach (var expression in CaseBranches)
            {
                sb.AppendLine($"  {expression};");
            }
            sb.Append("esac");
            return sb.ToString();
        }
    }
}