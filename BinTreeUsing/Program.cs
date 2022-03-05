using BinaryTree2020;
using System.Diagnostics;

Random rnd = new Random(DateTime.Now.Millisecond);
int[] arr = new int[10000];
for (int i = 0; i < arr.Length; i++)
{
    arr[i] = rnd.Next();
}

Stopwatch sw = Stopwatch.StartNew();
AVLTree<int, int> binTree = new();
foreach (var num in arr)
    binTree.Add(num, num);
for (int i = 5000; i <= 7000; i++)
{
    binTree.Delete(arr[i]);
}
for (int i = 0; i < arr.Length; i++)
    binTree.Contains(arr[i]);
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);

sw.Restart();
SortedDictionary<int, int> sd = new();
foreach (var num in arr)
    sd.Add(num, num);
for (int i = 5000; i <= 7000; i++)
{
    sd.Remove(arr[i]);
}
for (int i = 0; i < arr.Length; i++)
    sd.ContainsKey(arr[i]);
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);
