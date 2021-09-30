/*
    在計算花朵的金額時，要注意題目有一個重點
        (current purchase + previous purchases) X price
    
    表示，同一個人每多買一朵花，就需要考量到他之前買的數量
        第1朵，(1 + 0) x price
        第2朵，(1 + 1) x price
        第3朵，(1 + 2) x Price
        ...以此類推
    
    解題思維
        先把金額由高到低排好，接下來所有人先買第1朵，還有花再買第2朵，一直買到沒有花為止
 */
static int getMinimumCost(int k, int[] c)
{
    int minCost = 0;

    int n = c.Length;
    int previous = 0;
    if (n <= k)
    {
        minCost = BuyFlowers(c, 0, n, previous);
    }
    else
    {
        Array.Sort(c, (x, y) => y.CompareTo(x));
        Console.WriteLine($"sorted: {string.Join(",", c)}");

        int current = 0;
        while (current <= n)
        {
            int number = Math.Min(n - current, k);
            minCost += BuyFlowers(c, current, number, previous);

            current += k;
            previous++;
        }
    }

    return minCost;
}

static int BuyFlowers(int[] prices, int skip, int number, int previous)
{
    Console.WriteLine($"skip: {skip}, number: {number}, previous: {previous}\ndata: {string.Join(",", prices.Skip(skip).Take(number))}");
    return prices.Skip(skip).Take(number).Sum(x => (1 + previous) * x);
}

string[][] inputs = new[] {
    new[] {
        "2",
        "2 5 6"
    },
    new [] {
        "32",
        "900364 876803 469727 818827 521363 797927 293011 358419 935006 687468 225943 717791 817443 52028 894472 388300 746898 856300 67397 109579 183142 445096 197134 72657 651485 265216 672456 768500 304900 704585 175389 208681 544236 516977 942016 68247 561081 4643 210958 439703 134808 865683 877072 860491 86416 251878 456221 147352 966386 133889 828636 353217 264427 835373 812136 431841 827640 904164 487820 503493 502328 714555 573933 309284 792413 517291 442503 197627 145980 428669"
    },
};

foreach (string[] input in inputs)
{
    Console.WriteLine("".PadRight(80, '='));
    Console.WriteLine($"k: {input[0]}\nc: {input[1]}");

    int k = Convert.ToInt32(input[0]);
    int[] c = Array.ConvertAll(input[1].Split(' '), cTemp => Convert.ToInt32(cTemp));
    int minimumCost = getMinimumCost(k, c);

    Console.WriteLine($"result: {minimumCost}");
}
