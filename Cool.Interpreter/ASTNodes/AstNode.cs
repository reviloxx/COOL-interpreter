namespace Cool.Interpreter.ASTNodes;

public abstract class AstNode
{
    public int Line { get; }

    public int Column { get; }

    public Dictionary<string, dynamic> Attributes { get; } = [];

    public AstNode(ParserRuleContext? context)
    {
        if (context != null)
        {
            Line = context.Start.Line;
            Column = context.Start.Column + 1;
        }
        else
        {
            // For built-in nodes or nodes without context
            Line = 0;
            Column = 0;
        }
    }

    public AstNode(int line, int column)
    {
        Line = line;
        Column = column;
    }

    
    private static int indentLevel = 0;
    private const string IndentString = "  "; // Two spaces per level
    
    protected static string GetIndentation()
    {
        return string.Concat(Enumerable.Repeat(IndentString, indentLevel));
    }
    
    protected static void IncreaseIndent() => indentLevel++;
    protected static void DecreaseIndent() => indentLevel--;
    
    public override string ToString()
    {
        return $"{GetIndentation()}AstNode at line {Line}, column {Column}";
    }
    
    
    // internal abstract TResult Evaluate<TResult>(AstEvaluator<TResult> evaluator) where TResult : class;

    // public abstract void Execute();
    public abstract object? Execute(RuntimeEnvironment env);
}