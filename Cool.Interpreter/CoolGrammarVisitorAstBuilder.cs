using Antlr4.Runtime.Misc;
using Cool.Interpreter.ASTNodes;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitorAstBuilder : CoolGrammarBaseVisitor<object?>
{
    public override AstNode? VisitProgram([NotNull] ProgramContext context)
    {
        ProgramNode programNode = new(context)
        {
            ClassDefineNodes = context.classDefine()
                .Select(x => VisitClassDefine(x) as ClassDefineNode)
                .ToList()
        };

        return programNode;
    }

    public override AstNode? VisitClassDefine([NotNull] ClassDefineContext context)
    {
        var className = context.TYPE(0).GetText();
        var baseClassName = context.TYPE(1)?.GetText();

        ClassDefineNode classDefineNode = new(context, className, baseClassName);

        // Process each feature and add it to the appropriate collection
        foreach (var featureContext in context.feature())
        {
            if (VisitFeature(featureContext) is FeatureNode feature)
            {
                classDefineNode.AddFeature(feature);
            }
        }

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

        return null;
    }

    public override AstNode? VisitMethod([NotNull] MethodContext context)
    {
        var node = new MethodNode(context)
        {
            FeatureName = new IdNode(context.ID().GetText(), context),
            ReturnType = new TypeNode(context, context.TYPE().GetText())
        };

        node.Parameters = context.formal()
            .Select(param => VisitFormal(param) as ParameterNode)
            .Where(param => param != null)
            .ToList()!;

        node.Body = Visit(context.expression()) as BlockSequenceNode;

        return node;
    }


    public override AstNode? VisitProperty([NotNull] PropertyContext context)
    {
        var formal = context.formal();

        return new PropertyNode(context)
        {
            FeatureName = new IdNode(formal.ID().GetText(), context),
            InitialValue = context.expression() != null
                ? Visit(context.expression()) as ExpressionNode
                : null
        };
    }

    public override AstNode? VisitAssignment([NotNull] AssignmentContext context)
    {
        var targetName = context.ID().GetText();
        var value = Visit(context.expression()) as ExpressionNode;

        return new AssignmentNode(context, targetName, value);
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
        BoolNotNode node = new(context)
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
        if (Visit(context.expression(0)) is not ExpressionNode leftOperand)
            throw new InvalidOperationException($"Left expression visit returned null for {context.expression(0).GetText()}");

        if (Visit(context.expression(1)) is not ExpressionNode rightOperand)
            throw new InvalidOperationException($"Right expression visit returned null for {context.expression(1).GetText()}");

        return context.op.Text switch
        {
            "+" => new AddNode(context, leftOperand, rightOperand),
            "-" => new SubNode(context, leftOperand, rightOperand),
            "*" => new MulNode(context, leftOperand, rightOperand),
            "/" => new DivNode(context, leftOperand, rightOperand),
            _ => throw new NotSupportedException(),
        };
    }

    public override AstNode? VisitComparisson([NotNull] ComparissonContext context)
    {
        if (Visit(context.expression(0)) is not ExpressionNode leftOperand)
            throw new InvalidOperationException($"Left expression visit returned null for {context.expression(0).GetText()}");

        if (Visit(context.expression(1)) is not ExpressionNode rightOperand)
            throw new InvalidOperationException($"Right expression visit returned null for {context.expression(1).GetText()}");

        return context.op.Text switch
        {
            "=" => new EqualNode(context, leftOperand, rightOperand),
            "<" => new SmallerNode(context, leftOperand, rightOperand),
            "<=" => new SmallerEqualNode(context, leftOperand, rightOperand),
            _ => throw new NotSupportedException(),
        };
    }

    public override AstNode? VisitId([NotNull] IdContext context)
    {
        return new IdNode(context.ID().GetText(), context);
    }

    public override AstNode? VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    {
        var methodName = context.ID().GetText();
        var arguments = context.expression()
            .Where(expr => expr != null)
            .Select(expr => Visit(expr) as ExpressionNode)
            .ToList();

        return new DispatchNode(context, methodName, arguments!);
    }

    public override AstNode? VisitDispatchExplicit([NotNull] DispatchExplicitContext context)
    {
        var methodName = context.ID().GetText();
        var expression = Visit(context.expression(0)) as ExpressionNode;
        var staticTypeName = context.TYPE()?.GetText();
        var arguments = context.expression()
            .Skip(1)  // Skip the first expression (object)
            .Where(expr => expr != null)
            .Select(expr => Visit(expr) as ExpressionNode)            
            .ToList();

        return new DispatchNode(context, methodName, arguments!, staticTypeName, expression);
    }    

    public override AstNode? VisitNew([NotNull] NewContext context)
    {
        return new NewNode(context)
        {
            TypeName = new IdNode(context.TYPE().GetText(), context)
        };
    }

    public override AstNode? VisitParentheses([NotNull] ParenthesesContext context)
    {
        return new ParenthesesNode(context)
        {
            Expression = Visit(context.expression()) as ExpressionNode
        };
    }

    public override AstNode? VisitLetIn([NotNull] LetInContext context)
    {
        // Create a LetInNode to represent the let-in expression
        LetInNode letInNode = new(context);

        // Process each property in the let declaration
        foreach (var propertyContext in context.property())
        {
            if (VisitProperty(propertyContext) is PropertyNode propertyNode)
            {
                letInNode.Declarations.Add(propertyNode);
            }
        }

        // Process the body expression
        letInNode.Body = Visit(context.expression()) as ExpressionNode;        

        return letInNode;
    }


    public override AstNode? VisitIsvoid([NotNull] IsvoidContext context)
    {
        throw new NotImplementedException();
    }

    public override AstNode? VisitWhile([NotNull] WhileContext context)
    {
        return new WhileNode(context)
        {
            Condition = Visit(context.expression(0)) as ExpressionNode,
            Body = Visit(context.expression(1)) as ExpressionNode
        };
    }

    public override AstNode? VisitNegative([NotNull] NegativeContext context)
    {
        return new NegativeNode(context)
        {
            Expression = Visit(context.expression()) as ExpressionNode
        };
    }

    public override AstNode? VisitIf([NotNull] IfContext context)
    {
        if (Visit(context.expression(0)) is not ExpressionNode condition)
            throw new InvalidOperationException($"Condition expression visit returned null for {context.expression(0).GetText()}");

        if (Visit(context.expression(1)) is not ExpressionNode thenBranch)
            throw new InvalidOperationException($"Then branch expression visit returned null for {context.expression(1).GetText()}");

        var elseBranch = Visit(context.expression(2)) as ExpressionNode;

        return new IfNode(context, condition, thenBranch, elseBranch);
    }

    public override AstNode? VisitCase([NotNull] CaseContext context)
    {
        throw new NotImplementedException();
    }  
}