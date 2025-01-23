using Antlr4.Runtime.Misc;
using Cool.Interpreter.ASTNodes;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitorAstBuilder : CoolGrammarBaseVisitor<object?>
{
    public override ProgramNode VisitProgram([NotNull] ProgramContext context)
    {
        var classDefineNodes = context.classDefine()
            .Select(VisitClassDefine);

        return new ProgramNode(classDefineNodes, context);
    }

    public override ClassDefineNode VisitClassDefine([NotNull] ClassDefineContext context)
    {
        var className = context.TYPE(0).GetText();
        var baseClassName = context.TYPE(1)?.GetText();
        var features = context.feature().Select(VisitFeature);

        ClassDefineNode classDefineNode = new(className, baseClassName, context);
        classDefineNode.AddFeatures(features);

        return classDefineNode;
    }

    public override FeatureNode VisitFeature([NotNull] FeatureContext context)
    {
        if (context.method() != null)
        {
            return VisitMethod(context.method());
        }
        else if (context.property() != null)
        {
            return VisitProperty(context.property());
        }

        throw new NotSupportedException($"Feature {context.GetText()} not supported");
    }

    public override MethodNode VisitMethod([NotNull] MethodContext context)
    {
        var methodName = context.ID().GetText();
        var parameters = context.formal().Select(VisitFormal);
        var returnType = new TypeNode(context.TYPE().GetText(), context);
        var body = Visit(context.expression()) as BlockSequenceNode;

        return new MethodNode(methodName, parameters, returnType, body!, context);
    }

    public override PropertyNode VisitProperty([NotNull] PropertyContext context)
    {
        var propertyName = context.formal().ID().GetText();
        var propertyType = new TypeNode(context.formal().TYPE().GetText(), context);
        var initialValue = context.expression() != null
                ? Visit(context.expression()) as ExpressionNode
                : null;

        return new PropertyNode(propertyName, propertyType, initialValue, context);
    }

    public override AssignmentNode VisitAssignment([NotNull] AssignmentContext context)
    {
        var targetName = context.ID().GetText();
        var value = Visit(context.expression()) as ExpressionNode;
        
        return new AssignmentNode(targetName, value, context);
    }

    public override BlockSequenceNode VisitBlock([NotNull] BlockContext context)
    {
        var expressions = context.expression().Select(x => Visit(x) as ExpressionNode);
        return new BlockSequenceNode(expressions!, context);
    }

    public override ParameterNode VisitFormal([NotNull] FormalContext context)
    {
        var parameterName = context.ID().GetText();
        var parameterType = new TypeNode(context.TYPE().GetText(), context);

        return new ParameterNode(parameterName, parameterType, context);
    }

    public override BoolNode VisitBoolean([NotNull] BooleanContext context)
    {
        var value = context.value.Type == CoolGrammarLexer.TRUE;
        return new BoolNode(value, context);
    }

    public override BoolNotNode VisitBoolNot([NotNull] BoolNotContext context)
    {
        var operand = Visit(context.expression()) as ExpressionNode;
         return new BoolNotNode(operand!, context);
    }
        
    public override IntNode VisitInt([NotNull] IntContext context)
    {
        var value = int.Parse(context.INT().GetText());
        return new IntNode(value, context);
    }

    public override StringNode VisitString([NotNull] StringContext context)
    {
        var value = context.STRING().GetText().Trim('"'); // Remove the surrounding quotes from the parsed string
        return new StringNode(value, context);
    }    

    public override BinaryOperationNode VisitArithmetic([NotNull] ArithmeticContext context)
    {
        if (Visit(context.expression(0)) is not ExpressionNode leftOperand)
            throw new InvalidOperationException($"Left expression visit returned null for {context.expression(0).GetText()}");

        if (Visit(context.expression(1)) is not ExpressionNode rightOperand)
            throw new InvalidOperationException($"Right expression visit returned null for {context.expression(1).GetText()}");

        return context.op.Text switch
        {
            "+" => new AddNode(leftOperand, rightOperand, context),
            "-" => new SubNode(leftOperand, rightOperand, context),
            "*" => new MulNode(leftOperand, rightOperand, context),
            "/" => new DivNode(leftOperand, rightOperand, context),
            _ => throw new NotSupportedException(),
        };
    }

    public override BinaryOperationNode VisitComparisson([NotNull] ComparissonContext context)
    {
        if (Visit(context.expression(0)) is not ExpressionNode leftOperand)
            throw new InvalidOperationException($"Left expression visit returned null for {context.expression(0).GetText()}");

        if (Visit(context.expression(1)) is not ExpressionNode rightOperand)
            throw new InvalidOperationException($"Right expression visit returned null for {context.expression(1).GetText()}");

        BinaryOperationNode node = context.op.Text switch
        {
            "=" => new EqualNode(leftOperand, rightOperand, context),
            "<" => new SmallerNode(leftOperand, rightOperand, context),
            "<=" => new SmallerEqualNode(leftOperand, rightOperand, context),
            _ => throw new NotSupportedException(),
        };
        return node;
    }

    public override IdNode VisitId([NotNull] IdContext context)
    {
        return new IdNode(context.ID().GetText(), context);
    }

    public override DispatchNode VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    {
        var methodName = context.ID().GetText();
        var arguments = context.expression()
            .Where(expr => expr != null)
            .Select(expr => Visit(expr) as ExpressionNode);

        return new DispatchNode(methodName, arguments!, staticTypeName: null, expression: null, context);
    }

    public override DispatchNode VisitDispatchExplicit([NotNull] DispatchExplicitContext context)
    {
        var methodName = context.ID().GetText();
        var expression = Visit(context.expression(0)) as ExpressionNode;
        var staticTypeName = context.TYPE()?.GetText();
        var arguments = context.expression()
            .Skip(1)  // Skip the first expression (object)
            .Where(expr => expr != null)
            .Select(expr => Visit(expr) as ExpressionNode);

        return new DispatchNode(methodName, arguments!, staticTypeName, expression, context);
    }    

    public override NewNode VisitNew([NotNull] NewContext context)
    {
        var typeName = context.TYPE().GetText();
        return new NewNode(typeName, context);
    }

    public override ParenthesesNode VisitParentheses([NotNull] ParenthesesContext context)
    {
        var expression = Visit(context.expression()) as ExpressionNode;
        return new ParenthesesNode(expression, context);
    }

    public override LetInNode VisitLetIn([NotNull] LetInContext context)
    {
        var declarations = context.property().Select(VisitProperty);
        var body = Visit(context.expression()) as ExpressionNode;

        // Create a LetInNode to represent the let-in expression
        return new LetInNode(declarations, body, context);
    }

    public override AstNode? VisitIsvoid([NotNull] IsvoidContext context)
    {
        throw new NotImplementedException();
    }

    public override WhileNode VisitWhile([NotNull] WhileContext context)
    {
        var condition = Visit(context.expression(0)) as ExpressionNode;
        var body = Visit(context.expression(1)) as ExpressionNode;

        return new WhileNode(condition!, body!, context);
    }

    public override NegativeNode VisitNegative([NotNull] NegativeContext context)
    {
        var expression = Visit(context.expression()) as ExpressionNode;
        return new NegativeNode(expression, context);
    }

    public override CaseNode? VisitCase([NotNull] CaseContext context)
    {
        // Die Expression für das case-Statement
        var caseExpression = Visit(context.expression(0)) as ExpressionNode 
                             ?? throw new InvalidOperationException("Case expression is null.");

        // Branches parsen und FormalNode für ID und Typ verwenden
        var caseBranches = new List<ExpressionNode?>();

        for (int i = 1; i < context.expression().Length; i++)
        {
            caseBranches.Add(Visit(context.expression(i)) as ExpressionNode);

        }
//TODO irgendwas hier noch machen denk ich
        CaseNode? caseNode = new CaseNode(caseExpression, caseBranches);
        // return new CaseNode(caseExpression, branches, context);
        return caseNode;
    }


}