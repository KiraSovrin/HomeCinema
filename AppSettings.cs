using System.Text.Json;

namespace Home_Cinema
{
    public class AppSettings
    {
        public string MainFolderPath { get; set; }
        public Dictionary<string, FolderCustomization> FolderCustomizations { get; set; } = new();
        public string SelectedTheme { get; set; } = "Light";
    }

    public class FolderCustomization
    {
        public string CustomTitle { get; set; }
        public string CustomImagePath { get; set; }
    }
}
