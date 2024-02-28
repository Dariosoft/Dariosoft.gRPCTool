using System.Text;

namespace Dariosoft.TestConsole.V2
{

    public class MultiTextWriter : TextWriter, gRPCTool.V2.Utilities.IOuput
    {
        private readonly TextWriter[] writers;

        public MultiTextWriter(params TextWriter[] writers)
        {
            this.writers = writers ?? throw new ArgumentNullException(nameof(writers));
        }

        public override Encoding Encoding => Encoding.Default;


        #region Write
        public override void Write(char value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].Write(value);
        }

        public override void Write(string? value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].Write(value);
        }
        #endregion

        #region WriteLine
        public override void WriteLine()
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].WriteLine();
        }

        public override void WriteLine(bool value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].WriteLine(value);
        }

        public override void WriteLine(char value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].WriteLine(value);
        }

        public override void WriteLine(string? value)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].WriteLine(value);
        }
        #endregion

        #region Flush
        public override void Flush()
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].Flush();
        }

        public override Task FlushAsync()
            => Task.WhenAll(writers.Select(w => w.FlushAsync()));

        public override Task FlushAsync(CancellationToken cancellationToken)
            => Task.WhenAll(writers.Select(w => w.FlushAsync(cancellationToken)));
        #endregion

        public override void Close()
        {
            for (var i = 0; i < writers.Length; i++)
                writers[i].Close();
        }

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            for (int i = 0; i < writers.Length; i++)
                writers[i].Dispose();
        }


        public override async ValueTask DisposeAsync()
        {

            for (int i = 0; i < writers.Length; i++)
                await writers[i].DisposeAsync();
        } 
        #endregion
    }
}
