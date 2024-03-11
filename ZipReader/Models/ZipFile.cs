using ZipReader.Exceptions;

namespace ZipReader.Models
{
    public class ZipFile
    {
        private string _fileName;
        private readonly string[] _supportedExtensions = { ".zip", ".rar", ".7z" };

        public ZipFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ZipFileNotFoundException($"ZIP file '{fileName}' not found.");
            }

            if (!IsZipArchive(fileName))
            {
                throw new InvalidZipFileException($"File '{fileName}' is not a valid compressed file.");
            }

            _fileName = fileName;
        }

        protected bool IsZipArchive(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            return extension.Equals(".zip", StringComparison.OrdinalIgnoreCase) || IsSupportedArchive(fileName);
        }

        protected bool IsSupportedArchive(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            foreach (string ext in _supportedExtensions)
            {
                if (extension.Equals(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public long FileSize
        {
            get
            {
                FileInfo fileInfo = new FileInfo(_fileName);
                return fileInfo.Length;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                FileInfo fileInfo = new FileInfo(_fileName);
                return fileInfo.CreationTime;
            }
        }

        public int FileCount()
        {
            int count = 0;

            using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length >= 22)
                {
                    byte[] buffer = new byte[22];
                    fs.Seek(-22, SeekOrigin.End);
                    fs.Read(buffer, 0, 22);

                    if (BitConverter.ToUInt32(buffer, 0) == 0x06054B50)
                    {
                        ushort entryCount = BitConverter.ToUInt16(buffer, 10);
                        count = entryCount;
                    }
                }
            }

            return count;
        }
    }
}