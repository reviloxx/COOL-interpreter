namespace Cool.Interpreter.ASTNodes;

public class ProgramNode(IEnumerable<ClassDefineNode> classDefineNodes, ParserRuleContext? context) : AstNode(context)
{
    private readonly IEnumerable<ClassDefineNode> _classDefineNodes = classDefineNodes;

    public override object? Execute(RuntimeEnvironment env)
    {        
        // Make sure to register all classes in the runtime env
        foreach (var classDefineNode in _classDefineNodes)
        {
          env.RegisterClass(classDefineNode);   
        } 
        
        // Find the main Class
        var mainClass = _classDefineNodes
            .FirstOrDefault(c => c.ClassName.Equals("Main", StringComparison.OrdinalIgnoreCase) == true);

        if (mainClass != null)
        {
            mainClass.Execute(env);
        }
        else
        {
            throw new Exception("No Main class found in the program");
        }

        return null;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Program");
        
        IncreaseIndent();
        foreach (var classDefineNode in _classDefineNodes)
        {
            if (classDefineNode != null)
            {
                sb.AppendLine(classDefineNode.ToString());
            }
        }
        DecreaseIndent();
        
        return sb.ToString();
    }
    
}

