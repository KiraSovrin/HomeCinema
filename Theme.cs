using System.Drawing;

namespace Home_Cinema
{
    public class Theme
    {
        public string Name { get; set; } = string.Empty;
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color ButtonBackColor { get; set; }
        public Color ButtonForeColor { get; set; }
        public Color PanelBackColor { get; set; }
        public Font MainFont { get; set; } = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
    }
}
