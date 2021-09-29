/*
    Longest common subsequence: https://en.m.wikipedia.org/wiki/Longest_common_subsequence_problem

    常見的運用為diff tool，檔案比對時以word為單位，這個問題以letter為單位
 */
int commonChild(string s1, string s2)
{
    return FindLCS(s1, s2);
}

int FindLCS(string source, string target)
{
    int[,] matrix = new int[source.Length + 1, target.Length + 1];

    for (int i = 1; i <= source.Length; i++)
        for (int j = 1; j <= target.Length; j++)
        {
            if (source[i - 1] == target[j - 1])
            {
                matrix[i, j] = matrix[i - 1, j - 1] + 1;
            }
            else
            {
                matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
            }
        }

    PrintLCS(matrix, source);

    if (source.Length <= 80)
    {
        PrintMatrix(matrix, source, target);
    }

    return matrix[source.Length, target.Length];
}

void PrintLCS(int[,] matrix, string source)
{
    List<char> temp = new List<char>();

    int i = matrix.GetLength(0) - 1;
    int j = matrix.GetLength(0) - 1;
    while (i > 0 && j > 0)
    {
        if (matrix[i - 1, j] > matrix[i, j - 1])
        {
            i--;
        }
        else if (matrix[i - 1, j] < matrix[i, j - 1])
        {
            j--;
        }
        else
        {
            if(matrix[i, j] > matrix[i - 1, j - 1])
            {
                temp.Add(source[i - 1]);
            }
            
            i--;
            j--;
        }
    }

    temp.Reverse();
    Console.WriteLine($"diff: {string.Join("", temp)} | length: {temp.Count}");
}

void PrintMatrix(int[,] matrix, string source, string target)
{
    Console.WriteLine("========== matrix ==========");
    Console.Write("   0");

    foreach (char c in target)
    {
        Console.Write($"  {c}");
    }
    Console.WriteLine();

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        if(i == 0)
        {
            Console.Write("0");
        }
        else
        {
            Console.Write(source[i-1]);
        }

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write($" {matrix[i,j]:00}");
        }

        Console.WriteLine();
    }

    Console.WriteLine("========== matrix ==========");    
}

string[][] inputs = new[] {
    new[] {
        "HARRY",
        "SALLY"
    },
    new[] {
        "SHINCHAN",
        "NOHARAAA"
    },
    new[] {
        "UBBJXJGKLXGXTFBJYNLHQPULFILXLMPDQFWIYVBR",
        "WZFPTGLCXKNHAKSFEIXYOHTDCCSDAKYASOBHHDTB"
    }
};

foreach (string[] input in inputs)
{
    Console.WriteLine($"input 1: {input[0]}");
    Console.WriteLine($"input 2: {input[1]}");

    int result = commonChild(input[0], input[1]);

    Console.WriteLine($"result: {result}\n----------");
}
