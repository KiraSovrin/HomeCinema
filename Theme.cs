using System.Drawing;
using System.Text.Json.Serialization;

namespace Home_Cinema
{
    /// <summary>
    /// Centralized theme/style definition for the app. Grouped by UI area for easy editing.
    /// </summary>
    public class Theme
    {
        // ---- General Form ----
        public string Name { get; set; } = string.Empty;
        public string BackColor { get; set; } = "#FFFFFF";
        public string ForeColor { get; set; } = "#000000";
        public string MainFontFamily { get; set; } = "Segoe UI";
        public float MainFontSize { get; set; } = 10f;
        public FontStyle MainFontStyle { get; set; } = FontStyle.Regular;

        // ---- Panel ----
        public string PanelBackColor { get; set; } = "#F5F5F5";
        public string PanelBorderColor { get; set; } = "#CCCCCC";
        public int PanelBorderWidth { get; set; } = 1;
        public int PanelCornerRadius { get; set; } = 4;

        // ---- Button ----
        public string ButtonBackColor { get; set; } = "#D3D3D3";
        public string ButtonForeColor { get; set; } = "#000000";
        // You can add more button-specific properties here

        // ---- Label ----
        // Add label-specific style properties if needed

        // ---- Other controls ----
        // Add more groups as needed (e.g., TextBox, ListBox, etc.)

        // ---- Runtime helpers (not serialized) ----
        [JsonIgnore] public Color BackColorValue => ColorTranslator.FromHtml(BackColor);
        [JsonIgnore] public Color ForeColorValue => ColorTranslator.FromHtml(ForeColor);
        [JsonIgnore] public Color ButtonBackColorValue => ColorTranslator.FromHtml(ButtonBackColor);
        [JsonIgnore] public Color ButtonForeColorValue => ColorTranslator.FromHtml(ButtonForeColor);
        [JsonIgnore] public Color PanelBackColorValue => ColorTranslator.FromHtml(PanelBackColor);
        [JsonIgnore] public Color PanelBorderColorValue => ColorTranslator.FromHtml(PanelBorderColor);
        [JsonIgnore] public Font MainFontValue => new Font(MainFontFamily, MainFontSize, MainFontStyle);
    }
}
