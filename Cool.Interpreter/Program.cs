using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Cool.Interpreter;
using System.Drawing.Text;
using static CoolGrammarParser;

var inputFile = "helloworld.cl";
var streamReader = new StreamReader(inputFile);

AntlrInputStream input = new(streamReader);
CoolGrammarLexer lexer = new(input);
CommonTokenStream tokens = new(lexer);
CoolGrammarParser parser = new(tokens);
var context = parser.program();

StartVisitor(context);


static void StartVisitor(ProgramContext context)
{
    CoolGrammarVisitor visitor = new();
    visitor.Visit(context);
}

static void StartListener(ProgramContext context)
{
    ParseTreeWalker walker = new();
    CoolGrammarDebugListener listener = new();
    walker.Walk(listener, context);
}