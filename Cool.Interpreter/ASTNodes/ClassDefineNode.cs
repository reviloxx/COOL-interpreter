using System.Diagnostics;
using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;


public class ClassDefineNode : AstNode
{
        public required IdNode ClassName { get; set; } // ClassName
        public IdNode? BaseClassName { get; set; } // Optional: BaseClass
    
        private Dictionary<string, MethodNode> Methods { get; } = new();
        private Dictionary<string, PropertyNode> Properties { get; } = new();

        public ClassDefineNode(ParserRuleContext context) : base(context)
        {
                
        }
        
        public void AddFeature(FeatureNode feature)
        {
                if (feature is MethodNode method)
                {
                        Methods[method.FeatureName.Name.ToUpperInvariant()] = method;
                }
                else if (feature is PropertyNode property)
                {
                        Properties[property.FeatureName.Name.ToUpperInvariant()] = property;
                }
        }
        
        public MethodNode? GetMethod(string name)
        {
                return Methods.TryGetValue(name.ToUpperInvariant(), out var method) ? method : null;
        }
        
        
        public override string ToString()
        {
                var sb = new StringBuilder();
                sb.Append($"{GetIndentation()}Class {ClassName}");
        
                if (BaseClassName != null)
                {
                        sb.Append($" inherits {BaseClassName}");
                }
                sb.AppendLine();
        
                IncreaseIndent();
                foreach (var method in Methods)
                {
                        sb.AppendLine(method.ToString());
                }
                foreach (var property in Properties)
                {
                        sb.AppendLine(property.ToString());
                }
                
                DecreaseIndent();
        
                return sb.ToString();
        }
        
        public override object? Execute(RuntimeEnvironment env)
        {
                // Find and execute the main method if this is the Main class
                if (ClassName.Name.Equals("Main", StringComparison.OrdinalIgnoreCase))
                {
                        var mainMethod = GetMethod("main");
                        if (mainMethod == null)
                                throw new Exception("No main method found in Main class");
                
                        env.PushScope(); // Create scope for main method
                        env.DefineVariable("self", this);
                        mainMethod.Execute(env);
                        env.PopScope();
                }

                return null;
        }
}