using System;
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
        private const string Prompt = "> ";
        private const string Path = "learnedpaths.txt";
        private readonly ApplicationStarter _applicationStarter;

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
                        if (splinput.Length != 2) Test(1);
                        Test(int.Parse(splinput[1]));
                        break;
                    case "reset":
                        File.Delete(Path);
                        break;
                    case "exit":
                        exit = true;
                        break;
                    case "?":
                        Help();
                        break;
                    default:
                        Console.WriteLine($"'{input}' not a valid command.  '?' for help");
                        break;
                }
            }
        }

        /// <summary>
        ///     Starts CopyFuzz in learning mode, where it will listen to and record your input as you move through the
        ///     application.
        /// </summary>
        /// <param name="seed">The seed to start the application with.</param>
        private void Learn(int seed)
        {
            var sw = new StreamWriter(Path, true);
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
            for (var i = 0; i < iterations; i++)
            {
                var sr = new StreamReader(Path);
                // ReSharper disable once UnusedVariable
                var tester = new TesterFuzzBall(_applicationStarter, sr);
                sr.Close();
            }
        }

        /// <summary>
        ///     Prints help.
        /// </summary>
        private static void Help()
        {
            Console.WriteLine(@"");
            Console.WriteLine(@"cmd       desc");
            Console.WriteLine(@"");
            Console.WriteLine(@"learn     Starts the application and records user input");
            Console.WriteLine(@"test      Starts the application and applys CopyFuzz");
            Console.WriteLine(@"exit      ");
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
    }
}