using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GUI;
using GUI.Ui;

namespace CopyFuzz
{
    public class TesterFuzzBall
    {
        private readonly MainForm _application;
        private readonly bool _print;

        private readonly List<List<string>> _sessions = new List<List<string>>();

        public TesterFuzzBall(MainForm application, TextReader textReader)
        {
            _application = application;
            _print = true;

            LoadSessions(textReader);

            var thread = new Thread(Test);
            thread.Start();

            Application.EnableVisualStyles();
            Application.Run(_application);

            Console.WriteLine("Ended");
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
                }

                line = textReader.ReadLine();
            }
        }

        private void Test()
        {
            Thread.Sleep(2000);

            if (_sessions.Count == 1)
            {
                foreach (var line in _sessions[0])
                {
                    var splitLine = line.Split('-');
                    ProcessCopyAction(splitLine);

                    Thread.Sleep(splitLine[0] == "MoveMouse" ? 10 : 1);
                }
            }

            // TODO: Step 2:
            // Random testing
            fuzz();
            //_application.SendKey(new KeyEventArgs(Keys.Escape));
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
            _application.SimulateMouseMove(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void SimulateMouseDown(string[] args)
        {
            const int argCount = 4;
            if (args.Length != argCount) throw new InputSyntaxException($"Mouse move actions require {argCount} arguments (action-x-y-button), given {args.Length}");
            MouseButtons button;
            if (!Enum.TryParse(args[3], out button)) throw new ParseException($"{args[3]} is not a valid mouse button.");
            _application.SimulateMouseMove(new MouseEventArgs(button, 0, int.Parse(args[1]), int.Parse(args[2]), 0));
        }

        private void Say(string s)
        {
            if (_print) Console.WriteLine(s);
        }

        /// <summary>
        /// Used to fuzz test
        /// </summary>
        private void fuzz()
        {
            _application.SimulateMouseDown(new MouseEventArgs(MouseButtons.Left, 0, 200, 200, 0));
            Thread.Sleep(20);
            _application.SimulateMouseMove(new MouseEventArgs(MouseButtons.Left, 0, 50, 50, 0));
            Thread.Sleep(20);
            _application.SimulateMouseUp(new MouseEventArgs(MouseButtons.Left, 0, 50, 50, 0));
            Thread.Sleep(1000);
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
