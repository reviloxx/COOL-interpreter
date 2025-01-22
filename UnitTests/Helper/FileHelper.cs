namespace UnitTests.Helper;

public static class FileHelper
{
    public static string GetFile(TestType testType, string testName)
    {
        var file = testType switch
        {
            TestType.Algorithm => Path.Combine(Environment.CurrentDirectory, "TestCases", "Algorithm", testName),
            TestType.Parsing => Path.Combine(Environment.CurrentDirectory, "TestCases", "Parsing", testName),
            TestType.Semantics => Path.Combine(Environment.CurrentDirectory, "TestCases", "Semantics", testName),
            _ => throw new ArgumentException("Invalid testType!")
        };

        if (!File.Exists(file))
            throw new FileNotFoundException("Test case not found!", file);

        return file;
    }
}

public enum TestType
{
    Algorithm,
    Parsing,
    Semantics,
}
