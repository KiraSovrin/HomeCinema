using System.IO;
using System.Linq;
using System.Text.Json;

namespace Home_Cinema
{
    public partial class Form1 : Form
    {
        private const string SettingsFile = "appsettings.json";
        private AppSettings appSettings;
        private string currentMainFolder;
        private LibVLCSharp.Shared.LibVLC _libVLC;
        private LibVLCSharp.WinForms.VideoView _videoView;
        private string _currentPlayingFile;
        private Button _btnClosePlayer;
        private bool _isSeeking = false;
        private System.Windows.Forms.Timer _seekTimer;

        private Theme LightTheme = new Theme
        {
            Name = "Light",
            BackColor = Color.White,
            ForeColor = Color.Black,
            ButtonBackColor = Color.LightGray,
            ButtonForeColor = Color.Black,
            PanelBackColor = Color.WhiteSmoke,
            MainFont = new Font("Segoe UI", 10)
        };
        private Theme DarkTheme = new Theme
        {
            Name = "Dark",
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.White,
            ButtonBackColor = Color.FromArgb(60, 60, 60),
            ButtonForeColor = Color.White,
            PanelBackColor = Color.FromArgb(45, 45, 45),
            MainFont = new Font("Segoe UI", 10)
        };
        private Theme CustomTheme = null;
        private Theme CurrentTheme => CustomTheme ?? (appSettings.SelectedTheme == "Dark" ? DarkTheme : LightTheme);

        private Button btnSettings;

        public Form1()
        {
            InitializeComponent();
            btnSettings = new Button { Text = "Settings", Width = 100, Height = 30, Top = 12, Left = 180 };
            btnSettings.Click += (s, e) => ShowSettingsPanel();
            this.Controls.Add(btnSettings);
            LoadSettings();
            if (!string.IsNullOrEmpty(appSettings.MainFolderPath))
            {
                currentMainFolder = appSettings.MainFolderPath;
            }
            ShowMainPage();
            ApplyTheme(CurrentTheme);
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
                appSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            else
            {
                appSettings = new AppSettings();
            }
        }

