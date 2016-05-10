using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GUI;

namespace CopyFuzz
{
    public class TesterFuzzBall
    {
        private readonly MainForm _application;
        private readonly bool _print;
        private bool _inFuzz;
        private readonly double _clickBias = 0.8;
        private readonly Random _random = new Random();
        private readonly List<string> _actions = new List<string> {"click", "drag", "key press" };

        private readonly List<List<string>> _sessions = new List<List<string>>();

        private readonly List<List<int>> _knownClicks = new List<List<int>>();

        public TesterFuzzBall(MainForm application, TextReader textReader)
        {
            _application = application;
            _print = true;
            _inFuzz = false;

            LoadSessions(textReader);

            var thread = new Thread(ThreadRunner);
            thread.Start();

            Application.EnableVisualStyles();
            Application.Run(_application);

            Console.WriteLine(@"Ended");
        }

        private void LoadSessions(TextReader textReader)
        {
            var line = textReader.ReadLine();
            while (line != null)
            {
                if (line == "session")
                {
                    _sessions.Add(new List<string>());
                }
                else
                {
                    _sessions[_sessions.Count - 1].Add(line);
                    if(line.Substring(0, 10).Equals("MouseClick"))
                    {
                        string[] locations = line.Split('-');
                        int x;
                        int.TryParse(locations[1], out x);
                        int y;
                        int.TryParse(locations[2], out y);
                        _knownClicks.Add(new List<int> { x, y});
                        Console.WriteLine(@"Memorized click found at " + x + @" " + y);
                    }
                }

                line = textReader.ReadLine();
            }

        }

        private void ThreadRunner()
        {
            Thread.Sleep(1000);

            var rndSession = _random.Next(_sessions.Count);
            var rndStop = _random.Next(_sessions[rndSession].Count);
            for (var j = 0; j < rndStop; j++)
            {
                var splitLine = _sessions[rndSession][j].Split('-');
                ProcessCopyAction(splitLine);

                Thread.Sleep(splitLine[0] == "MoveMouse" ? 5 : 1);
            }

            Fuzz();

            _application.SimulateSendKey(new KeyEventArgs(Keys.Escape));
        }

        private void ProcessCopyAction(string[] args)
        {
            if (args.Length < 1) throw new InputSyntaxException($"No copy action has 0 arguments, given {args.Length}.");
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

        private void SimulateKeyDown(string[] args)
        {
            const int argCount = 2;
            if (args.Length != argCount) throw new InputSyntaxException($"Key down actions require {argCount} arguments (action-key), given {args.Length}");
            Keys key;
            if (!Enum.TryParse(args[1], out key)) throw new ParseException($"{args[1]} is not a valid key.");
            if (key == Keys.Escape && !_inFuzz) return;
            _application.SimulateSendKey(new KeyEventArgs(key));
        }

        private void SimulateMouseClick(string[] args)
        {
            const int argCount = 4;
            if (args.Length != argCount) throw new InputSyntaxException($"Mouse click actions require {argCount} arguments (action-x-y-button), given {args.Length}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button)) throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateClickMouse(new MouseEventArgs(button, 1, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void SimulateMouseMove(string[] args)
        {
            const int argCount = 3;
            if (args.Length != argCount) throw new InputSyntaxException($"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Length}");
            _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.None, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void SimulateMouseUp(string[] args)
        {
            const int argCount = 4;
            if (args.Length != argCount) throw new InputSyntaxException($"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Length}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button)) throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateMouseUp(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void SimulateMouseDown(string[] args)
        {
            const int argCount = 4;
            if (args.Length != argCount) throw new InputSyntaxException($"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Length}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button)) throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateMouseDown(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        /// <summary>
        /// Used to fuzz test
        /// </summary>
        private void Fuzz()
        {
            _inFuzz = true;

            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                var actionNumber = random.Next(0, _actions.Count);
                var action = _actions[actionNumber];

            var x1 = random.Next(0, _application.Width);
            var y1 = random.Next(0, _application.Height);
            var x2 = random.Next(0, _application.Width);
            var y2 = random.Next(0, _application.Height);

                if (action.Equals("click"))
                {
                    var prob = random.NextDouble();
                    if (prob < _clickBias)
                    {
                        var j = random.Next(0, _knownClicks.Count);
                        x1 = _knownClicks[j][0];
                        y1 = _knownClicks[j][1];
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
                else if (action.Equals("key press"))
                {
                    _application.SimulateSendKey(new KeyEventArgs(Keys.C));
                    Thread.Sleep(10);
                }
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
                _application.SimulateSendKey(new KeyEventArgs(Keys.C));
                Thread.Sleep(10);
            }
        }
    }

    internal class InputSyntaxException : Exception
    {
        public InputSyntaxException(string msg) : base(msg)
        {
            
        }
    }

    internal class ParseException : Exception
    {
        public ParseException(string msg) : base(msg)
        {

        }
    }


}
