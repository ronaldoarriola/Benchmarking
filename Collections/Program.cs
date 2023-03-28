using System.Buffers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Collections;

[MemoryDiagnoser]
public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Program>();
    }
    
    /*
|                  Method |         Mean |      Error |     StdDev |       Median |      Gen0 |      Gen1 |     Gen2 |  Allocated |
|------------------------ |-------------:|-----------:|-----------:|-------------:|----------:|----------:|---------:|-----------:|
|       InsertFirstInList | 9,563.702 ms | 57.4917 ms | 53.7777 ms | 9,543.386 ms |         - |         - |        - |  4200800 B |
| InsertFirstInLinkedList |    58.875 ms |  1.5755 ms |  4.6208 ms |    61.413 ms | 7250.0000 | 2250.0000 | 812.5000 | 24000726 B |
|  InsertFirstInArrayPool |     1.429 ms |  0.0014 ms |  0.0013 ms |     1.429 ms |         - |         - |        - |       75 B |
|        InsertLastInList |     1.881 ms |  0.0372 ms |  0.0348 ms |     1.883 ms |  998.0469 |  998.0469 | 998.0469 |  4195497 B |
|  InsertLastInLinkedList |    57.258 ms |  1.8138 ms |  5.3480 ms |    55.979 ms | 6562.5000 | 2250.0000 | 812.5000 | 24001052 B |
|   InsertLastInArrayPool |     1.432 ms |  0.0012 ms |  0.0011 ms |     1.432 ms |         - |         - |        - |       75 B |
     */

    [Benchmark]
    public void InsertFirstInList()
    {
        var list   = new List<int>();
        var random = new Random();

        for (var i = 0; i < 500_000; i++)
        {
            list.Insert(0, random.Next());
        }
    }

    [Benchmark]
    public void InsertFirstInLinkedList()
    {
        var list   = new LinkedList<int>();
        var random = new Random();

        for (var i = 0; i < 500_000; i++)
        {
            list.AddFirst(random.Next());
        }
    }

    [Benchmark]
    public void InsertFirstInArrayPool()
    {
        const int arrayLenght = 500_000;
        var       rentedArray = ArrayPool<int>.Shared.Rent(arrayLenght);
        try
        {
            var random = new Random();

            for (var i = arrayLenght - 1; i >= 0; i--)
            {
                rentedArray[i] = random.Next();
            }
        }
        finally
        {
            ArrayPool<int>.Shared.Return(rentedArray);
        }
    }

    [Benchmark]
    public void InsertLastInList()
    {
        var list   = new List<int>();
        var random = new Random();

        for (var i = 0; i < 500_000; i++)
        {
            list.Add(random.Next());
        }
    }


    [Benchmark]
    public void InsertLastInLinkedList()
    {
        var list   = new LinkedList<int>();
        var random = new Random();

        for (var i = 0; i < 500_000; i++)
        {
            list.AddLast(random.Next());
        }
    }


    [Benchmark]
    public void InsertLastInArrayPool()
    {
        const int arrayLenght = 500_000;
        var       rentedArray = ArrayPool<int>.Shared.Rent(arrayLenght);
        try
        {
            var random = new Random();

            for (var i = 0; i < arrayLenght; i++)
            {
                rentedArray[i] = random.Next();
            }
        }
        finally
        {
            ArrayPool<int>.Shared.Return(rentedArray);
        }
    }
}