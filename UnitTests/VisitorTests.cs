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
    public void Success(string testCase)
    {
        var file = GetFile(TestType.AlgorithmSuccess, testCase);
        var context = GetProgramContext(file);

        Assert.DoesNotThrow(() => StartVisitor(context));
    }

    [Test]
    [TestCase("hairyscary.cl")]
    public void Fail(string testCase)
    {
        var file = GetFile(TestType.AlgorithmFail, testCase);
        var context = GetProgramContext(file);

        Assert.Throws<Exception>(() => StartVisitor(context));
    }
}

public class ParsingTests : VisitorTestsBase
{
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
    public void Success(string testCase)
    {
        var file = FileHelper.GetFile(TestType.ParsingSuccess, testCase);
        var context = GetProgramContext(file);

        Assert.DoesNotThrow(() => StartVisitor(context));
    }

    [Test]
    [TestCase("all_else_true.cl")]
    [TestCase("attrcapitalname.cl")]
    [TestCase("bad.cl")]
    [TestCase("badblock.cl")]
    [TestCase("baddispatch1.cl")]
    [TestCase("baddispatch2.cl")]
    [TestCase("baddispatch3.cl")]
    [TestCase("baddispatch4.cl")]
    [TestCase("badexprlist.cl")]
    [TestCase("badfeaturenames.cl")]
    [TestCase("badfeatures.cl")]
    [TestCase("casenoexpr.cl")]
    [TestCase("classbadinherits.cl")]
    [TestCase("classbadname.cl")]
    [TestCase("classnoname.cl")]
    [TestCase("emptyassign.cl")]
    [TestCase("emptymethodbody.cl")]
    [TestCase("emptyprogram.cl")]
    [TestCase("emptystaticdispatch.cl")]
    [TestCase("extrasemicolonblock.cl")]
    [TestCase("firstbindingerrored.cl")]
    [TestCase("firstclasserrored.cl")]
    [TestCase("ifnoelse.cl")]
    [TestCase("ifnoelsebranch.cl")]
    [TestCase("ifnofi.cl")]
    [TestCase("ifnothenbranch.cl")]
    [TestCase("isvoidbadtype.cl")]
    [TestCase("multipleclasses.cl")]
    [TestCase("multiplemethoderrors.cl")]
    [TestCase("newbadtype.cl")]
    [TestCase("only_one_comment.cl")]
    [TestCase("returntypebad.cl")]
    [TestCase("secondbindingerrored.cl")]
    [TestCase("test.3.cl")]
    [TestCase("whilebad.cl")]
    public void Fail(string testCase)
    {
        var file = GetFile(TestType.ParsingFail, testCase);
        var context = GetProgramContext(file);

        Assert.Throws<Exception>(() => StartVisitor(context));
    }
}

public class SemanticsTests : VisitorTestsBase
{
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
    public void Success(string testCase)
    {
        var file = GetFile(TestType.SemanticsSuccess, testCase);
        var context = GetProgramContext(file);

        Assert.DoesNotThrow(() => StartVisitor(context));
    }

    [Test]
    [TestCase("addedlet-int+string.cl")]
    [TestCase("addedlet-string+string.cl")]
    [TestCase("all-types-builtin-and-complex.cl")]
    [TestCase("arithprecedence.cl")]
    [TestCase("associativity+.cl")]
    [TestCase("associativity-times.cl")]
    [TestCase("associativity.cl")]
    [TestCase("associativitydiv.cl")]
    [TestCase("comparisons-assoc.cl")]
    [TestCase("dispatchonearg.cl")]
    [TestCase("equalsassociativity.cl")]
    [TestCase("error-out_string-param.cl")]
    [TestCase("example_coolmanual_pag7.cl")]
    [TestCase("inherit-bool.cl")]
    [TestCase("inherit-int.cl")]
    [TestCase("inherit-string.cl")]
    [TestCase("letinitmultiplebindings.cl")]
    [TestCase("lteassociativity.cl")]
    [TestCase("multipleattributes.cl")]
    [TestCase("recclass.cl")]
    [TestCase("testing_self.cl")]
    [TestCase("type-final-inherit-string.cl")]
    [TestCase("type-intt-not-exist.cl")]
    [TestCase("variable-in-parent-class.cl")]
    [TestCase("while-not-return-Object.cl")]
    [TestCase("whileoneexpression-type-object.cl")]
    public void Fail(string testCase)
    {
        var file = GetFile(TestType.SemanticsFail, testCase);
        var context = GetProgramContext(file);

        Assert.Throws<Exception>(() => StartVisitor(context));
    }
}