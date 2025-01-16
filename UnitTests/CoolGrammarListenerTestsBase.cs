using Cool.Interpreter;

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

    protected void StartVisitor(ProgramContext context)
    {
        CoolGrammarVisitor visitor = new();
        visitor.Visit(context);
    }

    protected void StartListener(ProgramContext context)
    {
        ParseTreeWalker walker = new();
        CoolGrammarDebugListener listener = new();
        walker.Walk(listener, context);
    }
}
