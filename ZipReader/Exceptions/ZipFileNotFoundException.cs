namespace ZipReader.Exceptions
{
    public class ZipFileNotFoundException : Exception
    {
        public ZipFileNotFoundException(string message) : base(message)
        {
        }
    }
}