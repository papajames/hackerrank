/*
    備註：
        雖然Sample Test Cases裡的數例都是由小到大，然後都是R的N次方
        但在Constraints裡沒有保證這件事情

        所以在統計數字數量時，不要用平方根做為Key，而是原始的數字
        再進行組合計算時，再用n, n*r, n*r*r來尋找對映的數字

    v1:
        為資料由小到大排序時會正確，但資料為亂序時則會出錯
 */
long countTriplets_v2(List<long> arr, long r)
{
    long triplets = 0;
    Dictionary<long, long> dict = new Dictionary<long, long>();
    Dictionary<long, long> pairs = new Dictionary<long, long>();

    for (int i = arr.Count - 1; i >= 0; i--)
    {
        long vi = arr[i];
        long vj = vi * r;
        if (pairs.ContainsKey(vj))
        {
            triplets += pairs[vj];
        }

        if(dict.ContainsKey(vj))
        {
            if(pairs.ContainsKey(vi))
            {
                pairs[vi] += dict[vj];
            }
            else
            {
                pairs.Add(vi, dict[vj]);
            }
        }

        if (dict.ContainsKey(vi))
        {
            dict[vi]++;
        }
        else
        {
            dict.Add(vi, 1);
        }
    }

    return triplets;
}

long countTriplets_v1(List<long> arr, long r)
{
    long triplets = 0;

    Dictionary<long, long> count = new Dictionary<long, long>();
    foreach (long v in arr)
    {
        if (count.ContainsKey(v))
        {
            count[v]++;
        }
        else
        {
            count.Add(v, 1);
        }
    }

    foreach (long i in count.Keys)
    {
        long j = i * r;
        long k = j * r;
        if (count.ContainsKey(j) && count.ContainsKey(k))
        {
            Console.WriteLine($"(i,j,k) = ({i},{j},{k}) | [i,j,k] = [{count[i]},{count[j]},{count[k]}]");
            triplets += count[i] * count[j] * count[k];
        }
    }

    return triplets;
}

long countTriplets_Reverse(List<long> arr, long r)
{
    long count = 0;
    Dictionary<long, long> dict = new Dictionary<long, long>();
    Dictionary<long, long> pairs = new Dictionary<long, long>();

    arr.Reverse();
    foreach (long item in arr)
    {
        if (pairs.ContainsKey(item * r))
        {
            count += pairs[item * r];
        }

        if (dict.ContainsKey(item * r))
        {
            if (pairs.ContainsKey(item))
            {
                pairs[item] += dict[item * r];
            }
            else
            {
                pairs.Add(item, dict[item * r]);
            }
        }

        if (dict.ContainsKey(item))
        {
            dict[item]++;
        }
        else
        {
            dict.Add(item, 1);
        }
    }

    return count;
}

// ordered
string input97 = "1 1 1 2 2 2 4 4 4";
long r97 = 2;

// unordered
string input98 = "1 1 1 4 4 4 2 2 2";
long r98 = 2;

// random
string input99 = "1 17 80 68 5 5 58 17 38 81 26 44 38 6 12 11 37 67 70 16 19 35 71 16 32 45 7 39 2 14 16 78 82 5 18 86 61 37 12 8 27 90 13 26 57 24 36 4 52 67 71 71 11 51 48 42 57 16 43 58 29 58 8 20 24 25 15 84 61 78 53 49 39 66 75 6 51 72 9 13 49 79 45 21";
long r99 = 3;

string input = input99;
long r = r99;
List<long> arr = input.TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();
long ans;

ans = countTriplets_v2(arr, r);
Console.WriteLine(ans);

ans = countTriplets_Reverse(arr, r);
Console.WriteLine(ans);
