using static CoolGrammarParser;

namespace Cool.Interpreter;

public class CoolClass(string name, string? baseClass)
{
    public string Name { get; } = name;
    public string? BaseClass { get; } = baseClass;

    private Dictionary<string, MethodContext> _methods = [];

    private Dictionary<string, PropertyContext> _properties = [];

    public void AddMethod(string id, MethodContext context)
        => _methods.Add(id, context);

    public MethodContext? GetMethod(string id)
        => _methods[id];

    public void AddProperty(string id, PropertyContext context)
        => _properties.Add(id, context);

    public PropertyContext? GetProperty(string id)
        => _properties[id];
}
