using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the maxSubsetSum function below.
    public static int maxSubsetSum(int[] arr)
    {
        if (arr.Length == 0)
        {
            return 0;
        }
        else if (arr.Length == 1)
        {
            return arr[0];
        }
        else if (arr.Length == 2)
        {
            return Math.Max(arr[0], arr[1]);
        }

        int[] prefix = new int[arr.Length];
        prefix[0] = arr[0];
        prefix[1] = Math.Max(arr[0], arr[1]);

        int currentMax = prefix[1];
        for (int i = 2; i < arr.Length; i++)
        {
            currentMax = Math.Max(prefix[i - 2] + arr[i], currentMax);
            currentMax = Math.Max(arr[i], currentMax);
            prefix[i] = currentMax;
            Console.WriteLine($"i: {i}, prefix[i-2]: {prefix[i - 2]}, arr[i]: {arr[i]}, currentMax: {currentMax}");
        }

        return Math.Max(prefix[prefix.Length - 1], prefix[prefix.Length - 2]);
    }
}

string[] inputs = new[]{
    "./dynamic-programming/max-array-sum.input09.txt",
};

foreach (var input in inputs)
{
    var reader = new StreamReader(input);

    Console.WriteLine($"Start test case: {input}");
    Stopwatch sw = Stopwatch.StartNew();

    int n = Convert.ToInt32(reader.ReadLine());

    int[] arr = Array.ConvertAll(reader.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
    ;
    int res = Solution.maxSubsetSum(arr);
    Console.WriteLine($"result: {res}");

    sw.Stop();
    Console.WriteLine($"Execution time: {sw.Elapsed}");
    Console.WriteLine("".PadLeft(80, '='));

    reader.Dispose();
}