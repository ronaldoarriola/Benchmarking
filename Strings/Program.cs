using System.Text;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace Strings;

[MemoryDiagnoser]
public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Program>();
    }

    [Benchmark]
    public void StringConcat()
    {
        var numbers = Enumerable.Range(0, 100_000).ToArray();
        var text    = string.Empty;
        foreach (var number in numbers)
        {
            text += number.ToString();
        }

        // Console.WriteLine(text);
    }

    [Benchmark]
    public void StringBuilderAppend()
    {
        var numbers       = Enumerable.Range(0, 100_000).ToArray();
        var stringBuilder = new StringBuilder();
        foreach (var number in numbers)
        {
            stringBuilder.Append(numbers);
        }

        // Console.WriteLine(stringBuilder.ToString());
    }
}