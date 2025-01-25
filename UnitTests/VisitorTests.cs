using UnitTests;

namespace CoolGrammarInterpretorTests;

public class VisitorTests : VisitorTestsBase
{
    [Test]
    public void Modulo()
    {
        var file = FileHelper.GetFile("modulo.cl");
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
    }

    [Test]
    public void While()
    {
        var file = FileHelper.GetFile("while.cl");
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
    }

    [Test]
    public void CaseInt()
    {
        var file = FileHelper.GetFile("caseInt.cl");
        var context = GetProgramContext(file);

        List<string> expectedOutput =
        [
            "52"
        ];

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });

        Assert.That(ValidateOutput(expectedOutput), Is.True);
    }

    [Test]
    public void CaseString()
    {
        var file = FileHelper.GetFile("caseString.cl");
        var context = GetProgramContext(file);

        List<string> expectedOutput =
        [
            "Hello World!"
        ];

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });

        Assert.That(ValidateOutput(expectedOutput), Is.True);
    }
}