using System;
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
                MessageBox.Show(String.Join("; ", files));
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

        #endregion
    }
}
