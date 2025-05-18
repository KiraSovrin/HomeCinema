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
            sideBar = new Panel();
            btnSettings = new Button();
            sideBar.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Cursor = Cursors.Hand;
            btnSelectFolder.Location = new Point(12, 3);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(122, 30);
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
            flowPanelCards.Size = new Size(800, 400);
            flowPanelCards.TabIndex = 1;
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.Transparent;
            mainPanel.Location = new Point(156, 39);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(5);
            mainPanel.Size = new Size(728, 522);
            mainPanel.TabIndex = 6;
            // 
            // btnMinimize
            // 
            btnMinimize.Location = new Point(816, 8);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(25, 25);
            btnMinimize.TabIndex = 7;
            btnMinimize.Click += BtnMinimize_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(847, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(25, 25);
            btnClose.TabIndex = 0;
            btnClose.Click += BtnClose_Click;
            // 
            // sideBar
            // 
            sideBar.BackColor = Color.Transparent;
            sideBar.Controls.Add(btnSettings);
            sideBar.Controls.Add(btnSelectFolder);
            sideBar.Location = new Point(0, 39);
            sideBar.Name = "sideBar";
            sideBar.Padding = new Padding(2);
            sideBar.Size = new Size(150, 522);
            sideBar.TabIndex = 5;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(12, 39);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(122, 33);
            btnSettings.TabIndex = 1;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // HomeCinemaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(btnClose);
            Controls.Add(btnMinimize);
            Controls.Add(sideBar);
            Controls.Add(mainPanel);
            Name = "HomeCinemaForm";
            Text = "Home Cinema";
            Load += HomeCinemaForm_Load;
            sideBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel sideBar;
        private Button btnSettings;
    }
}
