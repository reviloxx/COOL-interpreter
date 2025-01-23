namespace Cool.Interpreter.ASTNodes
{
    /// <summary>
    /// Represents one branch in a case expression:
    ///   id : Type => expression
    /// </summary>
    public record CaseBranch(string Id, string TypeName, ExpressionNode Expression);

    /// <summary>
    /// Represents a 'case' expression in COOL, which has the form:
    ///   case expr0 of
    ///       id1 : Type1 => expr1;
    ///       id2 : Type2 => expr2;
    ///       ...
    ///   esac
    /// </summary>
    public class CaseNode : ExpressionNode
    {
        private readonly ExpressionNode _scrutinee;         // The expression we're "switching" on
        private readonly List<CaseBranch> _branches;        // All the branches

        public CaseNode(
            ExpressionNode scrutinee,
            List<CaseBranch> branches,
            ParserRuleContext? context = null)
            : base(context)
        {
            _scrutinee = scrutinee;
            _branches = branches;
        }

        //public override object? Execute(RuntimeEnvironment env)
        //{
        //    // 1. Evaluate the main expression (the scrutinee).
        //    var value = _scrutinee.Execute(env);

        //    // 2. Determine its runtime COOL type. (In a real interpreter,
        //    //    you'd have your own representation of COOL types,
        //    //    not just .NET's System.Type.)
        //    string runtimeTypeName = env.GetCoolType(value);
        //    // For example, if you store an internal type string
        //    // like "Int", "Bool", "String", "MyClass", etc.

        //    // 3. Find the first branch that is a supertype or matching type.
        //    foreach (var branch in _branches)
        //    {
        //        // In COOL, we check if runtimeTypeName <= branch.TypeName
        //        // in terms of the class hierarchy. That is, if the value's
        //        // type is a subtype of the branch type.
        //        if (env.IsSubtype(runtimeTypeName, branch.TypeName))
        //        {
        //            // 4. Introduce a new scope so we can bind the branch id.
        //            env.PushScope();

        //            // 5. Bind the branch variable to the scrutinee's value.
        //            env.DeclareVariable(branch.Id, value);

        //            // 6. Execute the branch’s expression.
        //            object? result = branch.Expression.Execute(env);

        //            // Cleanup: leave the new scope
        //            env.PopScope();

        //            // Return the result of this branch.
        //            return result;
        //        }
        //    }

        //    // If no branch matches, COOL specifies a runtime error.
        //    throw new Exception("No suitable case branch found at runtime.");
        //}

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetIndentation()}case {_scrutinee} of");
            foreach (var branch in _branches)
            {
                sb.AppendLine(
                    $"{GetIndentation()}  {branch.Id} : {branch.TypeName} => {branch.Expression};");
            }
            sb.Append($"{GetIndentation()}esac");
            return sb.ToString();
        }
    }
}