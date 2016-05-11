using System;
using System.IO;
using System.Windows.Forms;


namespace CopyFuzz
{
    internal class StudentFuzzBall
    {
        private ICopyFuzzifyer _application;
        private readonly TextWriter _output;
        private readonly bool _print;
        private int _lastMouseX;
        private int _lastMouseY;
        
        private float _distanceToRecordMousePoint = 4.0f;

        /// <summary>
        /// Launches the application form and listens to all input to record it.
        /// </summary>
        /// <param name="application">The application you want to learn input to.</param>
        /// <param name="output">The output stream this learner should print the results to</param>
        public StudentFuzzBall(ICopyFuzzifyer application, TextWriter output)
        {
            _application = application;
            _output = output;
            _print = true;

            _output.WriteLine("session");

            application.MouseClickEvent += (sender, args) => Record($"MouseClick-{args.X}-{args.Y}-{args.Button}");
            application.MouseDownEvent += (sender, args) => Record($"MouseDown-{args.X}-{args.Y}-{args.Button}");
            application.MouseUpEvent += (sender, args) => Record($"MouseUp-{args.X}-{args.Y}-{args.Button}");
            application.MouseMoveEvent += (sender, args) => RecordMouseMove(args.X, args.Y);
            application.KeyDownEvent += (sender, args) => Record($"KeyDown-{args.KeyCode}");

            _application.Launch();

            if (_print)
            {
                Console.WriteLine(@"Application closed.  Saving session...");
            }

            _output.Close();
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        private void RecordMouseMove(int x, int y)
        {
            var s = $"MouseMove-{x}-{y}";

            if (Math.Sqrt(Math.Pow(x - _lastMouseX, 2) + Math.Pow(y - _lastMouseY, 2)) > _distanceToRecordMousePoint)
            { 
                Record(s);
            }

            _lastMouseX = x;
            _lastMouseY = y;
        }

        private void Record(string s)
        {
            Say(s);
            _output.WriteLine(s);
        }
    }
}
