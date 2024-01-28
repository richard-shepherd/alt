using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UI_Test1
{
    /// <summary>
    /// First go at writing a maybe scrappy tailer.
    /// </summary>
    public partial class AnotherLogTailer_Form : Form
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public AnotherLogTailer_Form()
        {
            InitializeComponent();
            ctrlDropTarget.AllowDrop = true;
        }

        #endregion

        #region Form events

        /// <summary>
        /// Enables drag-drop for the drop-target box.
        /// </summary>
        private void ctrlDropTarget_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.All;
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        /// <summary>
        /// Called when files are dropped onto the drop-target.
        /// </summary>
        private void ctrlDropTarget_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if(files.Length == 1)
                {
                    cleanupLogTailer();
                    ctrlLog.Clear();
                    m_logTailer = new LogTailer(files[0], onLogTailerCallback);
                }
                
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        /// <summary>
        /// Called when the scroll bar has been scrolled.
        /// </summary>
        private void ctrlScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                lblInfo.Text = e.NewValue.ToString();
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Shows an error in a message box.
        /// </summary>
        private void logError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        /// <summary>
        /// Cleans up the active log tailer (if there is one).
        /// </summary>
        private void cleanupLogTailer()
        {
            m_logTailer?.Dispose();
            m_logTailer = null;
        }

        /// <summary>
        /// Called when we get an update from the log tailer.
        /// Threading: This is called on a non-UI thread.
        /// </summary>
        private void onLogTailerCallback(LogTailer.CallbackInfo callbackInfo)
        {
            try
            {
                marshalToUIThread(() =>
                {
                    onLogTailerCallback_UIThread(callbackInfo);
                });
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }
        private void onLogTailerCallback_UIThread(LogTailer.CallbackInfo callbackInfo)
        {
            try
            {
                while (m_lines.Count > 50)
                {
                    m_lines.RemoveFirst();
                }
                var s = new String(callbackInfo.CharBuffer);
                var newLines = s.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in newLines)
                {
                    m_lines.AddLast(line);
                }
                ctrlLog.Clear();
                foreach(var line in m_lines)
                {
                    ctrlLog.AppendText($"{line}{Environment.NewLine}");
                }
                ctrlLog.Navigate(ctrlLog.LinesCount - 1);
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        private void marshalToUIThread(Action action)
        {
            BeginInvoke(new Action<Action>(x => x()), new object[] { action });
        }

        #endregion

        #region Private data

        // Observes a file...
        private LogTailer m_logTailer = null;

        private LinkedList<string> m_lines = new LinkedList<string>();

        #endregion
    }
}
