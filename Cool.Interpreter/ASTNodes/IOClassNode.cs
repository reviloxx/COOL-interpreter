namespace Cool.Interpreter.ASTNodes;

public class IOClassNode : ClassDefineNode
{
    public IOClassNode() : base("IO") // no context since this is built-in
    {
        static object? write(RuntimeEnvironment env, List<object?> args)
        {
            if (args.Count > 0 && args[0] != null)
            {
                Console.Write(args[0]!.ToString());
            }
            return env.LookupVariable("self");
        }

        static object? writeLine(RuntimeEnvironment env, List<object?> args)
        {
            if (args.Count > 0 && args[0] != null)
            {
                Console.WriteLine(args[0]!.ToString());
            }
            return env.LookupVariable("self");
        }

        static object? readLine(RuntimeEnvironment env, List<object?> args)
        {
            return Console.ReadLine();
        }

        AddFeatures(
        [
            new BuiltInMethodNode("out_string", [new ParameterNode("x", new TypeNode("String"))], new TypeNode("SELF_TYPE"), write),
            new BuiltInMethodNode("out_int", [new ParameterNode("x", new TypeNode("Int"))], new TypeNode("SELF_TYPE"), write),
            new BuiltInMethodNode("out_stringln", [new ParameterNode("x", new TypeNode("String"))], new TypeNode("SELF_TYPE"), writeLine),
            new BuiltInMethodNode("out_intln", [new ParameterNode("x", new TypeNode("Int"))], new TypeNode("SELF_TYPE"), writeLine),
            new BuiltInMethodNode("in_string", [], new TypeNode("String"), readLine)
        ]);
    }
}