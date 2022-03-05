using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree2020
{
    internal class Node<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Node<TKey, TValue> Left, Right, Parent;

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
        #region AVL
        public int Height { get; set; }
        public int BalanceFactor
        {
            get
            {
                int l = (Left == null) ? 0 : Left.Height;
                int r = (Right == null) ? 0 : Right.Height;
                return r - l;
            }
        }
        internal void IncHeight()
        {
            int l = (Left == null) ? 0 : Left.Height;
            int r = (Right == null) ? 0 : Right.Height;
            Height = (l > r ? l : r) + 1;
        }
        #endregion
    }

    public class AVLTree<TKey, TValue> :IEnumerable<KeyValuePair<TKey,TValue>>  
        where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root;

        public AVLTree()
        {
            Count = 0;
        }
        #region AVL
        private void RotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> newnode = node.Left;
            node.Left = newnode.Right;
            newnode.Right = node;
            newnode.Right.IncHeight();
            newnode.IncHeight();
            node = newnode;
        }

        private void RotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> node2 = node.Right;
            node.Right = node2.Left;
            node2.Left = node;
            node2.Left.IncHeight();
            node2.IncHeight();
            node = node2;
        }

        void Balance(Node<TKey, TValue> node)
        {
            node.IncHeight();

            if (node.BalanceFactor == 2)
            {
                if (node.Right.BalanceFactor < 0)
                    RotateRight(node.Right);
                RotateLeft(node);
            }

            if (node.BalanceFactor == -2)
            {
                if (node.Left.BalanceFactor > 0)
                    RotateLeft(node.Left);
                RotateRight(node);
            }
        }
        #endregion
        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey,TValue>(key, value);
            if(_root==null)
            {
                _root = node;
                Count++;
                return;
            }
            var current = _root;
            var parent = _root;
            while(current!=null)
            {
                parent = current;
                if (current.Key.CompareTo(node.Key) == 0)
                {
                    throw new ArgumentException("Dublicate key");
                }
                if (current.Key.CompareTo( node.Key)>0)
                {
                    current = current.Left;
                }
                else if (current.Key.CompareTo(node.Key) < 0)
                {
                    current = current.Right;
                }
               
            }
            if (parent.Key.CompareTo(node.Key) > 0)
            {
                parent.Left = node; 
            }
            else
            {
                parent.Right = node;
            }
            node.Parent = parent;
            Count++;
            Balance(node);
        }
        public void Delete(TKey key)
        {
            var node = Search(key);
            if (node == null)
                throw new ArgumentException("There is no element at this collection");
            bool leftisnull = node.Left is null;
            bool rightisnull = node.Right is null;
            if (leftisnull && rightisnull)
            {
                if (Count == 1)
                {
                    _root = null;
                    return;
                }
                if (node.Key.CompareTo(node.Parent.Key) < 0) //если node -- левый потомок родителя
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
            else if (leftisnull)
            {
                if (node.Key.CompareTo(_root.Key) == 0)
                {
                    _root = node.Right;
                    return;
                }
                if (node.Key.CompareTo(node.Parent.Key) < 0)
                {
                    node.Parent.Left = node.Right;
                    node.Right.Parent = node.Parent;
                }
                else
                {
                    node.Parent.Right = node.Right;
                    node.Right.Parent = node.Parent;
                }
            }
            else if (rightisnull)
            {
                if (node.Key.CompareTo(_root.Key) == 0)
                {
                    _root = node.Left;
                    return;
                }
                if (node.Key.CompareTo(node.Parent.Key) < 0)
                {
                    node.Parent.Left = node.Left;
                    node.Left.Parent = node.Parent;
                }
                else
                {
                    node.Parent.Right = node.Left;
                    node.Left.Parent = node.Parent;
                }
            }
            else
            {
                //соединение родителя прибл. знач с потомком прибл. знач не написано //написано
                var approximatelyequalnode = node.Left;
                while(approximatelyequalnode.Right != null)
                {
                    approximatelyequalnode = approximatelyequalnode.Right;
                }
                if (approximatelyequalnode.Key.CompareTo(approximatelyequalnode.Parent.Key) < 0)
                {
                    approximatelyequalnode.Parent.Left = approximatelyequalnode.Left; //было: null
                }
                else
                {
                    approximatelyequalnode.Parent.Right = approximatelyequalnode.Left; //было: null
                }
                node.Key = approximatelyequalnode.Key;
                node.Value = approximatelyequalnode.Value;
            }
            Count--;
            Balance(node);
        }
        public bool Contains(TKey key)
        {
            return !(Search(key) is null);
        }
        public TValue this[TKey key]
        {
            get
            {
                return Search(key).Value;
            }
        }
        private Node<TKey, TValue> Search(TKey key)
        {
            if (Count == 0)
            {
                throw new ArgumentNullException("The collection is empty");
            }
            var node = _root;
            while(node != null)
            {
                if (node.Key.CompareTo(key) == 0)
                {
                    return node;
                }
                else if (node.Key.CompareTo(key) < 0)
                {
                    node = node.Right;
                }
                else
                {
                    node = node.Left;
                }
            }
            return null;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var current = _root;
            List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
            RecursiveTrav(_root, list);
            return list.GetEnumerator();
           
        }
        private void RecursiveTrav(Node<TKey, TValue> node, List<KeyValuePair<TKey, TValue>> list)
        {
            if (node.Left != null)
                RecursiveTrav(node.Left, list);
            list.Add(new KeyValuePair<TKey,TValue>(node.Key, node.Value));
            if(node.Right != null)
                RecursiveTrav(node.Right, list);

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}

