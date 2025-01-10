using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Cool;

var inputFile = "bool.cl";
var streamReader = new StreamReader(inputFile);

AntlrInputStream input = new(streamReader);
CoolGrammarLexer lexer = new(input);
CommonTokenStream tokens = new(lexer);
CoolGrammarParser parser = new(tokens);
var context = parser.program();

ParseTreeWalker walker = new();
CoolGrammarListener listener = new();
walker.Walk(listener, context);