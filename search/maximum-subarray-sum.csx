/*
    思維整理：
        1. Subarray Sum可以使用Prefix來解決
            1.1 Prefix公式為Sn = I1 + I2 + ... + In
            1.2 有個陣列長度為10，要找出A[5]..A[9]的加總，公式為Prefix[9]-Prefix[4] -- Kanade's Algorithm
        2. 餘數原理：(0 <= n < m) -- https://en.wikipedia.org/wiki/Modular_arithmetic
        3. Sorted collection效能排名： SortedDictionary > SortedList > SortedSet
 */

class Result
{
    // Execution time: 00:00:00.7568243
    public static long maximumSum(List<long> a, long m)
    {
        // generate prefix
        var current = 0L;
        var dict = new SortedDictionary<long, long>();
        for (var i = 0; i < a.Count; i++)
        {
            current = (a[i] % m + current) % m;
            dict[current] = i;
        }

        // find max value from sorted keys
        var keys = dict.Keys.ToArray();
        var max = keys.Last();
        for (var i = 0; i < keys.Length - 1; i++)
        {
            // need check max sum
            // if current value generated later then next
            if (dict[keys[i]] > dict[keys[i + 1]])
            {
                max = Math.Max(max,
                    (keys[i] - keys[i + 1] + m) % m);
            }
        }

        return max;
    }
}

string[] inputs = new[]{
    "./maximum-subarray-sum.input00.txt",
    "./maximum-subarray-sum.input13.txt",
};

foreach (var input in inputs)
{
    var reader = new StreamReader(input);

    Console.WriteLine($"Start test case: {input}");
    Stopwatch sw = Stopwatch.StartNew();

    int q = Convert.ToInt32(reader.ReadLine().Trim());

    for (int qItr = 0; qItr < q; qItr++)
    {
        string[] firstMultipleInput = reader.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        long m = Convert.ToInt64(firstMultipleInput[1]);

        List<long> a = reader.ReadLine().TrimEnd().Split(' ').ToList().Select(aTemp => Convert.ToInt64(aTemp)).ToList();

        long result = Result.maximumSum(a, m);
        Console.WriteLine($"SD: result: {result}");
    }

    sw.Stop();
    Console.WriteLine($"Execution time: {sw.Elapsed}");
    Console.WriteLine("".PadLeft(80, '='));

    reader.Dispose();
}