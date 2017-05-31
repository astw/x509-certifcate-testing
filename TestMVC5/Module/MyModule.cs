using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestMVC5
{
    public class MyModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.Context_BeginRequest);
            context.EndRequest += new EventHandler(this.Context_EndRequest);
        }

        public void Dispose()
        {

        }

        public void Context_EndRequest(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"C:\requestLog.txt", true);
            sw.WriteLine("End Request called at " + DateTime.Now.ToString()); sw.Close();
        }
        public void Context_BeginRequest(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"C:\requestLog.txt", true);
            sw.WriteLine("Begin request called at " + DateTime.Now.ToString()); sw.Close();
        }
    }
}