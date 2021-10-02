/*
    思維整理：
        1. 當m * w小於p時，不要浪費時間在空的迴圈裡，用計算跳過這些回合
        2. 先做投資，再計算產能，這個策略有個陷阱，如果當天產能就夠的話，就不需要再多等一天
            2.1 基於這個考量，同時要計算以當下產能來看，還需要多少rounds
            2.2 再判斷passes和rounds之間，哪個比較小，就是最小值
        3. 題目沒有提到m * w不會溢位，所以要多判斷與處理這個情境
*/
class Result
{
    public static long minimumPasses(long m, long w, long p, long n)
    {
        long passes = 0;
        long remaining = 0;
        long rounds = long.MaxValue;
        while (remaining < n)
        {
            long saveRounds = IsMultipleOverflow(m, w) ?
                0 : (p - remaining) / (m * w);
            if(saveRounds <= 0)
            {
                InvestInMachinesAndWorkers(p, ref remaining, ref m, ref w);
                saveRounds = 1;
            }

            passes += saveRounds;
            if(IsMultipleOverflow(saveRounds * m, w))
            {
                remaining = long.MaxValue;
            }
            else
            {
                remaining += saveRounds * m * w;
                rounds = Math.Min(rounds, 
                    (long)(passes + Math.Ceiling((n - remaining) / (m * w * 1.0))));
            }
            Console.WriteLine($"passes: {passes}, remaining: {remaining}, m: {m}, w: {w}, rounds: {rounds}");
        }

        Console.WriteLine($"passes: {passes}, remaining: {remaining}, m: {m}, w: {w}, rounds: {rounds}");

        return Math.Min(passes, rounds);
    }

    static bool IsMultipleOverflow(long m, long w)
    {
        return m > long.MaxValue / w;
    }

    static void InvestInMachinesAndWorkers(long p, ref long r, ref long m, ref long w)
    {
        if (r < p)
        {
            return;
        }

        long amt = r / p;
        r %= p;

        if (m >= w + amt)
        {
            w += amt;
        }
        else if (w >= m + amt)
        {
            m += amt;
        }
        else
        {
            long total = m + w + amt;
            m = total / 2;
            w = total - m;
        }
    }
}

string[] inputs = new[]{
    "./search/making-candies.input01.txt",
    "./search/making-candies.input10.txt",
};

foreach (var input in inputs)
{
    var reader = new StreamReader(input);

    Console.WriteLine($"Start test case: {input}");
    Stopwatch sw = Stopwatch.StartNew();

    string[] firstMultipleInput = reader.ReadLine().TrimEnd().Split(' ');

    long m = Convert.ToInt64(firstMultipleInput[0]);

    long w = Convert.ToInt64(firstMultipleInput[1]);

    long p = Convert.ToInt64(firstMultipleInput[2]);

    long n = Convert.ToInt64(firstMultipleInput[3]);

    long result = Result.minimumPasses(m, w, p, n);
    Console.WriteLine($"result: {result}");

    sw.Stop();
    Console.WriteLine($"Execution time: {sw.Elapsed}");
    Console.WriteLine("".PadLeft(80, '='));

    reader.Dispose();
}