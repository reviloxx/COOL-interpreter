using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class TypeNode : AstNode
{
    public string TypeName { get; private set; }
    public TypeEnum ValueType { get; private set; }

    public enum TypeEnum { tUndefined, tVoid, tInt, tString,tBool };

    public TypeNode(ParserRuleContext context, string typeName) : base(context)
    {
        TypeName = typeName;
        ValueType = TypeEnum.tUndefined;

        SetValueType();
    }

    public TypeNode(int line, int column, string typeName) : base(line, column)
    {
        TypeName = typeName;
        ValueType = TypeEnum.tUndefined;

        SetValueType();
    }

    void SetValueType()
    {
        if (TypeName == "string")
            ValueType = TypeEnum.tString;
        if (TypeName == "int")
            ValueType = TypeEnum.tInt;
        if (TypeName == "bool")
            ValueType = TypeEnum.tBool;
        if (TypeName == "void")
            ValueType = TypeEnum.tVoid;
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        return TypeName + " (" + ValueType.ToString() + ")";
    }
}