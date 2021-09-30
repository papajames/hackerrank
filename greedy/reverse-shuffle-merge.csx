/*
    邏輯整理：
        1. 記錄字串裡所有字元出現的數量
        2. 反向對輸入字串做處理
        3. 字元的數量一定是雙數（符合題目的Split要求）
        4. 判斷字元可用數量
            4.1 當字元的可用數量已經小於總數量一半時，直接捨棄
            4.2 因字典序要求，當必要的字元大於或等於前一個必要字元時，可直接加至結果字串
            4.3 如必要字元小於前一個必要字元時，做下列判斷
                4.3.1 前一個字元可用數量還在總數量一半以上時，移除
                4.3.2 反之，保留
                4.3.3 最後，加至結果字串
 */
string reverseShuffleMerge(string s)
{
    int[] charRemaining = CountChars(s);
    int[] charLimits = charRemaining.Select(x => x / 2).ToArray();
    int[] charUsed = new int[26];

    Stack<char> charStack = new Stack<char>();
    for (int i = s.Length - 1; i >= 0; i--)
    {
        char current = s[i];
        int currentIndex = current - 'a';
        if (charStack.Count == 0)
        {
            charStack.Push(current);
            charRemaining[currentIndex]--;
            charUsed[currentIndex]++;
            continue;
        }

        if (charUsed[currentIndex] >= charLimits[currentIndex])
        {
            charRemaining[currentIndex]--;
            continue;
        }

        while (true)
        {
            if (charStack.Count == 0)
            {
                break;
            }

            char prev = charStack.Peek();
            int prevIdx = prev - 'a';
            if (current >= prev ||
                charUsed[prevIdx] - 1 + charRemaining[prevIdx] < charLimits[prevIdx])
            {
                break;
            }

            charStack.Pop();
            charUsed[prevIdx]--;
        }

        charStack.Push(current);
        charRemaining[currentIndex]--;
        charUsed[currentIndex]++;
    }

    char[] result = charStack.ToArray(); // reversed
    Array.Reverse(result); // normal
    return new string(result);
}

int[] CountChars(string s)
{
    int[] count = new int[26];
    foreach (char c in s)
    {
        count[c - 'a']++;
    }

    return count;
}

string[] inputs = new[]{
    "ggggaabb",
    "eggegg",
    "abcdefgabcdefg",
    "aeiouuoiea"
};

foreach(string input in inputs)
{
    Console.WriteLine($"input: {input}");
    string result = reverseShuffleMerge(input);
    Console.WriteLine($"result: {result}");
    Console.WriteLine("".PadLeft(80, '='));
}