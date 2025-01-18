﻿using Antlr4.Runtime;
using Microsoft.VisualBasic;

namespace Cool.Interpreter.ASTNodes;

public abstract class AstNode
{
    public int Line { get; }

    public int Column { get; }

    public Dictionary<string, dynamic> Attributes { get; }

    public AstNode(ParserRuleContext context)
    {
        Line = context.Start.Line;
        Column = context.Start.Column + 1;
        Attributes = new Dictionary<string, dynamic>();
    }

    public AstNode(int line, int column)
    {
        Line = line;
        Column = column;
    }

    // internal abstract TResult Evaluate<TResult>(AstEvaluator<TResult> evaluator) where TResult : class;

    // internal abstract void Accept(AstVisitor visitor);
}