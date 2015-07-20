
namespace MuleSoft.RAML.Tools.CustomEditor
{
    partial class EditorTextBox
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 0;
            // 
            // EditorTextBox
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBoxCtrl_MouseRecording);
            this.MouseEnter += new System.EventHandler(this.richTextBoxCtrl_MouseRecording);
            this.MouseLeave += new System.EventHandler(this.richTextBoxCtrl_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxCtrl_MouseRecording);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
