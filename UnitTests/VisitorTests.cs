using UnitTests;

namespace CoolGrammarInterpretorTests;

public class VisitorTests : VisitorTestsBase
{
    private StringWriterMock _stringWriterMock;

    [SetUp]
    public void Initialize()
    {
        _stringWriterMock = new StringWriterMock();
        Console.SetOut(_stringWriterMock);
    }

    [Test]
    public void Modulo()
    {
        var file = GetFile("modulo.cl");
        var context = GetProgramContext(file);

        List<string> expectedOutput =
        [
            "is math.modulo(4, 2) = 0?",
            "True",
            "is math.modulo(4, 3) = 0?",
            "False"
        ];

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });

        Assert.That(ValidateOutput(expectedOutput), Is.True);
        _stringWriterMock.ClearWrittenLines();
    }

    [Test]
    public void While()
    {
        var file = GetFile("while.cl");
        var context = GetProgramContext(file);

        List<string> expectedOutput =
        [
            "0",
            "1",
            "2",
            "3",
            "4"
        ];

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });

        Assert.That(ValidateOutput(expectedOutput), Is.True);
        _stringWriterMock.ClearWrittenLines();
    }

    private string GetFile(string testName)
    {
        var file = Path.Combine(Environment.CurrentDirectory, "TestCases", testName);

        if (!File.Exists(file))
            throw new FileNotFoundException("Test case not found!", file);

        return file;
    }

    private bool ValidateOutput(List<string> expected)
    {
        return _stringWriterMock.WrittenLines.SequenceEqual(expected);
    }
}