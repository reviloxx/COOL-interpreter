namespace Cool.Interpreter.ASTNodes;

public class ProgramNode(ParserRuleContext context) : AstNode(context)
{
    public List<ClassDefineNode?> ClassDefineNodes = new();

    // public void Accept(ICoolGrammarVisitor<ASTNodes> visitor)
    // {
    // visitor.Visit(this);
    // }

    public override object? Execute(RuntimeEnvironment env)
    {
        
        // Make sure to register all classes in the runtime env
        foreach (var classDefineNode in ClassDefineNodes)
        {
          env.RegisterClass(classDefineNode);   
        } 
        
        // Find the main Class
        var mainClass = ClassDefineNodes
            .FirstOrDefault(c => c?.ClassName.Equals("Main", StringComparison.OrdinalIgnoreCase) == true);

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
        foreach (var classNode in ClassDefineNodes)
        {
            if (classNode != null)
            {
                sb.AppendLine(classNode.ToString());
            }
        }
        DecreaseIndent();
        
        return sb.ToString();
    }
    
}

