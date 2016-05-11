using System;
using System.Collections.Generic;
using System.IO;

namespace CopyFuzz
{
    /// <summary>
    ///     Starts a single instance of the application under test.
    /// </summary>
    /// <param name="seed">Seed for random number generators.</param>
    /// <returns>An <see cref="ICopyFuzzifyer" /> object that FuzzBalls can manipulate.</returns>
    public delegate ICopyFuzzifyer ApplicationStarter(int seed);

    /// <summary>
    ///     A CopyFuzz tester object.  When given a function that will generate an <see cref="ICopyFuzzifyer" /> object
    ///     handles console I/O.
    /// </summary>
    public class CopyFuzz
    {
        private const string Prompt = " > ";
        private readonly ApplicationStarter _applicationStarter;
        private int _fuzzIterations = 100;
        private string _path = "learnedpaths.txt";

        /// <summary>
        ///     Starting point for CopyFuzz testing.
        /// </summary>
        /// <param name="applicationStarter">Delegate that will create a new instance of the application under test.</param>
        public CopyFuzz(ApplicationStarter applicationStarter)
        {
            Welcome();
            _applicationStarter = applicationStarter;

            var seed = new Random().Next();
            var exit = false;

            while (!exit)
            {
                Console.Write(Prompt);
                var input = Console.ReadLine()?.ToLower();
                if (input == null) continue;
                var splinput = input.Split(' ');

                switch (splinput[0])
                {
                    case "learn":
                        Learn(seed);
                        break;
                    case "test":
                        Test(splinput.Length != 2 ? 1 : int.Parse(splinput[1]));
                        break;
                    case "path":
                        SetPath(splinput);
                        break;
                    case "reset":
                        File.Delete(_path);
                        break;
                    case "exit":
                        exit = true;
                        break;
                    case "param":
                        Params(splinput);
                        break;
                    case "?":
                        Help();
                        break;
                    case "":
                        break;
                    default:
                        Console.WriteLine($"'{input}' not a valid command.  '?' for help");
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the path of the file the copyfuzz reads and writes to.
        /// </summary>
        /// <param name="splinput">Split input arguments.</param>
        /// <remarks>Just a text file.</remarks>
        private void SetPath(IReadOnlyList<string> splinput)
        {
            if (splinput.Count < 2)
            {
                Console.WriteLine(@"path requires a path argument");
                return;
            }
            if (!File.Exists(splinput[1]))
            {
                Console.WriteLine(@"file does not exist");
                return;
            }

            _path = splinput[1];
            Console.WriteLine($"path set to {_path}");
        }

        /// <summary>
        ///     Starts CopyFuzz in learning mode, where it will listen to and record your input as you move through the
        ///     application.
        /// </summary>
        /// <param name="seed">The seed to start the application with.</param>
        private void Learn(int seed)
        {
            var sw = new StreamWriter(_path, true);
            // ReSharper disable once UnusedVariable
            var student = new StudentFuzzBall(_applicationStarter, sw, seed);
            sw.Close();
        }

        /// <summary>
        ///     Starts CopyFuzz in testing mode, where it will begin randomly providing inputs to the application.
        /// </summary>
        /// <param name="iterations">How many time you want to run testing mode.</param>
        private void Test(int iterations)
        {
            if (!File.Exists(_path))
            {
                Console.WriteLine(@"No user session data found");
                Console.WriteLine(@"Run CopyFuzz on your application in learn mode first");
                return;
            }

            for (var i = 0; i < iterations; i++)
            {
                var sr = new StreamReader(_path);
                // ReSharper disable once UnusedVariable
                var tester = new TesterFuzzBall(_applicationStarter, sr, _fuzzIterations);
                sr.Close();
            }
        }

        /// <summary>
        ///     Prints help.
        /// </summary>
        private static void Help()
        {
            Console.WriteLine(@"");
            Console.WriteLine(@"cmd                 desc");
            Console.WriteLine(@"");
            Console.WriteLine(@"learn               starts the application and records user input");
            Console.WriteLine(@"test [n]            starts the application n times and applys copyfuzz");
            Console.WriteLine(@"reset               deletes the file containing user sessions");
            Console.WriteLine(@"path p              sets the path of the file copyfuzz stored learned data");
            Console.WriteLine(@"param [n] [v]       settings and paramaters used while testing");
            Console.WriteLine(@"exit");
            Console.WriteLine(@"?");
            Console.WriteLine(@"");
        }

        /// <summary>
        ///     Prints "splash screen".
        /// </summary>
        private static void Welcome()
        {
            Console.WriteLine(@"");
            Console.WriteLine(@"");
            Console.WriteLine(@"CopyFuzz v0.1");
            Console.WriteLine(@"");
        }

        /// <summary>
        ///     Handles reading and setting of various testing parameters.
        /// </summary>
        /// <param name="args">The arguments that the user called 'param' with.</param>
        private void Params(IReadOnlyList<string> args)
        {
            Console.WriteLine(@"");
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (args.Count == 1)
            {
                Console.WriteLine(@"usage: param [name] [value]");
                Console.WriteLine(@"");
                Console.WriteLine(@"name      value");
                Console.Write(MakeConstWidthString("fuzz", 10));
                Console.WriteLine(_fuzzIterations);
            }
            else if (args.Count == 2)
            {
                if (args[1] == "fuzz")
                {
                    Console.Write(MakeConstWidthString("fuzz", 10));
                    Console.WriteLine(_fuzzIterations);
                }
            }
            else if (args.Count == 3)
            {
                int n;
                if (args[1] == "fuzz" && int.TryParse(args[2], out n))
                {
                    Console.Write($"changed fuzz from {_fuzzIterations} to {n}");
                    _fuzzIterations = n;
                }
            }
            Console.WriteLine(@"");
        }

        /// <summary>
        ///     Forces the given string to be the given width.
        /// </summary>
        /// <param name="string">Original string.</param>
        /// <param name="width">Desired width.</param>
        /// <returns>A new string that is either a substring of the original or the orinial concated with spaces.</returns>
        private static string MakeConstWidthString(string @string, int width)
        {
            while (@string.Length < width) @string += " ";
            return @string.Length > width ? @string.Substring(0, width) : @string;
        }
    }
}