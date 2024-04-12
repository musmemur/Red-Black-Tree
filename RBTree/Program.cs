using System;
namespace RBTree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RB tree = new();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(-1);
            tree.Insert(11);
            tree.Insert(6);
            tree.DisplayTree();
            tree.Delete(-1);
            tree.DisplayTree();
            tree.Delete(4);
            tree.DisplayTree();
            tree.Delete(6);
            tree.DisplayTree();
        }
    }
}
