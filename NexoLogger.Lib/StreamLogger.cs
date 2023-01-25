using System.Text;

namespace NexoLogger.Lib
{
    public class StreamLogger : BaseLogger, IDisposable
    {
        private readonly Stream _stream;
        private readonly StreamWriter _writer;
        private bool _disposedValue;

        public StreamLogger(Stream stream) 
        {
            _stream = stream;
            _writer = new StreamWriter(_stream, Encoding.Default);
            _writer.BaseStream.Seek(0, SeekOrigin.End);
        }
        protected override void Log(string message, LogLevel logLevel)
        {
            var formattedMessage = GetFormattedMessage(message, logLevel);
            _writer.WriteLine(formattedMessage);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_writer!= null) 
                        _writer.Close();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
