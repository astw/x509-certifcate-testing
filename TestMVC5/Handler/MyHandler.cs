using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestMVC5.Handler
{
    public class MyHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("The page request is " + context.Request.RawUrl.ToString());
            StreamWriter sw = new StreamWriter(@"C:\requestLog.txt", true);
            sw.WriteLine("Page requested at " + DateTime.Now.ToString() +
                        context.Request.RawUrl); sw.Close();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}