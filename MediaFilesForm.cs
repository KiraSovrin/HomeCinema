using System.IO;

namespace Home_Cinema
{
    public partial class MediaFilesForm : Form
    {
        private string folderPath;
        private ListBox listBoxFiles = null!; // Initialize with null-forgiving operator to satisfy the compiler

        public MediaFilesForm(string folderPath)
        {
            this.folderPath = folderPath;
            InitializeComponent();
            LoadFiles();
        }

        private void InitializeComponent()
        {
            this.listBoxFiles = new ListBox();
            this.SuspendLayout();
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Dock = DockStyle.Fill;
            this.listBoxFiles.Font = new Font("Segoe UI", 12F);
            this.listBoxFiles.ItemHeight = 24;
            //this.listBoxFiles.DoubleClick += ListBoxFiles_DoubleClick!;
            // 
            // MediaFilesForm
            // 
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.listBoxFiles);
            //this.Text = $"Files in {Path.GetFileName(folderPath)}";
            this.ResumeLayout(false);
        }

        private void LoadFiles()
        {
            var videoExtensions = new[] { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv" };
            var files = Directory.GetFiles(folderPath)
                .Where(f => videoExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToArray();
            listBoxFiles.Items.Clear();
            foreach (var file in files)
            {
                listBoxFiles.Items.Add(Path.GetFileName(file));
            }
        }

        private void ListBoxFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                string? fileName = listBoxFiles.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(fileName))
                {
                    string fullPath = Path.Combine(folderPath, fileName);
                    MessageBox.Show($"Selected: {fullPath}"); // Placeholder for playback
                }
            }
        }
    }
}
