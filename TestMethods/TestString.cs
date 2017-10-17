using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMethods
{
    public class TreePath
    {
        public List<Node> GetPaths(Node root)
        {
            Dictionary<string, List<Node>> paths = new Dictionary<string, List<Node>>(); 
            //Serialize(root, paths); 

            var pathList = new List<Node>(); 
            Serialize2(root, pathList); 

            var allPaths = paths.Values; 

            return null;  
        }

        private void Serialize2(Node root, List<Node> path)
        {
           if(root == null) return; 
            path.Add(root); 
            if(root.Left == root.Right && root.Left == null)
            {
                foreach(var n in path)
                { 
                    Console.Write(n.Data + " ");
                } 

                System.Console.WriteLine();
            }
            else
            {
                Serialize2(root.Left, path);  
                Serialize2(root.Right, path);
            }

            path.RemoveAt(path.Count -1); 
        }

        private string Serialize(Node root, Dictionary<string, List<Node>> paths)
        {
            if(root == null) return "#";  

            var s =  Serialize(root.Left, paths) + Serialize(root.Right, paths) + root.Data;  
            List<Node> path; 
            if(paths.Keys.Contains(s))
            {
                path = paths[s];
            }
            else
            {
                path = new List<Node>();
                paths[s] = path;
            }

            path.Add(root); 
            return s;
        }
    }
}
