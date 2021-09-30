/*
    要注意 Edge Case：
        1. 字元組裡其中一個字元出現1次，其它字元N次且次數相同時 (aaabbbcccd)，移掉出現一次的字元仍視為合法字串
        2. 字元組裡其中一個字元出現1次，另一個字元出現N次且與其它字元差1 (aaabbccd)，需移掉一個以上的字元，視為不合法字串

    出現1次的字元和需要移除的字元需要分開處理，才能滿足上述的案例
 */
string isValid(string s)
{
    int[] counting = new int[26];
    foreach (char c in s)
    {
        counting[c - 'a']++;
    }

    Console.WriteLine($"{string.Join("|", counting)}");

    int deletions = 0;
    int singleCount = 0;
    int? previous = null;
    for (int i = 0; i < counting.Length; i++)
    {
        if (counting[i] == 0)
        {
            continue;
        }
        else if(counting[i] == 1)
        {
            singleCount++;
        }
        else if (previous == null)
        {
            previous = counting[i];
        }
        else
        {
            deletions += Math.Abs(counting[i] - previous.Value);
        }
        
        Console.WriteLine($"d: {deletions}, s: {singleCount}");
        if (deletions > 1 && singleCount != 1 || deletions + singleCount > 1)
        {
            return "NO";
        }
    }

    return "YES";
}

string[] inputs = new[] {
    "xxxaabbccrry",
    "aaaabbbbc",
    "aabbcd",
    "aabbccddeefghi",
    "abcdefghhgfedecba"
};

foreach (string input in inputs)
{
    Console.WriteLine($"input: {input}");

    string result = isValid(input);

    Console.WriteLine(result + "\n----------");
}