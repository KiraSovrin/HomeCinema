namespace Home_Cinema
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCards;
        private System.Windows.Forms.Panel mainPanel;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.flowPanelCards = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(150, 30);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "Select Main Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // flowPanelCards
            // 
            this.flowPanelCards.Location = new System.Drawing.Point(12, 50);
            this.flowPanelCards.Name = "flowPanelCards";
            this.flowPanelCards.Size = new System.Drawing.Size(776, 388);
            this.flowPanelCards.TabIndex = 1;
            this.flowPanelCards.AutoScroll = true;
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(12, 50);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(776, 388);
            this.mainPanel.TabIndex = 2;
            this.mainPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.mainPanel);
            this.Name = "Form1";
            this.Text = "Home Cinema";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
