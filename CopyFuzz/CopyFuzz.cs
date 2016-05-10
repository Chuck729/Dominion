using System;
using System.IO;
using GUI;
using GUI.Ui;
using RHFYP;

namespace CopyFuzz
{
    internal static class CopyFuzz
    {

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var seed = new Random().Next();
            var exit = false;
            GameUi.AnimationsOn = false;

            Welcome();

            while (!exit)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.ToLower();
                if (input == null) continue;
                switch (input.Split(' ')[0])
                {
                    case "learn":
                        Learn(seed);
                        break;
                    case "test":
                        Test(seed, int.Parse(input.Split(' ')[1]));
                        break;
                    case "exit":
                        exit = true;
                        break;
                    case "?":
                        Help();
                        break;
                    default:
                        Console.WriteLine($"\"{input}\" not a valid command.  Type \"?\" for commands.");
                        break;
                }
            }

            Exit();
        }

        private static void Learn(int seed)
        {
            var sw = new StreamWriter("learnedpaths.txt");
            var student = new StudentFuzzBall(new MainForm("bob", "larry", null, null, seed), sw);
        }

        private static void Test(int seed, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                var sr = new StreamReader("learnedpaths.txt");
                var tester = new TesterFuzzBall(new MainForm("bob", "larry", null, null, seed), sr);
                sr.Close();
            }
        }

        private static void Help()
        {
            Console.WriteLine("\nVALID INPUT | DESCRIPTION\n            |");
            Console.WriteLine("learn       | Starts the application and records user input");
            Console.WriteLine("test        | Starts the application and applys CopyFuzz");
            Console.WriteLine("exit        | Exits CopyFuzz");
            Console.WriteLine("?           | Help");
            Console.WriteLine("");
            Console.WriteLine("Commands are not case sensitive\n");
        }

        private static void Welcome()
        {
            Console.WriteLine("");
            Console.WriteLine("\tWelcome to CopyFuzz!");
            Console.WriteLine("\t--------------------");
            Help();
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting CopyFuzz...");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
