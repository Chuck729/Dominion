using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFuzz
{
    public class TesterFuzzBall
    {
        private Form _application;
        private bool _print;

        public TesterFuzzBall(Form application)
        {
            _application = application;
            _print = true;

            var thread = new Thread(Test);
            thread.Start();

            Application.EnableVisualStyles();
            Application.Run(application);

        }

        private void Test()
        {
            Thread.Sleep(2000);
            SendKeys.SendWait("{ESCAPE}");
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }
    }
}
