using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Cool.Interpreter;
using System.Drawing.Text;
using static CoolGrammarParser;
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
Console.WriteLine(root_node);

root_node.Execute();

