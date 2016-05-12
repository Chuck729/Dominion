using System;
using System.IO;

namespace CopyFuzz
{
    internal class StudentFuzzBall
    {

        private const float DistanceToRecordMousePoint = 8.0f;
        private readonly TextWriter _output;
        private readonly bool _print;
        private int _lastMouseX;
        private int _lastMouseY;

        /// <summary>
        ///     Launches the application form and listens to all input to record it.
        /// </summary>
        /// <param name="applicationStarter">A delegate that will create and return an instance of the form being tested.</param>
        /// <param name="output">The output stream this learner should print the results to.</param>
        /// <param name="seed">An optional seed to pass into the application starter to seed any random numbers.</param>
        public StudentFuzzBall(ApplicationStarter applicationStarter, TextWriter output, int seed = 0)
        {
            var application = applicationStarter.Invoke(seed);
            application.PreTest();
            _output = output;
            _print = true;

            _output.WriteLine($"session-{seed}");

            application.MouseClickEvent += (sender, args) => Record($"MouseClick-{args.X}-{args.Y}-{args.Button}");
            application.MouseDownEvent += (sender, args) => Record($"MouseDown-{args.X}-{args.Y}-{args.Button}");
            application.MouseUpEvent += (sender, args) => Record($"MouseUp-{args.X}-{args.Y}-{args.Button}");
            application.MouseMoveEvent += (sender, args) => RecordMouseMove(args.X, args.Y);
            application.KeyDownEvent += (sender, args) => Record($"KeyDown-{args.KeyCode}");

            application.Launch();

            if (_print) Console.WriteLine(@"Application closed.  Saving session...");
        }

        /// <summary>
        ///     Prints s to the console if printing an enabled.
        /// </summary>
        /// <param name="s">The stringyou want to say.</param>
        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        /// <summary>
        ///     Special case for MouseMove because otherwise it generate way too many events.
        /// </summary>
        /// <param name="x">Mouse move to x position.</param>
        /// <param name="y">Mouse move to y position.</param>
        private void RecordMouseMove(int x, int y)
        {
            var s = $"MouseMove-{x}-{y}";

            if (!(Math.Sqrt(Math.Pow(x - _lastMouseX, 2) + Math.Pow(y - _lastMouseY, 2)) > DistanceToRecordMousePoint))
                return;
            Record(s);
            _lastMouseX = x;
            _lastMouseY = y;
        }

        /// <summary>
        ///     Writes the string onto the output file.
        /// </summary>
        /// <param name="s">The string you want to berecorded.</param>
        private void Record(string s)
        {
            Say(s);
            _output.WriteLine(s);
        }
    }
}