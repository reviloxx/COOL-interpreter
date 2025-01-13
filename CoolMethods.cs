using static Cool.CoolGrammarListener;

namespace Cool;

public static class CoolMethods
{
    public static CoolMethod OutString => new ("out_string", ["str"], args => Console.Write(args[0]?.ToString()));
}
