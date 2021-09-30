/*
    注意：
        * 雖然Array a & c 的長度在 in32 的範圍內，但int32相乘發生溢位時不會報錯同時產生負數
          所以在這兩個陣列索引的型別需用long(Int64)才能避免溢位的發生
            - 如果擔心陣列取值造成的轉型耗損，可以把索引和計數分開成兩個變數
        * Distinct()回傳的結果造成亂序，不論輸入的陣列是否已排序，所以在它之後再排序才能確保陣列的內容為依序且唯一
 */
static long triplets(int[] a, int[] b, int[] c)
{
    a = a.Distinct().OrderBy(x => x).ToArray();
    b = b.Distinct().OrderBy(x => x).ToArray();
    c = c.Distinct().OrderBy(x => x).ToArray();

    long count = 0;
    long l = 0;
    long r = 0;
    for(int i = 0; i < b.Length; i++)
    {
        while(l < a.Length && a[l] <= b[i])
        {
            l++;
        }

        while(r < c.Length && c[r] <= b[i])
        {
            r++;
        }

        count += l * r;
    }

    return count;
}

StreamReader reader = new StreamReader("./triple-sum.input04.txt");

string[] lenaLenbLenc = reader.ReadLine().Split(' ');

int lena = Convert.ToInt32(lenaLenbLenc[0]);

int lenb = Convert.ToInt32(lenaLenbLenc[1]);

int lenc = Convert.ToInt32(lenaLenbLenc[2]);

int[] arra = Array.ConvertAll(reader.ReadLine().Split(' '), arraTemp => Convert.ToInt32(arraTemp));

int[] arrb = Array.ConvertAll(reader.ReadLine().Split(' '), arrbTemp => Convert.ToInt32(arrbTemp));

int[] arrc = Array.ConvertAll(reader.ReadLine().Split(' '), arrcTemp => Convert.ToInt32(arrcTemp));
long ans = triplets(arra, arrb, arrc);

Console.WriteLine(ans);

reader.Dispose();