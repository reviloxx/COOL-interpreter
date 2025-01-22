namespace Cool.Interpreter.ASTNodes;

public class IOClassNode : ClassDefineNode
{
    public IOClassNode() : base(null, "IO", null) // null context since this is built-in
    {        
        // Add built-in methods
        AddFeature(new BuiltInMethodNode()
        {
            FeatureName = new IdNode("out_string", null),  // Required by FeatureNode
            Parameters =
            [
                new ParameterNode(null) 
                {
                    ParameterName = new IdNode("x", null),
                    ParameterType = new TypeNode(null, "String")
                } 
            ],
            ReturnType = new TypeNode(null, "SELF_TYPE"),
            ExecuteImpl = (env, args) => 
            {
                if (args.Count > 0 && args[0] != null)
                {
                    Console.WriteLine(args[0]!.ToString());
                }
                return env.LookupVariable("self");
            }
        });

        AddFeature(new BuiltInMethodNode()
        {
            FeatureName = new IdNode("out_int", null),  // Required by FeatureNode
            Parameters =
            [
                new ParameterNode(null) 
                {
                    ParameterName = new IdNode("x", null),
                    ParameterType = new TypeNode(null, "Int")
                } 
            ],
            ReturnType = new TypeNode(null, "SELF_TYPE"),
            ExecuteImpl = (env, args) => 
            {
                if (args.Count > 0 && args[0] != null)
                {
                    Console.WriteLine(args[0]!.ToString());
                }
                return env.LookupVariable("self");
            }
        });
    }
}