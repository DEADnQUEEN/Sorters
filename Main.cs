using Sorters;

class MainFunc
{
    public static void Main(string[] args)
    {
        TwoPointerSort sort = new TwoPointerSort(new double[] { 0, 5, 2, 1, 3 });
        sort.PrintSequence();
    }
}