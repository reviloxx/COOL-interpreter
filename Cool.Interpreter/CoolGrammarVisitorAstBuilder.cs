using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Cool.Interpreter.ASTNodes;
using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolGrammarVisitorAstBuilder : CoolGrammarBaseVisitor<object?>
{
    // private readonly Dictionary<string, ClassDefineNode> _classDefinitions = [];
    private readonly List<ClassDefineNode> _classDefinitions = [];
    // private readonly Dictionary<string, IdNode?> _variables = [];
    private readonly List<IdNode?> _variables = [];
    
    // private bool _invertNextAssignment = false;
    // private CoolClass? _classToDefine;

    public override object? VisitProgram([NotNull] ProgramContext context)
    {
        ProgramNode programNode = new ProgramNode(context);
        
        // foreach (var classDefine in context.classDefine())
        // {
            // _classDefinitions.Add(VisitClassDefine(classDefine));
        // }
        
        // ProgramNode programNode = new ProgramNode(context);
        
        programNode.ClassDefineNodes = context.classDefine().Select(x => VisitClassDefine(x) as ClassDefineNode);

        // Trausis Code:
        // FileNode theWhole = new FileNode(context);
        // theWhole.Functions = context.functionDecl().Select(x => Visit(x) as FunctionNode).ToList();
        // theWhole.Variables = context.varDecl().Select(x => Visit(x) as VariableNode).ToList();

        // return theWhole;
    }
    
    public override object? VisitClassDefine([NotNull] ClassDefineContext context)
    {
        foreach (var classDefineContext in context.classDefine())
        {
            var classDefineNode = VisitClassDefine(classDefineContext) as ClassDefineNode;
            if (classDefineNode != null)
            {
                _classDefinitions.Add(classDefineNode);
            }
        }

        return null;
        
        // var className = context.TYPE(0).GetText();
        // var baseClassName = context.TYPE(1)?.GetText();
        //
        // _classToDefine = new(className, baseClassName);
        //
        // foreach (var feature in context.feature())
        // {
        //     Visit(feature);
        // }
        //
        // _classDefinitions.Add(_classToDefine.Name, _classToDefine);
        // _classToDefine = null;
        // return null;
    }
    //
    // public override object? VisitFeature([NotNull] FeatureContext context)
    // {
    //     if (_classToDefine == null)
    //         throw new Exception("Failed to add feature to class definition!");
    //
    //     if (context.method() != null)
    //         _classToDefine.AddMethod(context.method().ID().GetText(), context.method());
    //
    //     // TODO
    //     //if (context.property() != null)
    //     //    _classToDefine.AddProperty(context.property().ASSIGNMENT().)
    //
    //     return null;
    // }
    //
    // public override object? VisitBlock([NotNull] BlockContext context)
    // {
    //     foreach (var expression in context.expression())
    //     {
    //         Visit(expression);
    //     }
    //     return null;
    // }
    //
    // public override object? VisitMethod([NotNull] MethodContext context)
    // {
    //     var methodName = context.ID().GetText();
    //     var args = context.formal().Select(Visit);
    //     var returnType = context.TYPE().GetText();
    //
    //     Visit(context.expression());
    //
    //     return null;
    // }
    //
    // public override object? VisitAssignment(AssignmentContext context)
    // {
    //     var varname = context.ID().GetText();
    //     var value = Visit(context.expression());
    //
    //     if (value is BoolNotContext)
    //     {
    //         _invertNextAssignment = true;
    //         return null;
    //     }
    //
    //     _variables[varname] = _invertNextAssignment? !(bool)value! : value;
    //     _invertNextAssignment = false;
    //
    //     return null;
    // }
    //
    // public override object? VisitBoolean([NotNull] BooleanContext context)
    //     => context.TRUE() != null;
    //
    // public override object? VisitBoolNot([NotNull] BoolNotContext context)
    //     => context;
    //
    // public override object? VisitInt([NotNull] IntContext context) 
    //     => int.Parse(context.INT().GetText());
    //
    // public override object? VisitString([NotNull] StringContext context) 
    //     => context.STRING().GetText();
    //
    // public override object? VisitArithmetic([NotNull] ArithmeticContext context)
    // {
    //     var left = Visit(context.expression(0))!;
    //     var right = Visit(context.expression(1))!;
    //
    //     if (left.GetType() != right.GetType())
    //         throw new Exception("Only matching types are supported for arithmetic expressions!");
    //
    //     if (left is int)
    //     {
    //         var leftInt = int.Parse(left.ToString()!);
    //         var rightInt = int.Parse(right.ToString()!);
    //
    //         return context.op.Text switch
    //         {
    //             "+" => leftInt + rightInt,
    //             "-" => leftInt - rightInt,
    //             "*" => leftInt * rightInt,
    //             "/" => rightInt != 0 ? leftInt / rightInt : throw new DivideByZeroException(),
    //             _ => throw new Exception($"Operator {context.op.Text} not supported for int arithmetics!")
    //         };
    //     }
    //
    //     if (left is string)
    //     {
    //         var leftStr = left.ToString();
    //         var rightStr = right.ToString();
    //
    //         return context.op.Text switch
    //         {
    //             "+" => leftStr + rightStr,
    //             _ => throw new Exception($"Operator {context.op.Text} not supported for string arithmetics!")
    //         };
    //     }
    //
    //     throw new Exception($"Type {left.GetType().Name} not supported for arithmetic expressions!");
    // }
    //
    // public override object? VisitComparisson([NotNull] ComparissonContext context)
    // {
    //     var left = Visit(context.expression(0))!;
    //     var right = Visit(context.expression(1))!;
    //
    //     if (left.GetType() != right.GetType())
    //         throw new Exception("Only matching types are supported for comparisson expressions!");
    //
    //     if (left is int)
    //     {
    //         var leftInt = int.Parse(left.ToString()!);
    //         var rightInt = int.Parse(right.ToString()!);
    //
    //         return context.op.Text switch
    //         {
    //             "<=" => leftInt <= rightInt,
    //             "<" => leftInt < rightInt,
    //             "=" => leftInt == rightInt,
    //             _ => throw new Exception($"Operator {context.op.Text} not supported for int comparisson!")
    //         };
    //     }
    //
    //     if (left is string)
    //     {
    //         var leftStr = left.ToString();
    //         var rightStr = right.ToString();
    //
    //         return context.op.Text switch
    //         {
    //             "=" => leftStr == rightStr,
    //             _ => throw new Exception($"Operator {context.op.Text} not supported for string comparisson!")
    //         };
    //     }
    //
    //     if (left is bool)
    //     {
    //         var leftBool = string.Equals(left.ToString(), "true", StringComparison.OrdinalIgnoreCase);
    //         var rightBool = string.Equals(right.ToString(), "true", StringComparison.OrdinalIgnoreCase);
    //
    //         return context.op.Text switch
    //         {
    //             "=" => leftBool == rightBool,
    //             _ => throw new Exception($"Operator {context.op.Text} not supported for boolean comparisson!")
    //         };
    //     }
    //
    //     throw new Exception($"Type {left.GetType().Name} not supported for comparisson expressions!");
    // }
    //
    // public override object? VisitId([NotNull] IdContext context)
    // {
    //     var varName = context.ID().GetText();
    //
    //     if (!_variables.TryGetValue(varName, out object? value))
    //         throw new Exception($"Variable {varName} is not defined");
    //
    //     return value;
    // }
    //
    // public override object? VisitDispatchImplicit([NotNull] DispatchImplicitContext context)
    // {
    //     var methodName = context.ID().GetText();
    //     var args = context.expression().Select(Visit).ToList();
    //
    //     if (methodName == "out_string")
    //     {
    //         Console.WriteLine(args[0]?.ToString());
    //         return null;
    //     }
    //
    //     foreach (var expression in context.expression())
    //     {
    //         // TODO: how to call other methods?
    //         Visit(expression);
    //     }        
    //
    //     return null;
    // }
    //
    // public override object? VisitProperty([NotNull] PropertyContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitFormal([NotNull] FormalContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitNew([NotNull] NewContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitParentheses([NotNull] ParenthesesContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitLetIn([NotNull] LetInContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitIsvoid([NotNull] IsvoidContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitWhile([NotNull] WhileContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitNegative([NotNull] NegativeContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitIf([NotNull] IfContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitCase([NotNull] CaseContext context)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override object? VisitDispatchExplicit([NotNull] DispatchExplicitContext context)
    // {
    //     throw new NotImplementedException();
    // }
}