using System;
using System.IO;
using System.Windows.Forms;
using GUI;


namespace CopyFuzz
{
    internal class StudentFuzzBall
    {
        private MainForm _application;
        private readonly TextWriter _output;
        private bool _print;
        private string _lastMouseMoved;

        /// <summary>
        /// Launches the application form and listens to all input to record it.
        /// </summary>
        /// <param name="application">The application you want to learn input to.</param>
        /// <param name="output">The output stream this learner should print the results to</param>
        public StudentFuzzBall(MainForm application, TextWriter output)
        {
            _application = application;
            _output = output;
            _print = true;

            _output.WriteLine("session");

            application.MouseClick += (sender, args) => Record($"MouseClick-{args.X}-{args.Y}-{args.Button}");
            application.MouseDown += (sender, args) => Record($"MouseDown-{args.X}-{args.Y}-{args.Button}");
            application.MouseUp += (sender, args) => Record($"MouseUp-{args.X}-{args.Y}-{args.Button}");
            application.MouseMove += (sender, args) => _lastMouseMoved = $"MouseMove-{args.X}-{args.Y}";
            application.KeyDown += (sender, args) => Record($"KeyDown-{args.KeyCode}");

            Application.EnableVisualStyles();
            Application.Run(application);

            if (_print)
            {
                Console.WriteLine("Application closed.  Saving session...");
            }

            _output.Close();
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        private void Record(string s)
        {
            if (_lastMouseMoved != "")
            {
                _output.WriteLine(_lastMouseMoved);
                Say(_lastMouseMoved);
                _lastMouseMoved = "";
            }

            Say(s);
            _output.WriteLine(s);
        }
    }
}
