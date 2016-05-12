using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CopyFuzz
{
    internal class TesterFuzzBall
    {
        private const double PickKnownClickBias = 0.8;
        private const double PickKnownKeyBias = 1.0; // Crashes when < 1 because I think some key is closing the application (not escape?)
        private readonly int _fuzzIterations;
        private readonly List<string> _actions = new List<string> {"click", "drag", "key press"};
        private readonly ICopyFuzzifyer _application;

        private readonly Random _rnd = new Random();

        private readonly List<List<int>> _knownClicks = new List<List<int>>();
        private readonly List<Keys> _knownKeys = new List<Keys>();
        private readonly List<Keys> _ignoredKeys = new List<Keys>();
        private readonly Random _random = new Random();
        private readonly List<int> _seeds = new List<int>();

        private readonly List<List<string>> _sessions = new List<List<string>>();
        private bool _inFuzz;

        /// <summary>
        ///     The tester fuzz ball opens an instance of the application, picks a pre-recorded user session
        ///     from the collection the student fuzz ball generated, and then copys that pre-recorded session
        ///     for a while, before finally going into fuzz testing mode.
        /// </summary>
        /// <param name="applicationStarter">Delegate that will launch an instance of the application under test.</param>
        /// <param name="textReader">The file that the tester to learn from.</param>
        /// <param name="fuzz">The number of random inputs to apply after the desired depth has been reached.</param>
        public TesterFuzzBall(ApplicationStarter applicationStarter, TextReader textReader, int fuzz)
        {
            _ignoredKeys.Add(Keys.Escape);

            LoadSessions(textReader);
            _fuzzIterations = fuzz;
            var rndSession = _random.Next(_sessions.Count);

            _application = applicationStarter.Invoke(_seeds[rndSession]);
            _inFuzz = false;

            _application.PreTest();

            var thread = new Thread(ThreadRunner);
            thread.Start();

            _application.Launch();

            Console.WriteLine(@"Done testing...");
        }

        /// <summary>
        ///     This is the learning part of CopyFuzz.  It reads in all previous sessions and used them for context of where it
        ///     should try preforming an action.
        /// </summary>
        /// <param name="textReader">The file to read from.</param>
        private void LoadSessions(TextReader textReader)
        {
            var line = textReader.ReadLine();
            while (line != null)
            {
                var splitLine = line.Split('-');
                if (splitLine[0].Equals("session"))
                {
                    _sessions.Add(new List<string>());
                    int seed;
                    if (splitLine.Length < 2) throw new ParseException("session line doesn't have a seed.");
                    if (!int.TryParse(splitLine[1], out seed)) throw new ParseException("Seed must be an integer.");
                    _seeds.Add(seed);
                }
                else
                {
                    _sessions[_sessions.Count - 1].Add(line);
                    if (splitLine[0].Equals("MouseClick"))
                    {
                        int x, y;
                        if (!int.TryParse(splitLine[1], out x))
                            throw new ParseException("Mouse click x position must be an integer.");
                        if (!int.TryParse(splitLine[2], out y))
                            throw new ParseException("Mouse click y position must be an integer.");
                        _knownClicks.Add(new List<int> {x, y});
                        Console.WriteLine(@"Memorized click found at " + x + @" " + y);
                    }
                    if (splitLine[0].Equals("KeyDown"))
                    {
                        Keys key;
                        if (!Enum.TryParse(splitLine[1], out key)) throw new ParseException($"{splitLine[1]} is not a valid key.");
                        if (key != Keys.Escape)
                        {
                            Console.WriteLine($"Memorized key {key}");
                            _knownKeys.Add(key);
                        }
                    }
                }

                line = textReader.ReadLine();
            }
        }

        /// <summary>
        ///     This should be run right before the application loop is started on the original TesterFuzzBall thread.
        /// </summary>
        private void ThreadRunner()
        {
            Thread.Sleep(10);

            var rndSession = _random.Next(_sessions.Count);
            if (_sessions[rndSession].Count > 5)
            {
                var rndStop = _random.Next(_sessions[rndSession].Count - 1);
                for (var j = 0; j < rndStop; j++)
                {
                    var splitLine = _sessions[rndSession][j].Split('-');
                    ProcessCopyAction(splitLine);

                    Thread.Sleep(splitLine[0] == "MoveMouse" ? 5 : 1);
                }
            }

            Fuzz();

            _application.SimulateSendKey(new KeyEventArgs(Keys.Escape));
        }

        /// <summary>
        ///     Proforms the proper steps depending on what action the tester is trying to copy.
        /// </summary>
        /// <param name="args">The split up arguments of the action.</param>
        private void ProcessCopyAction(IReadOnlyList<string> args)
        {
            if (args.Count < 1)
                throw new InputSyntaxException($"No copy action has 0 arguments, given {args.Count}.");
            switch (args[0])
            {
                case "MouseClick":
                    SimulateMouseClick(args);
                    break;
                case "MouseUp":
                    SimulateMouseUp(args);
                    break;
                case "MouseDown":
                    SimulateMouseDown(args);
                    break;
                case "KeyDown":
                    SimulateKeyDown(args);
                    break;
                case "MouseMove":
                    SimulateMouseMove(args);
                    break;
                default:
                    throw new InputSyntaxException($"\"{args[0]}\" is not a valid action.");
            }
        }

        /// <summary>
        ///     Error handling and calling the interface for key down events.
        /// </summary>
        /// <param name="args">Action arguments e.g. {"KeyDown", "Keys.C"}</param>
        private void SimulateKeyDown(IReadOnlyList<string> args)
        {
            const int argCount = 2;
            if (args.Count != argCount)
                throw new InputSyntaxException(
                    $"Key down actions require {argCount} arguments (action-key), given {args.Count}");
            Keys key;
            if (!Enum.TryParse(args[1], out key)) throw new ParseException($"{args[1]} is not a valid key.");
            if (!_ignoredKeys.Contains(key) && !_inFuzz) return;
            _application.SimulateSendKey(new KeyEventArgs(key));
        }

        /// <summary>
        ///     Error handling and calling the interface for mouse click events.
        /// </summary>
        /// <param name="args">Action arguments e.g. {"MouseClick", "x", "y", "button"}</param>
        private void SimulateMouseClick(IReadOnlyList<string> args)
        {
            const int argCount = 4;
            if (args.Count != argCount)
                throw new InputSyntaxException(
                    $"Mouse click actions require {argCount} arguments (action-x-y-button), given {args.Count}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button))
                throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateClickMouse(new MouseEventArgs(button, 1, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        /// <summary>
        ///     Error handling and calling the interface for mouse move events.
        /// </summary>
        /// <param name="args">Action arguments e.g. {"MouseClick", "x", "y", "button"}</param>
        private void SimulateMouseMove(IReadOnlyList<string> args)
        {
            const int argCount = 3;
            if (args.Count != argCount)
                throw new InputSyntaxException(
                    $"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Count}");
            _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.None, 0, int.Parse(args[1]),
                int.Parse(args[2]), 0));
        }

        /// <summary>
        ///     Error handling and calling the interface for mouse up..
        /// </summary>
        /// <param name="args">Action arguments e.g. {"MouseClick", "x", "y", "button"}</param>
        private void SimulateMouseUp(IReadOnlyList<string> args)
        {
            const int argCount = 4;
            if (args.Count != argCount)
                throw new InputSyntaxException(
                    $"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Count}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button))
                throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateMouseUp(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        /// <summary>
        ///     Error handling and calling the interface for mouse down.
        /// </summary>
        /// <param name="args">Action arguments e.g. {"MouseClick", "x", "y", "button"}</param>
        private void SimulateMouseDown(IReadOnlyList<string> args)
        {
            const int argCount = 4;
            if (args.Count != argCount)
                throw new InputSyntaxException(
                    $"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Count}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button))
                throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateMouseDown(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        /// <summary>
        ///     Used to fuzz test
        /// </summary>
        private void Fuzz()
        {
            _inFuzz = true;
            var keys = Enum.GetValues(typeof(Keys));

            for (var i = 0; i < _fuzzIterations; i++)
            {
                var actionNumber = _rnd.Next(0, _actions.Count);
                var action = _actions[actionNumber];

                var x1 = _rnd.Next(0, _application.MouseValidXRange);
                var y1 = _rnd.Next(0, _application.MouseValidYRange);
                var x2 = _rnd.Next(0, _application.MouseValidXRange);
                var y2 = _rnd.Next(0, _application.MouseValidYRange);

                if (action.Equals("click"))
                {
                    var prob = _rnd.NextDouble();
                    if (prob < PickKnownClickBias)
                    {
                        if (_knownClicks.Count != 0)
                        {
                            var j = _rnd.Next(0, _knownClicks.Count);
                            x1 = _knownClicks[j][0];
                            y1 = _knownClicks[j][1];
                        }
                    }
                    _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.None, 0, x1, y1, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseDown(new MouseEventArgs(MouseButtons.Left, 0, x1, y1, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseUp(new MouseEventArgs(MouseButtons.Left, 0, x1, y1, 0));
                    Thread.Sleep(10);
                }
                else if (action.Equals("drag"))
                {
                    _application.SimulateMouseDown(new MouseEventArgs(MouseButtons.Left, 0, x1, y1, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.None, 0, x2, y2, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseUp(new MouseEventArgs(MouseButtons.Left, 0, x2, y2, 0));
                    Thread.Sleep(10);
                }
                else if (action.Equals("drag"))
                {
                    _application.SimulateMouseDown(new MouseEventArgs(MouseButtons.Left, 0, x1, y1, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.None, 0, x2, y2, 0));
                    Thread.Sleep(10);
                    _application.SimulateMouseUp(new MouseEventArgs(MouseButtons.Left, 0, x2, y2, 0));
                    Thread.Sleep(10);
                }
                else if (action.Equals("key press"))
                {
                    var prob = _rnd.NextDouble();

                    if (prob < PickKnownKeyBias && _knownKeys.Count > 0)
                    {
                        var key = _knownKeys[_rnd.Next(_knownKeys.Count)];
                        if (!_ignoredKeys.Contains(key)) _application.SimulateSendKey(new KeyEventArgs(key));
                    }
                    else
                    {
                        var key = (Keys)keys.GetValue(_rnd.Next(keys.Length));
                        if (!_ignoredKeys.Contains(key)) _application.SimulateSendKey(new KeyEventArgs(key));
                    }

                    Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        ///     Thrown when the user inputs incorrct dat into the Console.
        /// </summary>
        private class InputSyntaxException : Exception
        {
            public InputSyntaxException(string msg) : base(msg)
            {
            }
        }

        /// <summary>
        ///     Thrown when the Studen and tester disagree on what the file format should be.
        /// </summary>
        private class ParseException : Exception
        {
            public ParseException(string msg) : base(msg)
            {
            }
        }
    }
}