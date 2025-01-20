using System.ComponentModel.Design;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Cool.Interpreter.ASTNodes;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitorAstBuilder : CoolGrammarBaseVisitor<object?>
{
    public override AstNode? VisitProgram([NotNull] ProgramContext context)
    {
        ProgramNode programNode = new ProgramNode(context);

        programNode.ClassDefineNodes =
            context.classDefine().Select(x => VisitClassDefine(x) as ClassDefineNode).ToList();

        return programNode;
    }

    public override AstNode? VisitClassDefine([NotNull] ClassDefineContext context)
    {
        var className = new IdNode(context.TYPE(0).GetText(), context);
        ClassDefineNode classDefineNode = new ClassDefineNode(context)
        {
            ClassName = className
        };

        if (context.TYPE(1) != null)
        {
            classDefineNode.BaseClassName = new IdNode(context.TYPE(1).GetText(), context);
        }

        // Features (Methoden, Properties) verarbeiten
        classDefineNode.FeatureNodes = context.feature()
            .Select(VisitFeature)
            .OfType<FeatureNode>() // Nur gültige FeatureNodes behalten
            .ToList();

        return classDefineNode;
    }


    public override AstNode? VisitFeature([NotNull] FeatureContext context)
    {
        if (context.method() != null)
        {
            return VisitMethod(context.method());
        }
        else if (context.property() != null)
        {
            return VisitProperty(context.property());
        }

        return null; // Unbekannter Feature-Typ
    }

    public override AstNode? VisitMethod([NotNull] MethodContext context)
    {
        var node = new MethodNode(context)
        {
            FeatureName = new IdNode(context.ID().GetText(), context), // Methodenname
            ReturnType = new TypeNode(context, context.TYPE().GetText()) // Rückgabetyp
        };

        node.Parameters = context.formal()
            .Select(param => VisitFormal(param) as ParameterNode)
            .Where(param => param != null)
            .ToList()!;

        node.Body = Visit(context.expression()) as BlockSequenceNode;
        
        //TODO - ALLE Kontexte abarbeiten - Erstes Problem: ich komme hier mit einem methodcontext rein, das sollte nicht möglich sein?
        // methodNode.Body = context.expression() switch
        // {
        //     ArithmeticContext arithmetic => VisitArithmetic(arithmetic) as ExpressionNode,
        //     AssignmentContext assignment => VisitAssignment(assignment) as ExpressionNode,
        //     BlockContext block => VisitBlock(block) as ExpressionNode,
        //     BoolNotContext boolNot => VisitBoolNot(boolNot) as ExpressionNode,
        //     BooleanContext boolean => VisitBoolean(boolean) as ExpressionNode,
        //     CaseContext caseContext => VisitCase(caseContext) as ExpressionNode,
        //     // ComparisonContext comparison => VisitComparison(comparison) as ExpressionNode,
        //     DispatchExplicitContext dispatchExplicit => VisitDispatchExplicit(dispatchExplicit) as ExpressionNode,
        //     DispatchImplicitContext dispatchImplicit => VisitDispatchImplicit(dispatchImplicit) as ExpressionNode,
        //     IdContext id => VisitId(id) as ExpressionNode,
        //     IfContext @if => VisitIf(@if) as ExpressionNode,
        //     IntContext @int => VisitInt(@int) as ExpressionNode,
        //     IsvoidContext isvoid => VisitIsvoid(isvoid) as ExpressionNode,
        //     LetInContext letIn => VisitLetIn(letIn) as ExpressionNode,
        //     NegativeContext negative => VisitNegative(negative) as ExpressionNode,
        //     StringContext str => VisitString(str) as ExpressionNode,
        //     WhileContext @while => VisitWhile(@while) as ExpressionNode,
        //     _ => throw new NotSupportedException("Unknown expression type.")
        // };

        return node;
    }


    public override AstNode? VisitProperty([NotNull] PropertyContext context)
    {
        var formal = context.formal();

        return new PropertyNode(context)
        {
            FeatureName = new IdNode(formal.ID().GetText(), context), // Name der Eigenschaft
            InitialValue = context.expression() != null
                ? Visit(context.expression()) as ExpressionNode // Optionaler Initialwert
                : null
        };
    }

    public override AstNode? VisitAssignment([NotNull] AssignmentContext context)
    {
        AssignmentNode node = new AssignmentNode(context)
        {
            Target = new IdNode(context.ID().GetText(), context)
        };
        
        AstNode value_node = Visit(context.expression()) as ExpressionNode;
        
        return new AssignmentNode(context)
        {
            Target = new IdNode(context.ID().GetText(), context), // Zielvariable
            Value = Visit(context.expression()) as ExpressionNode // Zuweisungswert
        };
    }

    public override AstNode? VisitBlock([NotNull] BlockContext context)
    {
        return new BlockSequenceNode(context)
        {
            Expressions = context.expression().Select(x => Visit(x) as ExpressionNode).ToList()
        };
    }

    public override AstNode? VisitFormal([NotNull] FormalContext context)
    {
        return new ParameterNode(context)
        {
            ParameterName = new IdNode(context.ID().GetText(), context),
            ParameterType = new TypeNode(context, context.TYPE().GetText())
        };
    }

    public override AstNode? VisitBoolean([NotNull] BooleanContext context)
    {
        return new BoolNode(context)
        {
            Value = context.value.Type == CoolGrammarLexer.TRUE
        };
    }

    public override AstNode? VisitBoolNot([NotNull] BoolNotContext context)
    {
        BoolNotNode node = new BoolNotNode(context)
        {
            Operand = Visit(context.expression()) as ExpressionNode
        };
        return node;
    }
        
    public override AstNode? VisitInt([NotNull] IntContext context)
    {
        return new IntNode(context)
        {
            Value = int.Parse(context.INT().GetText())
        };
    }

    public override AstNode? VisitString([NotNull] StringContext context)
    {
        return new StringNode(context)
        {
            Value = context.STRING().GetText().Trim('"') // Remove the surrounding quotes from the parsed string
        };
    }
    

    public override AstNode? VisitArithmetic([NotNull] ArithmeticContext context)
    {
       
        BinaryOperationNode node;
        switch (context.op.Text)
        {
            case "+":
                node = new AddNode(context);
                break;
            case "-":
                node = new SubNode(context);
                break;
            case "*":
                node = new MulNode(context);
                break;
            case "/":
                node = new DivNode(context);
                break;
            default:
                throw new NotSupportedException();
        }
        
        var leftExpr = Visit(context.expression(0));
        var rightExpr = Visit(context.expression(1));

        // Add debug checks
        if (leftExpr == null)
            throw new InvalidOperationException($"Left expression visit returned null for {context.expression(0).GetText()}");
    
        if (rightExpr == null)
            throw new InvalidOperationException($"Right expression visit returned null for {context.expression(1).GetText()}");

        node.LeftOperand = leftExpr as ExpressionNode 
                           ?? throw new InvalidOperationException($"Left expression is not an ExpressionNode. It is: {leftExpr.GetType()}");
        node.RightOperand = rightExpr as ExpressionNode 
                            ?? throw new InvalidOperationException($"Right expression is not an ExpressionNode. It is: {rightExpr.GetType()}");

        

        return node;
        
     
        // var left = Visit(context.expression(0))!;
        // var right = Visit(context.expression(1))!;
        //
        // if (left.GetType() != right.GetType())
        //     throw new Exception("Only matching types are supported for arithmetic expressions!");
        //
        // if (left is int)
        // {
        //     var leftInt = int.Parse(left.ToString()!);
        //     var rightInt = int.Parse(right.ToString()!);
        //
        //     return context.op.Text switch
        //     {
        //         "+" => leftInt + rightInt,
        //         "-" => leftInt - rightInt,
        //         "*" => leftInt * rightInt,
        //         "/" => rightInt != 0 ? leftInt / rightInt : throw new DivideByZeroException(),
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for int arithmetics!")
        //     };
        // }
        //
        // if (left is string)
        // {
        //     var leftStr = left.ToString();
        //     var rightStr = right.ToString();
        //
        //     return context.op.Text switch
        //     {
        //         "+" => leftStr + rightStr,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for string arithmetics!")
        //     };
        // }
        //
        // throw new Exception($"Type {left.GetType().Name} not supported for arithmetic expressions!");
    }

    public override AstNode? VisitComparisson([NotNull] ComparissonContext context)
    {

        BinaryOperationNode node;
        
        switch (context.op.Text)
        {
            case "=":
                node = new EqualNode(context);
                break;
            case "<":
                node = new SmallerNode(context);
                break;
            case "<=":
                node = new SmallerEqualNode(context);
                break;
            default:
                throw new NotSupportedException();
        }
        
        

        node.LeftOperand = Visit(context.expression(0)) as ExpressionNode;
        node.RightOperand = Visit(context.expression(1)) as ExpressionNode;
        return node;
        
        // throw new NotImplementedException();

        // var left = Visit(context.expression(0))!;
        // var right = Visit(context.expression(1))!;
        //
        // if (left.GetType() != right.GetType())
        //     throw new Exception("Only matching types are supported for comparisson expressions!");
        //
        // if (left is int)
        // {
        //     var leftInt = int.Parse(left.ToString()!);
        //     var rightInt = int.Parse(right.ToString()!);
        //
        //     return context.op.Text switch
        //     {
        //         "<=" => leftInt <= rightInt,
        //         "<" => leftInt < rightInt,
        //         "=" => leftInt == rightInt,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for int comparisson!")
        //     };
        // }
        //
        // if (left is string)
        // {
        //     var leftStr = left.ToString();
        //     var rightStr = right.ToString();
        //
        //     return context.op.Text switch
        //     {
        //         "=" => leftStr == rightStr,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for string comparisson!")
        //     };
        // }
        //
        // if (left is bool)
        // {
        //     var leftBool = string.Equals(left.ToString(), "true", StringComparison.OrdinalIgnoreCase);
        //     var rightBool = string.Equals(right.ToString(), "true", StringComparison.OrdinalIgnoreCase);
        //
        //     return context.op.Text switch
        //     {
        //         "=" => leftBool == rightBool,
        //         _ => throw new Exception($"Operator {context.op.Text} not supported for boolean comparisson!")
        //     };
        // }
        //
        // throw new Exception($"Type {left.GetType().Name} not supported for comparisson expressions!");
    }

    public override AstNode? VisitId([NotNull] IdContext context)
    {
        return new IdNode(context.ID().GetText(), context);
        // throw new NotImplementedException();

        // var varName = context.ID().GetText();
        //
        // if (!_variables.TryGetValue(varName, out object? value))
        //     throw new Exception($"Variable {varName} is not defined");
        //
        // return value;
    }

    public override AstNode? VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    {
        var node = new DispatchNode(context)
        {
            MethodName = new IdNode(context.ID().GetText(), context)
        };

        // Visit all arguments
        node.Arguments = context.expression()
            .Select(expr => Visit(expr) as ExpressionNode)
            .Where(expr => expr != null)
            .ToList()!;

        return node;
    }

    public override AstNode? VisitDispatchExplicit([NotNull] DispatchExplicitContext context)
    {
        var node = new DispatchNode(context)
        {
            Object = Visit(context.expression(0)) as ExpressionNode,
            MethodName = new IdNode(context.ID().GetText(), context)
        };

        // Handle @TYPE if present
        if (context.TYPE() != null)
        {
            node.StaticType = new TypeNode(context, context.TYPE().GetText());
        }

        // Visit all arguments (skip first expression as it's the object)
        node.Arguments = context.expression()
            .Skip(1)  // Skip the first expression (object)
            .Select(expr => Visit(expr) as ExpressionNode)
            .Where(expr => expr != null)
            .ToList()!;

        return node;
    }
    

    public override AstNode? VisitNew([NotNull] NewContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitParentheses([NotNull] ParenthesesContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitLetIn([NotNull] LetInContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitIsvoid([NotNull] IsvoidContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitWhile([NotNull] WhileContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitNegative([NotNull] NegativeContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitIf([NotNull] IfContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitCase([NotNull] CaseContext context)
    {
        throw new NotImplementedException();
    }

  
}