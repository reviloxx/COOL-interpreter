namespace UnitTests;

public static class FileHelper
{
    public static string GetFileMoodle(TestType testType, string testName)
    {
        var file = testType switch
        {
            TestType.AlgorithmSuccess => Path.Combine(Environment.CurrentDirectory, "TestCases", "Algorithm", "success", testName),
            TestType.AlgorithmFail => Path.Combine(Environment.CurrentDirectory, "TestCases", "Algorithm", "fail", testName),
            TestType.ParsingSuccess => Path.Combine(Environment.CurrentDirectory, "TestCases", "Parsing", "success", testName),
            TestType.ParsingFail => Path.Combine(Environment.CurrentDirectory, "TestCases", "Parsing", "fail", testName),
            TestType.SemanticsSuccess => Path.Combine(Environment.CurrentDirectory, "TestCases", "Semantics", "success", testName),
            TestType.SemanticsFail => Path.Combine(Environment.CurrentDirectory, "TestCases", "Semantics", "fail", testName),
            _ => throw new ArgumentException("Invalid testType!")
        };

        if (!File.Exists(file))
            throw new FileNotFoundException("Test case not found!", file);

        return file;
    }

    public static string GetFile(string testName)
    {
        var file = Path.Combine(Environment.CurrentDirectory, "TestCases", testName);

        if (!File.Exists(file))
            throw new FileNotFoundException("Test case not found!", file);

        return file;
    }

}


public enum TestType
{
    AlgorithmSuccess,
    AlgorithmFail,
    ParsingSuccess,
    ParsingFail,
    SemanticsSuccess,
    SemanticsFail
}
