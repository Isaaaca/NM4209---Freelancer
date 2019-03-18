using System;
using System.Diagnostics;

namespace BinarySearchTree
{
    class Node: IComparable<Node>
    {
        public string value;
        public Node left;
        public Node right;

        public int CompareTo(Node other)
        {
            return value.CompareTo(other.value);
        }

        public Node(string str)
        {
            value = str;
        }
    }

    class Tree<T>
    {
        private Node root = null;

        public Tree(string str)
        {
            root = insert(root, str);
        }

        public Node insert(Node insertPoint, string v)
        {
            if (insertPoint == null)
            {
                insertPoint = new Node(v);
            }
            else if (insertPoint.value.CompareTo(v)<0)
            {
                insertPoint.left = insert(insertPoint.left, v);
            }
            else
            {
                insertPoint.right = insert(insertPoint.right, v);
            }

            return insertPoint;
        }

        public void traverse(Node root)
        {
            if (root == null)
            {
                return;
            }

            traverse(root.left);
            traverse(root.right);
        }
    }


    class BinarySearchTree
    {
        
    }
}
