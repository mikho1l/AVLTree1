using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTree2020;
using System.Collections.Generic;
using System.Linq;

namespace BinTreeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIncreaseAfterAdding()
        {
            var tree = new AVLTree<int, int>();
            int n = 2;
            for(int i=0; i<n; i++)
            {
                tree.Add(i,i);
            }
            Assert.AreEqual(n, tree.Count);
        }
       [TestMethod]
        public void ItemsExistsAfterAdding()
        {
            var tree = new AVLTree<int, int>();
            var a = new[] { 22, 30, 15, 5, 17, 24, 33, 10, 16, 26 };
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                tree.Add(a[i], i);
            }
            Assert.AreEqual(n, tree.Count);
            Array.Sort(a);
            int j = 0;
            foreach (var pair in tree)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }
        }
       
        [TestMethod]
        public void CountIsZeroAfterTreeCreation()
        {
            var tree = new AVLTree<int, int>();
            Assert.AreEqual(0, tree.Count);
        }
       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddDuplicateKey()
        {
            var tree = new AVLTree<int, int>();
            tree.Add(5, 5);
            tree.Add(5, 2);
        }
        
        [TestMethod]
        public void CheckingOfDeleteMethod()
        {
            var tree = new AVLTree<int, int>();
            var a = new[] { 22, 30, 15, 5, 17, 24, 33, 10, 16, 26 };
            int n = a.Length;
            const int k = 3;
            for (int i = 0; i < n; i++)
            {
                tree.Add(a[i], i);
            }
            for (int i = 0; i < k; i++)
            {
                tree.Delete(a[i]);
                a[i] = int.MaxValue;
                n--;
            }
            Array.Sort(a);
            int j = 0;
            foreach(var pair in tree)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, j);

        }
        

    }
}
