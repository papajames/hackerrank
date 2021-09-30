#r "System.Runtime"

/*
    Merge Sort說明：https://www.youtube.com/watch?v=JSceec-wEyw
    Merge Sort實作：https://www.geeksforgeeks.org/merge-sort/

    這個實作是把 count 以 by reference 傳入 method
    直接修改原始陣列實作：https://www.hackerrank.com/challenges/ctci-merge-sort/forum/comments/194889
 */

using System.Diagnostics;

long countInversions(List<int> arr)
{
    long count = 0;
    int[] result = MergeSort(arr.ToArray(), 0, arr.Count - 1, ref count);
    Console.WriteLine($"{string.Join(",", result)}");

    return count;
}

int[] MergeSort(int[] array, int left, int right, ref long count)
{
    if (left < right)
    {
        int mid = (left + right) / 2;
        int[] leftArray = MergeSort(array, left, mid, ref count);
        int[] rightArray = MergeSort(array, mid + 1, right, ref count);
        return Merge(leftArray, rightArray, ref count);
    }
    else
    {
        return new int[1] { array[left] };
    }
}

int[] Merge(int[] left, int[] right, ref long count)
{
    int[] temp = new int[left.Length + right.Length];

    int lindex = 0;
    int rindex = 0;
    while (lindex < left.Length || rindex < right.Length)
    {
        int tindex = lindex + rindex;
        if (lindex == left.Length)
        {
            temp[tindex] = right[rindex];
            rindex++;
        }
        else if (rindex == right.Length)
        {
            temp[tindex] = left[lindex];
            lindex++;
        }
        else if (left[lindex] <= right[rindex])
        {
            temp[tindex] = left[lindex];
            lindex++;
        }
        else
        {
            temp[tindex] = right[rindex];
            count += left.Length + rindex - tindex;

            rindex++;
        }
    }

    Console.WriteLine($"count: {count}, left: {string.Join(",", left)}, right: {string.Join(",", right)} temp: {string.Join(",", temp)}");
    return temp;
}

Stopwatch sw = Stopwatch.StartNew();

string[] inputs = new[] {
    "2 1 3 1 2",
    "1 5 3 7",
    "7 5 3 1"
};

foreach (var input in inputs)
{
    List<int> arr = input.TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();
    Console.WriteLine($"input: {input}");
    long result = countInversions(arr);
    Console.WriteLine(result);
}

sw.Stop();
Console.WriteLine(sw.Elapsed);