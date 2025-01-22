using Cool.Interpreter;
using Cool.Interpreter.ASTNodes;

namespace UnitTests;

public abstract class VisitorTestsBase
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

    protected AstNode GetRootNode(ProgramContext context)
    {
        CoolGrammarVisitorAstBuilder builder = new();
        return (AstNode)builder.Visit(context)!;
    }

    protected void Execute(AstNode rootNode)
    {
        RuntimeEnvironment env = new();
        rootNode.Execute(env);
    }
}
