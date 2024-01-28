namespace UI_Test1
{
    partial class AnotherLogTailer_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnotherLogTailer_Form));
            this.ctrlGroupBox_Settings = new System.Windows.Forms.GroupBox();
            this.ctrlGroupBox_Log = new System.Windows.Forms.GroupBox();
            this.ctrlDropTarget = new System.Windows.Forms.GroupBox();
            this.ctrlLog = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ctrlScrollBar = new System.Windows.Forms.VScrollBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.ctrlGroupBox_Settings.SuspendLayout();
            this.ctrlGroupBox_Log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlLog)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlGroupBox_Settings
            // 
            this.ctrlGroupBox_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlGroupBox_Settings.Controls.Add(this.lblInfo);
            this.ctrlGroupBox_Settings.Controls.Add(this.ctrlDropTarget);
            this.ctrlGroupBox_Settings.Location = new System.Drawing.Point(12, 9);
            this.ctrlGroupBox_Settings.Name = "ctrlGroupBox_Settings";
            this.ctrlGroupBox_Settings.Padding = new System.Windows.Forms.Padding(0);
            this.ctrlGroupBox_Settings.Size = new System.Drawing.Size(978, 114);
            this.ctrlGroupBox_Settings.TabIndex = 0;
            this.ctrlGroupBox_Settings.TabStop = false;
            this.ctrlGroupBox_Settings.Text = "Settings";
            // 
            // ctrlGroupBox_Log
            // 
            this.ctrlGroupBox_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlGroupBox_Log.Controls.Add(this.ctrlScrollBar);
            this.ctrlGroupBox_Log.Controls.Add(this.ctrlLog);
            this.ctrlGroupBox_Log.Location = new System.Drawing.Point(12, 129);
            this.ctrlGroupBox_Log.Name = "ctrlGroupBox_Log";
            this.ctrlGroupBox_Log.Size = new System.Drawing.Size(978, 480);
            this.ctrlGroupBox_Log.TabIndex = 1;
            this.ctrlGroupBox_Log.TabStop = false;
            this.ctrlGroupBox_Log.Text = "Log";
            // 
            // ctrlDropTarget
            // 
            this.ctrlDropTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlDropTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ctrlDropTarget.Location = new System.Drawing.Point(860, 16);
            this.ctrlDropTarget.Name = "ctrlDropTarget";
            this.ctrlDropTarget.Size = new System.Drawing.Size(112, 89);
            this.ctrlDropTarget.TabIndex = 0;
            this.ctrlDropTarget.TabStop = false;
            this.ctrlDropTarget.Text = "Drop file here";
            this.ctrlDropTarget.DragDrop += new System.Windows.Forms.DragEventHandler(this.ctrlDropTarget_DragDrop);
            this.ctrlDropTarget.DragOver += new System.Windows.Forms.DragEventHandler(this.ctrlDropTarget_DragOver);
            // 
            // ctrlLog
            // 
            this.ctrlLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlLog.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.ctrlLog.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.ctrlLog.BackBrush = null;
            this.ctrlLog.CharHeight = 14;
            this.ctrlLog.CharWidth = 8;
            this.ctrlLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ctrlLog.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ctrlLog.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.ctrlLog.IsReplaceMode = false;
            this.ctrlLog.Location = new System.Drawing.Point(6, 19);
            this.ctrlLog.Name = "ctrlLog";
            this.ctrlLog.Paddings = new System.Windows.Forms.Padding(0);
            this.ctrlLog.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ctrlLog.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ctrlLog.ServiceColors")));
            this.ctrlLog.ShowLineNumbers = false;
            this.ctrlLog.ShowScrollBars = false;
            this.ctrlLog.Size = new System.Drawing.Size(945, 455);
            this.ctrlLog.TabIndex = 0;
            this.ctrlLog.Zoom = 100;
            // 
            // ctrlScrollBar
            // 
            this.ctrlScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlScrollBar.Location = new System.Drawing.Point(951, 19);
            this.ctrlScrollBar.Maximum = 1000000;
            this.ctrlScrollBar.Name = "ctrlScrollBar";
            this.ctrlScrollBar.Size = new System.Drawing.Size(20, 455);
            this.ctrlScrollBar.TabIndex = 1;
            this.ctrlScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ctrlScrollBar_Scroll);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(684, 13);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "label1";
            // 
            // AnotherLogTailer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 621);
            this.Controls.Add(this.ctrlGroupBox_Log);
            this.Controls.Add(this.ctrlGroupBox_Settings);
            this.Name = "AnotherLogTailer_Form";
            this.Text = "ALT (Another Log Tailer)";
            this.ctrlGroupBox_Settings.ResumeLayout(false);
            this.ctrlGroupBox_Settings.PerformLayout();
            this.ctrlGroupBox_Log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ctrlGroupBox_Settings;
        private System.Windows.Forms.GroupBox ctrlGroupBox_Log;
        private System.Windows.Forms.GroupBox ctrlDropTarget;
        private FastColoredTextBoxNS.FastColoredTextBox ctrlLog;
        private System.Windows.Forms.VScrollBar ctrlScrollBar;
        private System.Windows.Forms.Label lblInfo;
    }
}

