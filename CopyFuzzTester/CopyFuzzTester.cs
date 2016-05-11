using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using RHFYP;

namespace CopyFuzzTester
{
    class CopyFuzzTester
    {
        static void Main(string[] args)
        {
            var copyFuzz = new CopyFuzz.CopyFuzz((seed) => new MainForm(new[] {"bob", "larry"}, seed));
        }
    }
}
