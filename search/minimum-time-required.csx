/*
    說明：
        1. 先找出天數搜尋的左右邊界
            1.1 左邊界的假設是所有machines都是最小值時，需要的天數是多少
            1.2 右邊界的假設是所有machines都是最大值時，需要的天數是多少
        2. 接下來，採用二分搜尋法，即可找出天數
        3. 注意計算公式的型別
            3.1 當goal >= machines.Length時，int64 / int64 => int64，即使小數點被去掉，仍可以正常計算出左右邊界
            3.2 當goal < machines.Length時，得到的結果為0，會造成左右邊界也是0，造成後續邏輯異常
            3.3 故，在計算左右邊界時，先轉成double，得到結果後再轉回long
 */

using System.Linq;
using System.Diagnostics;

class Solution
{
    static long minTime(long[] machines, long goal)
    {
        Array.Sort(machines);
        Console.WriteLine($"[DEBUG] goal: {goal}, machines: {machines.Length}, min: {machines[0]}, max: {machines[machines.Length - 1]}");

        long left = (long)(1.0 * goal / machines.Length * machines[0]);
        long right = (long)(1.0 * goal / machines.Length * machines[machines.Length - 1]);
        long days = -1;

        Console.WriteLine($"init: left: {left}, right: {right}");
        while(left < right)
        {
            long mid = (left + right) / 2;
            Console.WriteLine($"before: left: {left}, right: {right}, mid: {mid}");

            long prod = 0;
            foreach(long machine in machines)
            {
                prod += mid / machine;
                if(prod >= goal)
                {
                    break;
                }
            }

            Console.WriteLine($"debug: prod: {prod}, goal: {goal}");
            if(prod < goal)
            {
                left = mid + 1;
                days = mid + 1;
            }
            else
            {
                right = mid;
                days = mid;
            }
            Console.WriteLine($"after: left: {left}, right: {right}, mid: {mid}");
        }

        return days;
    }

    public static void Entry()
    {
        string[] inputs = new[] {
            "./minimum-time-required.input10.txt",
            "./minimum-time-required.input07.txt",
            "./minimum-time-required.input06.txt",
        };

        foreach (string input in inputs)
            using (StreamReader reader = new StreamReader(input))
            {
                Console.WriteLine($"Start test case: {input}");
                Stopwatch sw = Stopwatch.StartNew();
                string[] nGoal = reader.ReadLine().Split(' ');

                int n = Convert.ToInt32(nGoal[0]);

                long goal = Convert.ToInt64(nGoal[1]);

                long[] machines = Array.ConvertAll(reader.ReadLine().Split(' '), machinesTemp => Convert.ToInt64(machinesTemp));

                long ans = minTime(machines, goal);

                Console.WriteLine(ans);
                sw.Stop();
                Console.WriteLine($"Execution time: {sw.Elapsed}");
                Console.WriteLine("".PadLeft(80, '='));
            }
    }
}


Solution.Entry();
