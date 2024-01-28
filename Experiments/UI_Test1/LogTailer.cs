using System;
using System.IO;
using Utilities;
using static UI_Test1.LogTailer;

namespace UI_Test1
{
    /// <summary>
    /// Observes a file allowing you to tail it.
    /// </summary>
    internal class LogTailer
    {
        #region Callbacks

        /// <summary>
        /// Data provides with log tail callbacks.
        /// </summary>
        public class CallbackInfo
        {
            public long FileLength { get; set; }
            public char[] CharBuffer { get; set; }
        }

        /// <summary>
        /// Callback for log tail updates.
        /// </summary>
        public delegate void Callback(CallbackInfo callbackInfo);

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public LogTailer(string path, Callback callback)
        {
            m_path = path;
            m_callback = callback;

            // We run a timer for checking for file updates...
            m_timer = new System.Timers.Timer(100);
            m_timer.Elapsed += onTimerElapsed;
            m_timer.AutoReset = true;
            m_timer.Enabled = true;
        }

        /// <summary>
        /// Starts following the tail.
        /// </summary>
        public void followTail()
        {

        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We clean up the timer...
            m_timer.Elapsed -= onTimerElapsed;
            m_timer.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private functions

        /// <summary>
        /// Called (on the timer thread) when the timer ticks.
        /// </summary>
        private void onTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                CallbackInfo callbackInfo = null;
                using (var fileStream = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite))
                {
                    // If this is the first time this is called we note the file length...
                    if(m_fileLength == -1)
                    {
                        m_fileLength = fileStream.Length;
                    }

                    // We check if the file length has changed...
                    var newLength = fileStream.Length;
                    if(newLength == m_fileLength)
                    {
                        return;
                    }

                    // We read data from last position we saw to the new position...
                    using (var streamReader  = new StreamReader(fileStream))
                    {
                        var numChars = (int)(newLength - m_fileLength);
                        callbackInfo = new CallbackInfo();
                        callbackInfo.CharBuffer = new char[numChars];
                        streamReader.BaseStream.Seek(m_fileLength, SeekOrigin.Begin);
                        streamReader.ReadBlock(callbackInfo.CharBuffer, 0, numChars);
                    }

                    // We note the new file length...
                    m_fileLength = newLength;
                }

                // We call back to the client...
                if(callbackInfo != null)
                {
                    m_callback(callbackInfo);
                }

            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly string m_path;
        private readonly Callback m_callback;

        // We check for tail updates on a timer...
        private readonly System.Timers.Timer m_timer;

        // The file length...
        private long m_fileLength = -1;

        #endregion
    }
}
