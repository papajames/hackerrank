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

class Result
{

    /*
     * Complete the 'isBalanced' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string isBalanced(string s)
    {
        var openBrackets = new[] { '(', '{', '[' };
        var closeBrackets = new[] { ')', '}', ']' };

        var stack = new Stack<char>();
        foreach (var @char in s)
        {
            if (openBrackets.Contains(@char))
            {
                stack.Push(@char);
            }
            else
            {
                var indexOfClose = Array.IndexOf(closeBrackets, @char);
                var openBracket = openBrackets[indexOfClose];
                if (!stack.Any() || openBracket != stack.Peek())
                {
                    return "NO";
                }

                stack.Pop();
            }
        }

        return "YES";
    }

}

var inputs = new[]{
    "./balanced-brackets.input09.txt",
};
var outputs = new[]{
    "./balanced-brackets.output09.txt",
};

for(var i = 0; i < inputs.Length; i++)
{
    var input = inputs[i];
    var output = outputs[i];

    var readerIn = new StreamReader(input);
    var readerOut = new StreamReader(output);

    Console.WriteLine($"Start test case: {input}");
    Stopwatch sw = Stopwatch.StartNew();

    int t = Convert.ToInt32(readerIn.ReadLine().Trim());

    for (int tItr = 0; tItr < t; tItr++)
    {
        string sin = readerIn.ReadLine();
        string sout = readerOut.ReadLine();

        string result = Result.isBalanced(sin);

        Console.WriteLine($"({tItr+1})[{sout == result}]expected: {sout}, actual: {result}");
    }

    sw.Stop();
    Console.WriteLine($"Execution time: {sw.Elapsed}");
    Console.WriteLine("".PadLeft(80, '='));

    readerIn.Dispose();
    readerOut.Dispose();
}