        private void SaveSettings()
        {
            appSettings.MainFolderPath = currentMainFolder;
            var json = JsonSerializer.Serialize(appSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFile, json);
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentMainFolder = dialog.SelectedPath;
                    SaveSettings();
                    ShowMainPage();
                }
            }
        }

        private void ShowMainPage()
        {
            mainPanel.Controls.Clear();
            if (string.IsNullOrEmpty(currentMainFolder) || !Directory.Exists(currentMainFolder))
                return;
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            var subfolders = Directory.GetDirectories(currentMainFolder);
            foreach (var folder in subfolders)
            {
                var card = CreateMediaCard(folder);
                flowPanel.Controls.Add(card);
            }
            mainPanel.Controls.Add(flowPanel);
        }

        private Panel CreateMediaCard(string folderPath)
        {
            var panel = new Panel
            {
                Width = 150,
                Height = 200,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            var picture = new PictureBox
            {
                Width = 130,
                Height = 130,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            var label = new Label
            {
                Top = 150,
                Left = 10,
                Width = 130,
                TextAlign = ContentAlignment.MiddleCenter
            };
            // Load customization if exists
            var folderKey = folderPath;
            if (appSettings.FolderCustomizations.TryGetValue(folderKey, out var custom))
            {
                label.Text = string.IsNullOrWhiteSpace(custom.CustomTitle) ? Path.GetFileName(folderPath) : custom.CustomTitle;
                if (!string.IsNullOrEmpty(custom.CustomImagePath) && File.Exists(custom.CustomImagePath))
                {
                    picture.Image = Image.FromFile(custom.CustomImagePath);
                }
                else
                {
                    picture.Image = SystemIcons.Application.ToBitmap();
                }
            }
            else
            {
                label.Text = Path.GetFileName(folderPath);
                picture.Image = SystemIcons.Application.ToBitmap();
            }
            // Add edit button
            var btnEdit = new Button
            {
                Text = "Edit",
                Width = 50,
                Height = 25,
                Top = 170,
                Left = 50
            };
            btnEdit.Click += (s, e) => EditFolderCard(folderPath);
            panel.Controls.Add(picture);
            panel.Controls.Add(label);
            panel.Controls.Add(btnEdit);
            panel.Cursor = Cursors.Hand;
            panel.Click += (s, e) => ShowMediaFilesView(folderPath);
            picture.Click += (s, e) => ShowMediaFilesView(folderPath);
            label.Click += (s, e) => ShowMediaFilesView(folderPath);
            return panel;
        }

        private void EditFolderCard(string folderPath)
        {
            // Simple dialog for title and image
            using (var form = new Form { Width = 400, Height = 200, Text = "Edit Card" })
            {
                var lblTitle = new Label { Text = "Title:", Top = 20, Left = 10, Width = 50 };
                var txtTitle = new TextBox { Top = 20, Left = 70, Width = 300 };
                var lblImage = new Label { Text = "Image:", Top = 60, Left = 10, Width = 50 };
                var txtImage = new TextBox { Top = 60, Left = 70, Width = 220 };
                var btnBrowse = new Button { Text = "Browse", Top = 60, Left = 300, Width = 70 };
                var btnOk = new Button { Text = "OK", Top = 110, Left = 220, Width = 70, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Top = 110, Left = 300, Width = 70, DialogResult = DialogResult.Cancel };
                form.Controls.AddRange(new Control[] { lblTitle, txtTitle, lblImage, txtImage, btnBrowse, btnOk, btnCancel });
                // Load current values
                if (appSettings.FolderCustomizations.TryGetValue(folderPath, out var custom))
                {
                    txtTitle.Text = custom.CustomTitle;
                    txtImage.Text = custom.CustomImagePath;
                }
                btnBrowse.Click += (s, e) =>
                {
                    using (var ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif" })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            txtImage.Text = ofd.FileName;
                        }
                    }
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newCustom = new FolderCustomization
                    {
                        CustomTitle = txtTitle.Text,
                        CustomImagePath = txtImage.Text
                    };
                    appSettings.FolderCustomizations[folderPath] = newCustom;
                    SaveSettings();
                    ShowMainPage();
                }
            }
        }

        private void ShowMediaFilesView(string folderPath)
        {
            mainPanel.Controls.Clear();
            // Video player area (always visible)
            Panel videoArea = new Panel
            {
                Top = 10,
                Left = 10,
                Width = mainPanel.Width - 40,
                Height = 350,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle
            };
            videoArea.Name = "videoArea";
            // Controls panel (always visible)
            Panel controlsPanel = new Panel
            {
                Top = videoArea.Bottom + 5,
                Left = 10,
                Width = mainPanel.Width - 40,
                Height = 60,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            controlsPanel.Name = "controlsPanel";
            // Back button
            var backButton = new Button
            {
                Text = "Back",
                Width = 80,
                Height = 30,
                Top = 10,
                Left = 10
            };
            backButton.Click += (s, e) => ShowMainPage();
            // Video files list
            var panelFiles = new Panel
            {
                Top = controlsPanel.Bottom + 10,
                Left = 10,
                Width = mainPanel.Width - 40,
                Height = mainPanel.Height - controlsPanel.Bottom - 30,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true
            };
            var videoExtensions = new[] { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv" };
            var files = Directory.GetFiles(folderPath)
                .Where(f => videoExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToArray();
            int y = 10;
            foreach (var file in files)
            {
                var filePanel = new Panel
                {
                    Width = panelFiles.Width - 30,
                    Height = 40,
                    Top = y,
                    Left = 0,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                var btnPlay = new Button
                {
                    Text = "Play",
                    Width = 60,
                    Height = 30,
                    Top = 5,
                    Left = 5
                };
                string filePath = file;
                btnPlay.Click += (s, e) => PlayInTopVideoPlayer(filePath, folderPath, videoArea, controlsPanel);
                var lblFile = new Label
                {
                    Text = Path.GetFileName(file),
                    Top = 10,
                    Left = 75,
                    Width = filePanel.Width - 85,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                filePanel.Controls.Add(btnPlay);
                filePanel.Controls.Add(lblFile);
                panelFiles.Controls.Add(filePanel);
                y += 45;
            }
            mainPanel.Controls.Add(backButton);
            mainPanel.Controls.Add(videoArea);
            mainPanel.Controls.Add(controlsPanel);
            mainPanel.Controls.Add(panelFiles);
        }

        private void PlayInTopVideoPlayer(string filePath, string folderPath, Panel videoArea, Panel controlsPanel)
        {
            // Stop and dispose previous playback and timer
            if (_videoView != null)
            {
                if (_videoView.MediaPlayer != null)
                {
                    _videoView.MediaPlayer.Stop();
                    _videoView.MediaPlayer.Dispose();
                }
                _videoView.Dispose();
                _videoView = null;
            }
            if (_seekTimer != null)
            {
                _seekTimer.Stop();
                _seekTimer.Dispose();
                _seekTimer = null;
            }
            videoArea.Controls.Clear();
            controlsPanel.Controls.Clear();
            // Show loading indicator
            var loadingLabel = new Label
            {
                Text = "Loading...",
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Top = videoArea.Height / 2 - 20,
                Left = videoArea.Width / 2 - 60
            };
            videoArea.Controls.Add(loadingLabel);
            videoArea.Refresh();
            var timer = new System.Windows.Forms.Timer { Interval = 100 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                videoArea.Controls.Clear();
                if (_libVLC == null)
                {
                    LibVLCSharp.Shared.Core.Initialize();
                    _libVLC = new LibVLCSharp.Shared.LibVLC();
                }
                _videoView = new LibVLCSharp.WinForms.VideoView
                {
                    Dock = DockStyle.Fill,
                    MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC)
                };
                videoArea.Controls.Add(_videoView);
                _videoView.MediaPlayer.Play(new LibVLCSharp.Shared.Media(_libVLC, filePath, LibVLCSharp.Shared.FromType.FromPath));
                // Controls
                var btnPlay         = new Button { Text = "Play", Width = 60, Left = 10, Top = 15 };
                var btnPause        = new Button { Text = "Pause", Width = 60, Left = 80, Top = 15 };
                var btnStop         = new Button { Text = "Stop", Width = 60, Left = 150, Top = 15 };
                var trackBarSeek    = new TrackBar { Left = 220, Top = 20, Width = 300, Minimum = 0, Maximum = 100, TickStyle = TickStyle.None };
                var lblTime         = new Label { Left = 530, Top = 22, Width = 80 };
                var trackBarVolume  = new TrackBar { Left = 620, Top = 20, Width = 100, Minimum = 0, Maximum = 100, Value = 100, TickStyle = TickStyle.None };
                var lblVolume       = new Label { Left = 730, Top = 22, Width = 50, Text = "Vol" };
                btnPlay.Click       += (s2, e2) => _videoView.MediaPlayer.Play();
                btnPause.Click      += (s2, e2) => _videoView.MediaPlayer.Pause();
                btnStop.Click       += (s2, e2) => _videoView.MediaPlayer.Stop();
                trackBarVolume.Scroll += (s2, e2) => _videoView.MediaPlayer.Volume = trackBarVolume.Value;
                _videoView.MediaPlayer.Volume = 100;
                // Seek logic
                trackBarSeek.MouseDown += (s2, e2) => _isSeeking = true;
                trackBarSeek.MouseUp += (s2, e2) =>
                {
                    if (_videoView.MediaPlayer.Length > 0)
                    {
                        _videoView.MediaPlayer.Time = (long)(_videoView.MediaPlayer.Length * (trackBarSeek.Value / 100.0));
                    }
                    _isSeeking = false;
                };
                // Timer for updating seek bar and time
                _seekTimer = new System.Windows.Forms.Timer { Interval = 500 };
                _seekTimer.Tick += (s2, e2) =>
                {
                    if (_videoView.MediaPlayer.IsPlaying && !_isSeeking && _videoView.MediaPlayer.Length > 0)
                    {
                        var percent = (int)(100.0 * _videoView.MediaPlayer.Time / _videoView.MediaPlayer.Length);
                        trackBarSeek.Value = Math.Min(Math.Max(percent, 0), 100);
                        lblTime.Text = $"{TimeSpan.FromMilliseconds(_videoView.MediaPlayer.Time):mm\\:ss} / {TimeSpan.FromMilliseconds(_videoView.MediaPlayer.Length):mm\\:ss}";
                    }
                };
                _seekTimer.Start();
                controlsPanel.Controls.Add(btnPlay);
                controlsPanel.Controls.Add(btnPause);
                controlsPanel.Controls.Add(btnStop);
                controlsPanel.Controls.Add(trackBarSeek);
                controlsPanel.Controls.Add(lblTime);
                controlsPanel.Controls.Add(trackBarVolume);
                controlsPanel.Controls.Add(lblVolume);
            };
            timer.Start();
        }

        private void ShowSettingsPanel()
        {
            mainPanel.Controls.Clear();
            var lblTheme = new Label { Text = "Theme:", Top = 30, Left = 30, Width = 60 };
            var comboTheme = new ComboBox { Top = 30, Left = 100, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            comboTheme.Items.AddRange(new object[] { "Light", "Dark", "Custom" });
            comboTheme.SelectedItem = appSettings.SelectedTheme ?? "Light";
            var btnImport = new Button { Text = "Import Skin", Top = 70, Left = 100, Width = 150 };
            var btnBack = new Button { Text = "Back", Top = 120, Left = 100, Width = 150 };
            mainPanel.Controls.Add(lblTheme);
            mainPanel.Controls.Add(comboTheme);
            mainPanel.Controls.Add(btnImport);
            mainPanel.Controls.Add(btnBack);
            comboTheme.SelectedIndexChanged += (s, e) =>
            {
                appSettings.SelectedTheme = comboTheme.SelectedItem.ToString();
                CustomTheme = null;
                SaveSettings();
                ApplyTheme(CurrentTheme);
            };
            btnImport.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog { Filter = "JSON Files|*.json" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var json = File.ReadAllText(ofd.FileName);
                            CustomTheme = JsonSerializer.Deserialize<Theme>(json);
                            appSettings.SelectedTheme = "Custom";
                            SaveSettings();
                            ApplyTheme(CurrentTheme);
                            comboTheme.SelectedItem = "Custom";
                        }
                        catch
                        {
                            MessageBox.Show("Invalid theme file.");
                        }
                    }
                }
            };
            btnBack.Click += (s, e) => ShowMainPage();
            ApplyTheme(CurrentTheme);
        }

        private void ApplyTheme(Theme theme)
        {
            this.BackColor = theme.BackColor;
            this.ForeColor = theme.ForeColor;
            this.Font = theme.MainFont;
            ApplyThemeToControls(this.Controls, theme);
        }

        private void ApplyThemeToControls(Control.ControlCollection controls, Theme theme)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Button)
                {
                    ctrl.BackColor = theme.ButtonBackColor;
                    ctrl.ForeColor = theme.ButtonForeColor;
                }
                else if (ctrl is Panel)
                {
                    ctrl.BackColor = theme.PanelBackColor;
                    ctrl.ForeColor = theme.ForeColor;
                }
                else
                {
                    ctrl.BackColor = theme.BackColor;
                    ctrl.ForeColor = theme.ForeColor;
                }
                if (ctrl.HasChildren)
                    ApplyThemeToControls(ctrl.Controls, theme);
            }
        }
    }
}
