﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new ImageReduceForm());  
            //Application.Run(new PdfTestFrm());
            //Application.Run(new XmlSortFrm());
            //Application.Run(new PallaTest());
            Application.Run(new TestForm());
        }
    }
}
