namespace Home_Cinema
{
    partial class HomeCinemaForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCards;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;

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
            btnSelectFolder = new Button();
            flowPanelCards = new FlowLayoutPanel();
            mainPanel = new Panel();
            btnMinimize = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(12, 12);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(150, 30);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Main Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // flowPanelCards
            // 
            flowPanelCards.AutoScroll = true;
            flowPanelCards.Location = new Point(12, 50);
            flowPanelCards.Name = "flowPanelCards";
            flowPanelCards.Size = new Size(776, 388);
            flowPanelCards.TabIndex = 1;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Location = new Point(12, 50);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(776, 388);
            mainPanel.TabIndex = 2;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.Location = new Point(800 - 70, 0); // 70px from right
            btnMinimize.Text = "_";
            btnMinimize.Name = "btnMinimize";
            btnMinimize.TabIndex = 3;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Size = new Size(30, 30);
            btnClose.Location = new Point(800 - 35, 0); // 35px from right
            btnClose.Text = "X";
            btnClose.Name = "btnClose";
            btnClose.TabIndex = 4;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // HomeCinemaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelectFolder);
            Controls.Add(mainPanel);
            Controls.Add(btnMinimize);
            Controls.Add(btnClose);
            Name = "HomeCinemaForm";
            Text = "Home Cinema";
            Load += HomeCinemaForm_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}
