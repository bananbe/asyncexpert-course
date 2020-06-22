using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        private Dictionary<ulong, ulong> fibonacciCache;

        [GlobalSetup]
        public void Setup()
        {
            fibonacciCache = new Dictionary<ulong, ulong>();
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (fibonacciCache.ContainsKey(n)) return fibonacciCache[n];

            if (n == 1 || n == 2) return 1;

            fibonacciCache[n] = RecursiveWithMemoization(n - 2) + RecursiveWithMemoization(n - 1);
            return fibonacciCache[n];
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            ulong previous_previous, previous= 0, current = 1;
            
            for (ulong i = 1; i < n; i++)
            {
                previous_previous = previous;
                previous = current;
                current = previous_previous + previous;
            }

            return current;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
