using System.Drawing;
using System.Text.Json.Serialization;

namespace Home_Cinema
{
    public class Theme
    {
        public string Name { get; set; } = string.Empty;
        public string BackColor { get; set; } = "#FFFFFF";
        public string ForeColor { get; set; } = "#000000";
        public string ButtonBackColor { get; set; } = "#D3D3D3";
        public string ButtonForeColor { get; set; } = "#000000";
        public string PanelBackColor { get; set; } = "#F5F5F5";
        public string MainFontFamily { get; set; } = "Segoe UI";
        public float MainFontSize { get; set; } = 10f;
        public FontStyle MainFontStyle { get; set; } = FontStyle.Regular;

        [JsonIgnore]
        public Color BackColorValue => ColorTranslator.FromHtml(BackColor);
        [JsonIgnore]
        public Color ForeColorValue => ColorTranslator.FromHtml(ForeColor);
        [JsonIgnore]
        public Color ButtonBackColorValue => ColorTranslator.FromHtml(ButtonBackColor);
        [JsonIgnore]
        public Color ButtonForeColorValue => ColorTranslator.FromHtml(ButtonForeColor);
        [JsonIgnore]
        public Color PanelBackColorValue => ColorTranslator.FromHtml(PanelBackColor);
        [JsonIgnore]
        public Font MainFontValue => new Font(MainFontFamily, MainFontSize, MainFontStyle);
    }
}
