using System.Drawing;
using System.Xml.Linq;

namespace RBTree
{
    class RB
    {
        private Node? root;
        private void LeftRotate(Node? X)
        {
            Node? Y = X.Right; 
            X.Right = Y.Left;
            if (Y.Left != null)
            {
                Y.Left.Parent = X;
            }
            if (Y != null)
            {
                Y.Parent = X.Parent;
            }
            if (X.Parent == null)
            {
                root = Y;
            }
            if (X == X.Parent.Left)
            {
                X.Parent.Left = Y;
            }
            else
            {
                X.Parent.Right = Y;
            }
            Y.Left = X;
            if (X != null)
            {
                X.Parent = Y;
            }

        }
        private void RightRotate(Node Y)
        {
            Node? X = Y.Left;
            Y.Left = X.Right;
            if (X.Right != null)
            {
                X.Right.Parent = Y;
            }
            if (X != null)
            {
                X.Parent = Y.Parent;
            }
            if (Y.Parent == null)
            {
                root = X;
            }
            if (Y == Y.Parent.Right)
            {
                Y.Parent.Right = X;
            }
            if (Y == Y.Parent.Left)
            {
                Y.Parent.Left = X;
            }

            X.Right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.Parent = X;
            }
        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }
        public Node? Find(int key)
        {
            bool isFound = false;
            Node? temp = root;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                else if (key < temp.Value)
                {
                    temp = temp.Left;
                }
                else if (key > temp.Value)
                {
                    temp = temp.Right;
                }
                else
                {
                    isFound = true;
                }
            }
            if (isFound)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        public void Insert(int item)
        {
            Node? newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.colour = NodeColor.Black;
                return;
            }
            Node? Y = null;
            Node? X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.Value < X.Value)
                {
                    X = X.Left;
                }
                else
                {
                    X = X.Right;
                }
            }
            newItem.Parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.Value < Y.Value)
            {
                Y.Left = newItem;
            }
            else
            {
                Y.Right = newItem;
            }
            newItem.Left = null;
            newItem.Right = null;
            newItem.colour = NodeColor.Red;
            InsertFixUp(newItem);
        }
        private void InOrderDisplay(Node? current)
        {
            if (current != null)
            {
                InOrderDisplay(current.Left);
                Console.Write("({0}) ", current.Value);
                InOrderDisplay(current.Right);
            }
        }
        private void InsertFixUp(Node? item)
        {
            while (item != root && item.Parent.colour == NodeColor.Red)
            {
                if (item.Parent == item.Parent.Parent.Left)
                {
                    Node? Y = item.Parent.Parent.Right;
                    if (Y != null && Y.colour == NodeColor.Red) //Case 1: uncle is red
                    {
                        item.Parent.colour = NodeColor.Black;
                        Y.colour = NodeColor.Black;
                        item.Parent.Parent.colour = NodeColor.Red;
                        item = item.Parent.Parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.Parent.Right)
                        {
                            item = item.Parent;
                            LeftRotate(item);
                        }
                        //Case 3
                        item.Parent.colour = NodeColor.Black;
                        item.Parent.Parent.colour = NodeColor.Red;
                        RightRotate(item.Parent.Parent);
                    }

                }
                else
                {
                    Node? X = item.Parent.Parent.Left;
                    if (X != null && X.colour == NodeColor.Black)//Case 1
                    {
                        item.Parent.colour = NodeColor.Red;
                        X.colour = NodeColor.Red;
                        item.Parent.Parent.colour = NodeColor.Black;
                        item = item.Parent.Parent;
                    }
                    else //Case 2
                    {
                        if (item == item.Parent.Left)
                        {
                            item = item.Parent;
                            RightRotate(item);
                        }
                        //Case 3
                        item.Parent.colour = NodeColor.Black;
                        item.Parent.Parent.colour = NodeColor.Red;
                        LeftRotate(item.Parent.Parent);

                    }

                }
                root.colour = NodeColor.Black;
            }
        }
        public void Delete(int key)
        {
            var item = Find(key);
            if (item == null)
            {
                Console.WriteLine("Удалять нечего");
                return;
            }
            Node? Y;
            if (item.Left == null || item.Right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            Node? X;
            if (Y.Left != null)
            {
                X = Y.Left;
            }
            else
            {
                X = Y.Right;
            }
            if (X != null)
            {
                X.Parent = Y;
            }
            if (Y.Parent == null)
            {
                root = X;
            }
            else if (Y == Y.Parent.Left)
            {
                Y.Parent.Left = X;
            }
            else
            {
                Y.Parent.Left = X;
            }
            if (Y != item)
            {
                item.Value = Y.Value;
            }
            if (Y.colour == NodeColor.Black)
            {
                DeleteFixUp(X);
            }
        }
        private void DeleteFixUp(Node? X)
        {

            while (X != null && X != root && X.colour == NodeColor.Black)
            {
                if (X == X.Parent.Left)
                {
                    Node? W = X.Parent.Right;
                    if (W.colour == NodeColor.Red)  //case 1
                    {
                        W.colour = NodeColor.Black; 
                        X.Parent.colour = NodeColor.Red; 
                        LeftRotate(X.Parent); 
                        W = X.Parent.Right; 
                    }
                    if (W.Left.colour == NodeColor.Black && W.Right.colour == NodeColor.Black)  //case 2
                    {
                        W.colour = NodeColor.Red; 
                        X = X.Parent; 
                    }
                    else if (W.Right.colour == NodeColor.Black)     //case 3
                    {
                        W.Left.colour = NodeColor.Black; 
                        W.colour = NodeColor.Red; 
                        RightRotate(W); 
                        W = X.Parent.Right; 
                    }
                    //case 4
                    W.colour = X.Parent.colour; 
                    X.Parent.colour = NodeColor.Black; 
                    W.Right.colour = NodeColor.Black; 
                    LeftRotate(X.Parent); 
                    X = root; 
                }
                else 
                {
                    Node? W = X.Parent.Left;
                    if (W.colour == NodeColor.Red)
                    {
                        W.colour = NodeColor.Black;
                        X.Parent.colour = NodeColor.Red;
                        RightRotate(X.Parent);
                        W = X.Parent.Left;
                    }
                    if (W.Right.colour == NodeColor.Black && W.Left.colour == NodeColor.Black)
                    {
                        W.colour = NodeColor.Black;
                        X = X.Parent;
                    }
                    else if (W.Left.colour == NodeColor.Black)
                    {
                        W.Right.colour = NodeColor.Black;
                        W.colour = NodeColor.Red;
                        LeftRotate(W);
                        W = X.Parent.Left;
                    }
                    W.colour = X.Parent.colour;
                    X.Parent.colour = NodeColor.Black;
                    W.Left.colour = NodeColor.Black;
                    RightRotate(X.Parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = NodeColor.Black;
        }
        private Node Minimum(Node X)
        {
            while (X.Left.Left != null)
            {
                X = X.Left;
            }
            if (X.Left.Right != null)
            {
                X = X.Left.Right;
            }
            return X;
        }
        private Node TreeSuccessor(Node X)
        {
            if (X.Left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node? Y = X.Parent;
                while (Y != null && X == Y.Right)
                {
                    X = Y;
                    Y = Y.Parent;
                }
                return Y;
            }
        }
    }
}