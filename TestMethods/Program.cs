using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMethods
{
    public class BaseTest
    {
        public virtual int Test()
        {
            return 10; 
        }

    }

    public class DClass:BaseTest
    {
        public new int Test()
        {
            return 20; 
        }
    }



    class Program
    {
        private static void Test()
        { 

        }
        static void Main(string[] args)
        {
            //var root = new Node(1); 
            //root.Left = new Node(2); 
            //root.Left.Left = new Node(4); 
            //root.Left.Right = new Node(5);
            //root.Right = new Node(3);

            Node root = new Node(28);
            root.Left = new Node(23);
            root.Right = new Node(35);
            root.Left.Left = new Node(20);
            root.Left.Right = new Node(25);
            root.Left.Left.Left = new Node(19);
            root.Left.Left.Right = new Node(26);
            root.Right.Left = new Node(30);
            root.Right.Right = new Node(45);


            TreePath pathFinder = new TreePath();
            var paths = pathFinder.GetPaths(root);


            int[] arr = {1,2,3}; 

            System.Console.WriteLine(arr.Length);
            System.Console.WriteLine(arr.Count());
 
	 
           System.Console.WriteLine("t");


            BaseTest bt = new DClass(); 
            var t = bt.Test(); 

            Test();



        }
    }
}
