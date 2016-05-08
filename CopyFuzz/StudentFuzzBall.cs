using System;
using System.IO;
using System.Windows.Forms;


namespace CopyFuzz
{
    internal class StudentFuzzBall
    {
        private Form _application;
        private readonly TextWriter _output;
        private bool _print;

        /// <summary>
        /// Launches the application form and listens to all input to record it.
        /// </summary>
        /// <param name="application">The application you want to learn input to.</param>
        /// <param name="output">The output stream this learner should print the results to</param>
        public StudentFuzzBall(Form application, TextWriter output)
        {
            _application = application;
            _output = output;
            _print = true;

            application.MouseClick += (sender, args) => Record($"Click-{args.X}-{args.Y}-{args.Button}");
            application.MouseDown += (sender, args) => Record($"MouseUp-{args.X}-{args.Y}");
            application.MouseUp += (sender, args) => Record($"MouseUp-{args.X}-{args.Y}");
            application.MouseMove += (sender, args) => Record($"MouseMove-{args.X}-{args.Y}");
            application.KeyDown += (sender, args) => Record($"KeyDown-{args.KeyCode}");

            Application.EnableVisualStyles();
            Application.Run(application);
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        private void Record(string s)
        {
            Say(s);
            _output.WriteLine(s);
        }
    }
}
