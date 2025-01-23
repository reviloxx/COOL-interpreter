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

    [Test]
    public void Case()
    {
        var file = GetFile("case.cl");
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
    
    [Test]
    [TestCase("arith.cl")]
    [TestCase("assignment-val.cl")]
    [TestCase("assignseq.cl")]
    [TestCase("atoi_EDITED.cl")]
    [TestCase("bigexample.cl")]
    [TestCase("book_list.cl")]
    [TestCase("bool.cl")]
    [TestCase("c.cl")]
    [TestCase("cells.cl")]
    [TestCase("classonefield_EDITED.cl")]
    [TestCase("complex.cl")]
    [TestCase("copy-self-dispatch.cl")]
    [TestCase("dispatch-override-static.cl")]
    [TestCase("example_coolmanual_pag7.cl")]
    [TestCase("example_coolmanual_pag9_EDITED.cl")]
    [TestCase("good_EDITED.cl")]
    [TestCase("graph.cl")]
    [TestCase("hello_world.cl")]
    [TestCase("init-default.cl")]
    [TestCase("init-order-super.cl")]
    [TestCase("io.cl")]
    [TestCase("lam.cl")]
    [TestCase("lam-gc.cl")]
    [TestCase("life.cl")]
    [TestCase("list.cl")]
    [TestCase("multiple-static-dispatch.cl")]
    [TestCase("new-st.cl")]
    [TestCase("objectequality.cl")]
    [TestCase("override.cl")]
    [TestCase("override-basic.cl")]
    [TestCase("recclass.cl")]
    [TestCase("scoping.cl")]
    [TestCase("selftypeattribute.cl")]
    [TestCase("shadow-attr-case_EDITED.cl")]
    [TestCase("sort_list.cl")]
    [TestCase("test.cl")]
    public void Algorithm(string testCase)
    {
        var file = FileHelper.GetFile(TestType.AlgorithmSuccess, testCase);
        var context = GetProgramContext(file);        

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });
    }

    [Test]
    [TestCase("abort.cl")]
    [TestCase("addedlet.cl")]
    [TestCase("arith.cl")]
    [TestCase("arithprecedence.cl")]
    [TestCase("assigngetstype.cl")]
    [TestCase("assignment-val.cl")]
    [TestCase("assignment.cl")]
    [TestCase("assignseq.cl")]
    [TestCase("associativity+.cl")]
    [TestCase("associativity-times.cl")]
    [TestCase("associativity.cl")]
    [TestCase("associativitydiv.cl")]
    [TestCase("atoi.cl")]
    [TestCase("atoi_test.cl")]
    [TestCase("basic-init.cl")]
    [TestCase("basicequality.cl")]
    [TestCase("bigexample.cl")]
    [TestCase("bigexpr.cl")]
    [TestCase("binary_tree.cl")]
    [TestCase("book_list.cl")]
    [TestCase("bool.cl")]
    [TestCase("c.cl")]
    [TestCase("calls.cl")]
    [TestCase("case-none.cl")]
    [TestCase("case-order.cl")]
    [TestCase("casemultiplebranch.cl")]
    [TestCase("casevoid.cl")]
    [TestCase("cells.cl")]
    [TestCase("classmethodonearg.cl")]
    [TestCase("classonefield.cl")]
    [TestCase("classtwofields.cl")]
    [TestCase("comparisons-assoc.cl")]
    [TestCase("complex.cl")]
    [TestCase("cool.cl")]
    [TestCase("copy-self-dispatch.cl")]
    [TestCase("copy-self-init.cl")]
    [TestCase("dispatch-override-dynamic.cl")]
    [TestCase("dispatch-override-static.cl")]
    [TestCase("dispatch-void-dynamic.cl")]
    [TestCase("dispatch-void-static.cl")]
    [TestCase("dispatcharglist.cl")]
    [TestCase("dispatchnoargs.cl")]
    [TestCase("dispatchonearg.cl")]
    [TestCase("dispatchvoidlet.cl")]
    [TestCase("equalsassociativity.cl")]
    [TestCase("eval-order-args.cl")]
    [TestCase("eval-order-arith.cl")]
    [TestCase("eval-order-self.cl")]
    [TestCase("example_coolmanual_pag5.cl")]
    [TestCase("example_coolmanual_pag7.cl")]
    [TestCase("example_coolmanual_pag9.cl")]
    [TestCase("exp.cl")]
    [TestCase("fact.cl")]
    [TestCase("fibo.cl")]
    [TestCase("formallists.cl")]
    [TestCase("good.cl")]
    [TestCase("graph.cl")]
    [TestCase("hairyscary.cl")]
    [TestCase("hello_world.cl")]
    [TestCase("ifexpressionblock.cl")]
    [TestCase("ifnested.cl")]
    [TestCase("init-default.cl")]
    [TestCase("init-order-self.cl")]
    [TestCase("init-order-super.cl")]
    [TestCase("interaction-attrinit-method.cl")]
    [TestCase("io.cl")]
    [TestCase("lam-gc.cl")]
    [TestCase("lam.cl")]
    [TestCase("let-nested.cl")]
    [TestCase("letassociativity.cl")]
    [TestCase("letinit.cl")]
    [TestCase("letinitmultiplebindings.cl")]
    [TestCase("letnoinit.cl")]
    [TestCase("letparens.cl")]
    [TestCase("life.cl")]
    [TestCase("list.cl")]
    [TestCase("lteassociativity.cl")]
    [TestCase("many_objects_on_heap.cl")]
    [TestCase("mod-param.cl")]
    [TestCase("multiple-dispatch.cl")]
    [TestCase("multiple-static-dispatch.cl")]
    [TestCase("multipleatdispatches.cl")]
    [TestCase("multipleattributes.cl")]
    [TestCase("multipledispatches.cl")]
    [TestCase("nested-arith.cl")]
    [TestCase("nestedblocks.cl")]
    [TestCase("nestedlet.cl")]
    [TestCase("new-self-dispatch.cl")]
    [TestCase("new-self-init.cl")]
    [TestCase("new-st.cl")]
    [TestCase("newbasic.cl")]
    [TestCase("new_complex.cl")]
    [TestCase("not.cl")]
    [TestCase("objectequality.cl")]
    [TestCase("override-basic.cl")]
    [TestCase("override.cl")]
    [TestCase("palindrome.cl")]
    [TestCase("primes.cl")]
    [TestCase("recclass.cl")]
    [TestCase("scoping.cl")]
    [TestCase("selftypeattribute.cl")]
    [TestCase("sequence.cl")]
    [TestCase("shadow-attr-case.cl")]
    [TestCase("shadow-attr-formal.cl")]
    [TestCase("shadow-attr-let.cl")]
    [TestCase("shadow-case-let.cl")]
    [TestCase("shadow-formal-case.cl")]
    [TestCase("shadow-formal-let.cl")]
    [TestCase("shadow-let-case.cl")]
    [TestCase("shadow-let-let.cl")]
    [TestCase("simple-gc.cl")]
    [TestCase("sort_list.cl")]
    [TestCase("staticdispatchnoargs.cl")]
    [TestCase("string-methods.cl")]
    [TestCase("test.cl")]
    [TestCase("typename.cl")]
    [TestCase("unaryassociativity.cl")]
    [TestCase("while-val.cl")]
    [TestCase("whileexpressionblock.cl")]
    [TestCase("whileOK.cl")]
    [TestCase("whileoneexpression.cl")]
    public void Parsing(string testCase)
    {
        var file = FileHelper.GetFile(TestType.ParsingSuccess, testCase);
        var context = GetProgramContext(file);

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });
    }

    [Test]
    [TestCase("abort.cl")]
    [TestCase("addedlet.cl")]
    [TestCase("all-types-builtin.cl")]
    [TestCase("arith.cl")]
    [TestCase("assigngetstype.cl")]
    [TestCase("assignment-val.cl")]
    [TestCase("assignment.cl")]
    [TestCase("assignseq.cl")]
    [TestCase("associativity+.cl")]
    [TestCase("associativity-times.cl")]
    [TestCase("associativity.cl")]
    [TestCase("associativitydiv.cl")]
    [TestCase("atoi.cl")]
    [TestCase("basic-init.cl")]
    [TestCase("basicequality.cl")]
    [TestCase("bigexample.cl")]
    [TestCase("bigexpr.cl")]
    [TestCase("book_list.cl")]
    [TestCase("bool.cl")]
    [TestCase("c.cl")]
    [TestCase("calls.cl")]
    [TestCase("case-order.cl")]
    [TestCase("casemultiplebranch.cl")]
    [TestCase("casevoid.cl")]
    [TestCase("cells.cl")]
    [TestCase("classmethodonearg.cl")]
    [TestCase("classonefield.cl")]
    [TestCase("classtwofields.cl")]
    [TestCase("comparisons-assoc.cl")]
    [TestCase("cool.cl")]
    [TestCase("copy-self-dispatch.cl")]
    [TestCase("copy-self-init.cl")]
    [TestCase("dispatch-override-dynamic.cl")]
    [TestCase("dispatch-override-static.cl")]
    [TestCase("dispatch-void-dynamic.cl")]
    [TestCase("dispatch-void-static.cl")]
    [TestCase("dispatcharglist.cl")]
    [TestCase("dispatchnoargs.cl")]
    [TestCase("dispatchvoidlet.cl")]
    [TestCase("eval-order-args.cl")]
    [TestCase("eval-order-arith.cl")]
    [TestCase("eval-order-self.cl")]
    [TestCase("example_coolmanual_pag5.cl")]
    [TestCase("example_coolmanual_pag9.cl")]
    [TestCase("exp.cl")]
    [TestCase("fact.cl")]
    [TestCase("fibo.cl")]
    [TestCase("fibo2.cl")]
    [TestCase("good.cl")]
    [TestCase("graph.cl")]
    [TestCase("hairyscary.cl")]
    [TestCase("hello_world.cl")]
    [TestCase("ifexpressionblock.cl")]
    [TestCase("ifnested.cl")]
    [TestCase("init-default.cl")]
    [TestCase("init-order-self.cl")]
    [TestCase("init-order-super.cl")]
    [TestCase("interaction-attrinit-method.cl")]
    [TestCase("io.cl")]
    [TestCase("let-nested.cl")]
    [TestCase("let-scope-variable.cl")]
    [TestCase("letassociativity.cl")]
    [TestCase("letinit.cl")]
    [TestCase("letinitmultiplebindings.cl")]
    [TestCase("letnoinit.cl")]
    [TestCase("letparens.cl")]
    [TestCase("life.cl")]
    [TestCase("list.cl")]
    [TestCase("many_objects_on_heap.cl")]
    [TestCase("mod-param.cl")]
    [TestCase("multiple-dispatch.cl")]
    [TestCase("nested-arith.cl")]
    [TestCase("nestedblocks.cl")]
    [TestCase("nestedlet.cl")]
    [TestCase("new-self-dispatch.cl")]
    [TestCase("new-self-init.cl")]
    [TestCase("new-st.cl")]
    [TestCase("newbasic.cl")]
    [TestCase("not.cl")]
    [TestCase("object-returned.cl")]
    [TestCase("override-basic.cl")]
    [TestCase("override.cl")]
    [TestCase("palindrome.cl")]
    [TestCase("primes.cl")]
    [TestCase("print_string.cl")]
    [TestCase("scoping.cl")]
    [TestCase("selftypeattribute.cl")]
    [TestCase("sequence.cl")]
    [TestCase("shadow-attr-case.cl")]
    [TestCase("shadow-attr-formal.cl")]
    [TestCase("shadow-attr-let.cl")]
    [TestCase("shadow-case-let.cl")]
    [TestCase("shadow-formal-case.cl")]
    [TestCase("shadow-formal-let.cl")]
    [TestCase("shadow-let-case.cl")]
    [TestCase("shadow-let-let.cl")]
    [TestCase("simple-gc.cl")]
    [TestCase("sort_list.cl")]
    [TestCase("staticdispatchnoargs.cl")]
    [TestCase("stdio.h.cl")]
    [TestCase("string-methods.cl")]
    [TestCase("test.cl")]
    [TestCase("testing_self-assign.cl")]
    [TestCase("testing_self.cl")]
    [TestCase("unaryassociativity.cl")]
    [TestCase("variable-without-right-assign.cl")]
    [TestCase("void-not-call-function.cl")]
    [TestCase("while-val.cl")]
    [TestCase("whileexpressionblock.cl")]
    [TestCase("whileOK.cl")]
    [TestCase("whileoneexpression.cl")]
    public void Semantics(string testCase)
    {
        var file = FileHelper.GetFile(TestType.SemanticsSuccess, testCase);
        var context = GetProgramContext(file);

        Assert.DoesNotThrow(() =>
        {
            var rootNode = GetRootNode(context);
            Execute(rootNode);
        });
    }
}