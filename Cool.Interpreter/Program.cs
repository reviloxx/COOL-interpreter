using Antlr4.Runtime;
using Cool.Interpreter;
using Cool.Interpreter.ASTNodes;

var inputFile = "helloworld.cl";
var streamReader = new StreamReader(inputFile);

AntlrInputStream input = new(streamReader);
CoolGrammarLexer lexer = new(input);
CommonTokenStream tokens = new(lexer);
CoolGrammarParser parser = new(tokens);
var context = parser.program();

CoolGrammarVisitorAstBuilder builder = new();

AstNode root_node = (AstNode)builder.Visit(context)!;
//Console.WriteLine(root_node);

RuntimeEnvironment env = new();
root_node.Execute(env);

