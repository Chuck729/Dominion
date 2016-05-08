using System;
using System.Data.SqlTypes;
using System.IO;
using GUI;

namespace CopyFuzz
{
    internal static class Program
    {

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var seed = new Random().Next();
            var exit = false;

            Welcome();

            while (!exit)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.ToLower();
                switch (input)
                {
                    case "learn":
                        Learn(seed);
                        break;
                    case "test":
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
            var student = new StudentFuzzBall(new MainForm("bob", "larry", null, null, seed), TextWriter.Null);
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
