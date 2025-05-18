using System.Drawing;
using System.Windows.Forms;

namespace Home_Cinema
{
    public class MediaCardControl : UserControl
    {
        // Use fields instead of public properties to avoid WinForms designer serialization warnings
        public PictureBox coverImage;
        public Label titleLabel;

        public MediaCardControl()
        {
            this.Width = 250;
            this.Height = 300;
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

            coverImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };
            this.Controls.Add(coverImage);

            titleLabel = new Label
            {
                AutoSize = false,
                Width = this.Width - 16,
                Height = 32,
                Top = this.Height - 48,
                Left = 8,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 210, 210, 210),
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(titleLabel);
            titleLabel.BringToFront();

        }
    }
}
