using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1.ReducePictureSize
{
    public class ReduceSize
    {
        public static int Result;
        public static volatile bool Finished;
        //public static bool finished;

        static void Thread2()
        {
            Result = 143;
            Finished = true;
        }

        public static void Start()
        {
            new Thread(new ThreadStart(Thread2)).Start();


            //for (;;)
            //{
            //    if (finished)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"result {result}");
            //        return;
            //    }
            //    Debug.WriteLine(".....");
            //}


            // For parallel
            // Return a value type with a lambda expression
            Task<int> task1 = Task<int>.Factory.StartNew(() => 1);
            int i = task1.Result;

            // Return a named reference type with a multi-line statement lambda.
            Task<TaskResultRetual> task2 = Task<TaskResultRetual>.Factory.StartNew(() =>
            {
                string s = ".NET";
                double d = 4.0;
                return new TaskResultRetual { Name = s, Number = d };
            });
            TaskResultRetual test = task2.Result;

            // Return an array produced by a PLINQ query
            Task<string[]> task3 = Task<string[]>.Factory.StartNew(() =>
            {
                string path = @"C:\Users\swang\Pictures\";
                string[] files = System.IO.Directory.GetFiles(path);

                var result = (from file in files.AsParallel()
                              let info = new System.IO.FileInfo(file)
                              where info.Extension == ".jpg"
                              select file).ToArray();

                return result;
            });

            foreach (var name in task3.Result)
                Console.WriteLine(name);



            System.Console.ReadKey();
        }
    }


    public class TaskResultRetual
    {
        public string Name { get; set; }
        public double Number { get; set; }

    }
}
