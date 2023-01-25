namespace NexoLogger.Lib
{
    public class FileLogger : BaseLogger
    {
        private readonly string _logFileName;
        private readonly int _maxNumberOfArchives;
        private readonly int _maxFileSizeInBytes;
        private int _currentFileLength = 0;

        public FileLogger(string logFileName = "log", int maxNumberOfArchives = 9, int fileSizeInKB = 5)
        {
            if (String.IsNullOrWhiteSpace(logFileName))
                throw new ArgumentException("File name cannot be empty.", nameof(logFileName));

            if (maxNumberOfArchives < 0)
                throw new ArgumentException("Number of archives cannot be negative.", nameof(maxNumberOfArchives));

            if (fileSizeInKB <= 0)
                throw new ArgumentException("File size must be greater than 0 kilobytes", nameof(fileSizeInKB));

            _logFileName = logFileName;
            _maxNumberOfArchives = maxNumberOfArchives;
            _maxFileSizeInBytes = fileSizeInKB * 1024;
        }
        protected override void Log(string message, LogLevel logLevel)
        {
            if (message == null)
                throw new ArgumentException("The message cannot be null.", nameof(message));

            var formattedMessage = GetFormattedMessage(message, logLevel);
            if (formattedMessage.Length > _maxFileSizeInBytes)
                formattedMessage = formattedMessage.Substring(0, _maxFileSizeInBytes);

            if (_currentFileLength + formattedMessage.Length > _maxFileSizeInBytes)
                ArchiveFiles();

            var fileName = GetFileName(0);
            File.AppendAllLines(fileName, new List<string> { formattedMessage });
            _currentFileLength += formattedMessage.Length;
        }

        private void ArchiveFiles()
        {
            for(int i = _maxNumberOfArchives; i >= 0; i--) 
            {
                var fileName = GetFileName(i);
                if (File.Exists(fileName) && i < _maxNumberOfArchives)
                {
                    var newFileName = GetFileName(i + 1);
                    File.Move(fileName, newFileName, true);
                }
            }

            _currentFileLength = 0;
        }

        private string GetFileName(int archiveNumber)
        {
            if (archiveNumber == 0)
                return $"{_logFileName}.txt";

            return $"{_logFileName}.{archiveNumber}.txt";
        }
    }
}
