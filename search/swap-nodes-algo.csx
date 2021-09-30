/*
    重點記錄：
        1. 因為資料來源的資料順序，是由左到右並有節點才有對映的資料，所以可以用Queue來解決這個問題
        2. 因二元樹實作沒有記錄父節點，所以要找某個深度的節點時，可以用Stack來暫存節點變數
        3. 題目提到深度為[K, 2K, 3K...]的節點都需要為交換節點的左右節點，所以要用餘數來判斷是否符合
        4. 因題目的情境不會變更深度，只會變更左右，所以在建立節點時先算好深度方便交換時判斷
 */
class Result
{
    public static List<List<int>> swapNodes(List<List<int>> indexes, List<int> queries)
    {
        BinaryTree tree = new BinaryTree(indexes);

        List<List<int>> result = new List<List<int>>();
        foreach (int query in queries)
        {
            tree.Swap(query);
            result.Add(tree.Traverse());
        }

        return result;
    }
}

public class BinaryTree
{
    BinaryNode _root;

    public BinaryTree(List<List<int>> indexes)
    {
        _root = new BinaryNode(1, 1);

        Queue<BinaryNode> queue = new Queue<BinaryNode>();
        queue.Enqueue(_root);

        for (int i = 0; i < indexes.Count; i++)
        {
            BinaryNode temp = queue.Dequeue();
            if(indexes[i][0] != -1)
            {
                temp.Left = new BinaryNode(indexes[i][0], temp.Depth + 1);
            }

            if(indexes[i][1] != -1)
            {
                temp.Right = new BinaryNode(indexes[i][1], temp.Depth + 1);
            }

            if (temp.Left != null)
            {
                queue.Enqueue(temp.Left);
            }

            if (temp.Right != null)
            {
                queue.Enqueue(temp.Right);
            }
        }
    }

    public void Swap(int depth)
    {
        Stack<BinaryNode> stack = new Stack<BinaryNode>();
        stack.Push(_root);

        while(stack.Count > 0)
        {
            BinaryNode current = stack.Pop();
            if(current.Depth % depth == 0)
            {
                current.Swap();
            }

            if(current.Left != null)
            {
                stack.Push(current.Left);
            }

            if(current.Right != null)
            {
                stack.Push(current.Right);
            }
        }
    }

    public List<int> Traverse()
    {
        List<int> output = new List<int>();
        InternalTraverse(_root, output);
        return output;
    }

    private void InternalTraverse(BinaryNode node, List<int> output)
    {
        if(node == null)
        {
            return;
        }

        InternalTraverse(node.Left, output);
        output.Add(node.Value);
        InternalTraverse(node.Right, output);
    }
}

public class BinaryNode
{
    public BinaryNode Left { get; set; }
    public BinaryNode Right { get; set; }

    public int Value { get; }
    public int Depth { get; }

    public BinaryNode(int value, int depth)
    {
        Value = value;
        Depth = depth;
    }

    public void Swap()
    {
        BinaryNode temp = Left;
        Left = Right;
        Right = temp;
    }
}

using (var reader = new StreamReader("./SwapNodes_BinaryTree.Sample.txt"))
{
    int n = Convert.ToInt32(reader.ReadLine().Trim());

    List<List<int>> indexes = new List<List<int>>();

    for (int i = 0; i < n; i++)
    {
        indexes.Add(
            reader.ReadLine().TrimEnd().Split(' ').ToList()
                .Select(indexesTemp => Convert.ToInt32(indexesTemp)).ToList());
    }

    int queriesCount = Convert.ToInt32(reader.ReadLine().Trim());

    List<int> queries = new List<int>();

    for (int i = 0; i < queriesCount; i++)
    {
        int queriesItem = Convert.ToInt32(reader.ReadLine().Trim());
        queries.Add(queriesItem);
    }

    List<List<int>> result = Result.swapNodes(indexes, queries);

    Console.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));
}