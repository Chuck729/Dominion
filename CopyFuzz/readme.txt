
CopyFuzz is a fuzz testing tool that records user sessions though 
mouse and keyboard inputs in order to gain an understanding of the 
application.  When CopyFuzz tests, it first randomly selects a 
recorded user session, then randomly selects a depth to copy that
session to.  CopyFuzz will simulate all mouse and keyboard actions
to the randomly chosen depth and then begin fuzz testing.

To use CopyFuzz:

1. Include the CopyFuzz project in your solution
2. Add a reference from the class you want to test the CopyFuzz project
3. Implement the ICopyFuzzifiy interface into the class you want to test
4. Create a console application
5. Create a CopyFuzz object with the appropite factory delegate to begin testing:

	private static void Main(string[] args)
    {
        new CopyFuzz.CopyFuzz(seed => new Form(seed), seed));
    }