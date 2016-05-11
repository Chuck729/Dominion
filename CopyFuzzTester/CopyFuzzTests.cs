using GUI;

namespace CopyFuzzTester
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CopyFuzzTests
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CopyFuzz.CopyFuzz(seed => new MainForm(new[] {"bob", "larry"}, seed));
        }
    }
}