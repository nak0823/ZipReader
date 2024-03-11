using ZipReader.Models;

namespace ZipReader.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ZipFile zipFile = new ZipFile(@"C:\Users\AnotherPC\Downloads\TestZipFile.zip");
            Console.WriteLine(zipFile.FileSize);
            Console.WriteLine(zipFile.CreationTime);
            Console.WriteLine(zipFile.FileCount());
            Console.ReadKey();
        }
    }
}