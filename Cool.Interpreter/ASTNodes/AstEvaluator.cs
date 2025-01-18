namespace Cool.Interpreter.ASTNodes;

internal abstract class AstEvaluator<TResult> where TResult : class
{
    public virtual TResult Evaluate() => throw new NotImplementedException();
}