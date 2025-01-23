using System.Text;

namespace UnitTests;

public class StringWriterMock : TextWriter
{
    public override Encoding Encoding { get; }
    public List<string> WrittenLines = [];

    public override void WriteLine(string? value)
    {
        if (value == null) return;

        WrittenLines.Add(value);
    }

    public override void Write(string? value)
    {
        if (value == null) return;

        WrittenLines.Add(value);
    }

    public void ClearWrittenLines() => WrittenLines = [];
}
