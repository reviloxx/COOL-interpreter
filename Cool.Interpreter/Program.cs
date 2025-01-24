using Cool.Interpreter;
using Cool.Interpreter.ASTNodes;

var inputFile = "prime.cl";
var streamReader = new StreamReader(inputFile);

AntlrInputStream input = new(streamReader);
CoolGrammarLexer lexer = new(input);
CommonTokenStream tokens = new(lexer);
CoolGrammarParser parser = new(tokens);
var context = parser.program();

CoolGrammarVisitorAstBuilder builder = new();

AstNode root_node = (AstNode)builder.Visit(context)!;

RuntimeEnvironment env = new();
root_node.Execute(env);

