/*
    思維
        1. 一個字元也合法，先次數加1再進行後續處理
        2. 合法的字串，第一個規則為所有字元需一樣，所以先找出最多的連續相同字元的字串，並且每多一個字元時次數加1
        3. 合法的字串，另一個規則為所有字元需一樣除了中間那個，所以把第2點找到的字串，長度乘2再加1，如果符合規則時次數加1
 */
static long substrCount(int n, string s)
{
    long count = 0;
    for (int i = 0; i < s.Length; i++)
    {
        // length 1 always counts 1
        int length = 1;
        Console.WriteLine(s.Substring(i, length));
        count++;

        while (i + length + 1 <= s.Length
            && IsAllTheSame(s.Substring(i, length + 1)))
        {
            Console.WriteLine(s.Substring(i, length + 1));

            count++;
            length++;
        }

        if (i + (length * 2 + 1) <= s.Length
            && IsAllTheSameExceptMiddle(s.Substring(i, length * 2 + 1)))
        {
            Console.WriteLine(s.Substring(i, length * 2 + 1));

            count++;
        }
    }

    return count;
}

static bool IsAllTheSame(string substring)
{
    char? first = null;
    foreach (char c in substring)
    {
        if (first == null)
        {
            first = c;
        }
        else
        {
            if (first != c)
            {
                return false;
            }
        }
    }

    return true;
}

static bool IsAllTheSameExceptMiddle(string substring)
{
    int mid = substring.Length / 2; // zero base, no plus one required
    char? first = null;
    for (int i = 0; i < substring.Length; i++)
    {
        if (i == mid)
        {
            continue;
        }

        if (first == null)
        {
            first = substring[i];
        }
        else
        {
            if (first != substring[i])
            {
                return false;
            }
        }
    }

    return true;
}

string[] inputs = new[]{
    "asasd",
    "abcbaba",
    "aaaa"
};

foreach (string input in inputs)
{
    Console.WriteLine($"input: {input}");
    long result = substrCount(input.Length, input);
    Console.WriteLine($"result: {result}\n----------");
}