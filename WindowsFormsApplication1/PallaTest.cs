using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class PallaTest : Form
    {
        EmailService _emailService = new EmailService();
        public PallaTest()
        {
            InitializeComponent();
        }

        private void PallaTest_Load(object sender, EventArgs e)
        {
            //Test_Asyn_Parall(); 
        }

        private void Test_Asyn_Exception()
        {
            _emailService.SendEmail();

        }
        private void Test_Asyn_Parall()
        {
            Task.Factory.StartNew<int>(() => 8)
               .ContinueWith(ant => ant.Result * 2)
               .ContinueWith(ant => Math.Sqrt(ant.Result))
               .ContinueWith(ant => Console.WriteLine(ant.Result));   // 4


            int x = 0;
            Task<int> calc = Task.Factory.StartNew(() => 7 / x);
            try
            {
                Console.WriteLine(calc.Result);
            }

            //catch (AggregateException aex)
            //{
            //    Console.Write(aex.InnerException.Message);  // Attempted to divide by 0
            //}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            //Task continuation = Task.Factory.StartNew(() => { throw null; })
            //                        .ContinueWith(ant =>
            //                                      {
            //                                          if (ant.Exception != null)
            //                                              throw ant.Exception;
            //                                      });

            //continuation.Wait();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Test_Asyn_Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    public class EmailService
    {
        public async Task SendEmail()
        {
            try
            {
                var template = await GetTemplate();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ax)
            {
                Console.WriteLine(ax.Message);
            }
            catch (Exception exe)
            {
                Console.WriteLine(exe.Message);
            }

            throw new Exception("Exception from aysn function");
        }

        private Task<string> GetTemplate()
        {
            throw new Exception("Exception from GetTemplate ");
            return Task.FromResult("result from GetTemplate");
        }

    }

}
