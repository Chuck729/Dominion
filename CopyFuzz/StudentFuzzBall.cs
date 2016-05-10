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
        private int _lastLastMouseX;
        private int _lastLastMouseY;
        private int _lastMouseX;
        private int _lastMouseY;
        
        private float _distanceToRecordMousePoint = 4.0f;

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
            application.MouseMove += (sender, args) => RecordMouseMove(args.X, args.Y);
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

        private void RecordMouseMove(int x, int y)
        {
            var s = $"MouseMove-{x}-{y}";

            var angle = AngleFrom3PointsInDegrees(_lastLastMouseX, _lastLastMouseY, _lastMouseX, _lastMouseY, x, y);

            if (Math.Sqrt(Math.Pow(x - _lastMouseX, 2) + Math.Pow(y - _lastMouseY, 2)) > _distanceToRecordMousePoint)
            { 
                Record(s);
            }

            _lastLastMouseX = _lastMouseX;
            _lastLastMouseX = _lastMouseY;
            _lastMouseX = x;
            _lastMouseX = y;

        }

        private static double AngleFrom3PointsInDegrees(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var a = x2 - x1;
            var b = y2 - y1;
            var c = x3 - x2;
            var d = y3 - y2;

            var atanA = Math.Atan2(a, b);
            var atanB = Math.Atan2(c, d);

            return (atanA - atanB) * (-180 / Math.PI);
        }

        private void Record(string s)
        {
            Say(s);
            _output.WriteLine(s);
        }
    }
}
