namespace UnitTests;

public abstract class CoolGrammarListenerTestsBase
{
    protected ProgramContext GetProgramContext(string fileName)
    {
        var streamReader = new StreamReader(fileName);

        AntlrInputStream input = new(streamReader);
        CoolGrammarLexer lexer = new(input);
        CommonTokenStream tokens = new(lexer);
        CoolGrammarParser parser = new(tokens);

        return parser.program();
    }

    protected void WalkProgramContext(ProgramContext context)
    {
        ParseTreeWalker walker = new();
        CoolGrammarListener listener = new();
        walker.Walk(listener, context);
    }
}
