namespace Cool.Interpreter.ASTNodes;

public abstract class AstNode(ParserRuleContext? context)
{
    private static int indentLevel = 0;
    private const string IndentString = "  "; // Two spaces per level

    protected readonly int _line = context?.Start.Line ?? 0;
    protected readonly int _column = context?.Start.Column + 1 ?? 0;

    public Dictionary<string, dynamic> Attributes { get; } = [];    
    
    protected static string GetIndentation()
    {
        return string.Concat(Enumerable.Repeat(IndentString, indentLevel));
    }
    
    protected static void IncreaseIndent() => indentLevel++;
    protected static void DecreaseIndent() => indentLevel--;  
    
    // public abstract void Execute();
    public abstract object? Execute(RuntimeEnvironment env);

    public override string ToString()
    {
        return $"{GetIndentation()}AstNode at line {_line}, column {_column}";
    }
}