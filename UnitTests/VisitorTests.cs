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
    public void Case()
    {
        var file = FileHelper.GetFile("case.cl");
        var context = GetProgramContext(file);

        List<string> expectedOutput =
        [
            "Value is five."
        ];

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });

        Assert.That(ValidateOutput(expectedOutput), Is.True);
    }


    
}