namespace Cool.Interpreter.ASTNodes;

public class IOClassNode : ClassDefineNode
{
    public IOClassNode() : base("IO") // no context since this is built-in
    {
        static object? Write(RuntimeEnvironment env, List<object?> args)
        {
            Console.Write(args.FirstOrDefault()?.ToString());
            return env.LookupVariable("self");
        }

        static object? WriteLine(RuntimeEnvironment env, List<object?> args)
        {
            Console.WriteLine(args.FirstOrDefault()?.ToString());
            return env.LookupVariable("self");
        }

        static object? ReadString(RuntimeEnvironment env, List<object?> args)
        {
            return Console.ReadLine();
        }

        static object? ReadInt(RuntimeEnvironment env, List<object?> args)
        {
            if (!int.TryParse(Console.ReadLine(), out int value))
                throw new Exception();

            return value;                
        }

        AddFeatures(
        [
            new BuiltInMethodNode("out_string", [new ParameterNode("x", new TypeNode("String"))], new TypeNode("SELF_TYPE"), Write),
            new BuiltInMethodNode("out_int", [new ParameterNode("x", new TypeNode("Int"))], new TypeNode("SELF_TYPE"), Write),
            new BuiltInMethodNode("out_stringln", [new ParameterNode("x", new TypeNode("String"))], new TypeNode("SELF_TYPE"), WriteLine),
            new BuiltInMethodNode("out_intln", [new ParameterNode("x", new TypeNode("Int"))], new TypeNode("SELF_TYPE"), WriteLine),
            new BuiltInMethodNode("in_string", [], new TypeNode("String"), ReadString),
            new BuiltInMethodNode("in_int", [], new TypeNode("Int"), ReadInt)
        ]);
    }
}