using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Goal_Tracker
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /* --- Colors & Style --- */

            BgColorTextBox.Text = LoadFromIni("Color", "BgColor");
            if (LoadFromIni("Color", "BorderType") == "B")
            {
                BorderStyleToggleButton.Content = "Type B";
            }

            VeryEasyBorderTextBox.Text = LoadOrDefault("Color", "VeryEasyBorder", "#80d0d0");
            EasyBorderTextBox.Text = LoadOrDefault("Color", "EasyBorder", "#40d080");
            NormalBorderTextBox.Text = LoadOrDefault("Color", "NormalBorder", "#40d040");
            HardBorderTextBox.Text = LoadOrDefault("Color", "HardBorder", "#f8d000");
            ToughBorderTextBox.Text = LoadOrDefault("Color", "ToughBorder", "#f02800");
            InsaneBorderTextBox.Text = LoadOrDefault("Color", "InsaneBorder", "#e048f0");
            ExtremeBorderTextBox.Text = LoadOrDefault("Color", "ExtremeBorder", "#8000a0");
            ExtremeIIBorderTextBox.Text = LoadOrDefault("Color", "ExtremeIIBorder", "#780050");
            ExtremeIIIBorderTextBox.Text = LoadOrDefault("Color", "ExtremeIIIBorder", "#70001b");
            ImpossibleBorderTextBox.Text = LoadOrDefault("Color", "ImpossibleBorder", "#d35d6e");

            VeryEasyName.Text = LoadOrDefault("Color", "VeryEasyName", "Very Easy");
            EasyName.Text = LoadOrDefault("Color", "EasyName", "Easy");
            NormalName.Text = LoadOrDefault("Color", "NormalName", "Normal");
            HardName.Text = LoadOrDefault("Color", "HardName", "Hard");
            ToughName.Text = LoadOrDefault("Color", "ToughName", "Tough");
            InsaneName.Text = LoadOrDefault("Color", "InsaneName", "Insane");
            ExtremeName.Text = LoadOrDefault("Color", "ExtremeName", "Extreme");
            ExtremeIIName.Text = LoadOrDefault("Color", "ExtremeIIName", "Extreme II");
            ExtremeIIIName.Text = LoadOrDefault("Color", "ExtremeIIIName", "Extreme III");
            ImpossibleName.Text = LoadOrDefault("Color", "ImpossibleName", "Impossible");

            // Very Easy
            VeryEasyFillTextBox.Text = LoadOrDefault("Color", "VeryEasyFill", "#a0f8f8");
            if (LoadFromIni("Color", "VeryEasyWText") == "True")
            {
                VeryEasyTColorButton.Background = Brushes.Black;
                VeryEasyTColorButton.Foreground = Brushes.White;
            }

            // Easy
            EasyFillTextBox.Text = LoadOrDefault("Color", "EasyFill", "#50f8a0");
            if (LoadFromIni("Color", "EasyWText") == "True")
            {
                EasyTColorButton.Background = Brushes.Black;
                EasyTColorButton.Foreground = Brushes.White;
            }

            // Normal
            NormalFillTextBox.Text = LoadOrDefault("Color", "NormalFill", "#50f862");
            if (LoadFromIni("Color", "NormalWText") == "True")
            {
                NormalTColorButton.Background = Brushes.Black;
                NormalTColorButton.Foreground = Brushes.White;
            }

            // Hard
            HardFillTextBox.Text = LoadOrDefault("Color", "HardFill", "#fff840");
            if (LoadFromIni("Color", "HardWText") == "True")
            {
                HardTColorButton.Background = Brushes.Black;
                HardTColorButton.Foreground = Brushes.White;
            }

            // Tough
            ToughFillTextBox.Text = LoadOrDefault("Color", "ToughFill", "#ff5046");
            if (LoadFromIni("Color", "ToughWText") == "True")
            {
                ToughTColorButton.Background = Brushes.Black;
                ToughTColorButton.Foreground = Brushes.White;
            }

            // Insane
            InsaneFillTextBox.Text = LoadOrDefault("Color", "InsaneFill", "#ff5aff");
            if (LoadFromIni("Color", "InsaneWText") == "True")
            {
                InsaneTColorButton.Background = Brushes.Black;
                InsaneTColorButton.Foreground = Brushes.White;
            }

            // Extreme
            ExtremeFillTextBox.Text = LoadOrDefault("Color", "ExtremeFill", "#a000c8");
            if (LoadFromIni("Color", "ExtremeWText") == "False")
            {
                ExtremeTColorButton.Background = Brushes.White;
                ExtremeTColorButton.Foreground = Brushes.Black;
            }

            // Extreme II
            ExtremeIIFillTextBox.Text = LoadOrDefault("Color", "ExtremeIIFill", "#960062");
            if (LoadFromIni("Color", "ExtremeIIWText") == "False")
            {
                ExtremeIITColorButton.Background = Brushes.White;
                ExtremeIITColorButton.Foreground = Brushes.Black;
            }

            // Extreme III
            ExtremeIIIFillTextBox.Text = LoadOrDefault("Color", "ExtremeIIIFill", "#8c0023");
            if (LoadFromIni("Color", "ExtremeIIIWText") == "False")
            {
                ExtremeIIITColorButton.Background = Brushes.White;
                ExtremeIIITColorButton.Foreground = Brushes.Black;
            }

            // Impossible
            ImpossibleFillTextBox.Text = LoadOrDefault("Color", "ImpossibleFill", "#000000");
            if (LoadFromIni("Color", "ImpossibleWText") == "False")
            {
                ImpossibleTColorButton.Background = Brushes.White;
                ImpossibleTColorButton.Foreground = Brushes.Black;
            }

            /* --- General --- */
            if (LoadFromIni("General", "AutoSort") == "True")
            {
                AutoSortButton.IsChecked = true;
            }

            if (LoadFromIni("General", "MoreTier") == "True")
            {
                MoreTierButton.IsChecked = true;
            }
        }

        private string LoadOrDefault(string section, string key, string defaultValue)
        {
            string value = LoadFromIni(section, key);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        public string LoadFromIni(string section, string key)
        {
            string iniFilePath = "gtoptions.ini"; // Path to your INI file

            try
            {
                if (File.Exists(iniFilePath))
                {
                    var lines = File.ReadAllLines(iniFilePath);
                    bool sectionFound = false;

                    foreach (var line in lines)
                    {
                        if (line.StartsWith($"[{section}]"))
                        {
                            sectionFound = true;
                        }

                        if (sectionFound && line.StartsWith(key))
                        {
                            var keyValue = line.Split('=');
                            if (keyValue.Length == 2)
                            {
                                return keyValue[1].Trim(); // Return the value
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from INI file: {ex.Message}");
            }

            return string.Empty; // Return empty string if the key is not found
        }

        public static void SaveToIni(string section, string key, string value)
        {
            string iniFilePath = "gtoptions.ini"; // Path to your INI file

            try
            {
                var lines = File.Exists(iniFilePath) ? File.ReadAllLines(iniFilePath).ToList() : new List<string>();

                bool sectionFound = false;
                bool keyUpdated = false;
                int sectionIndex = -1;
                int insertIndex = -1; // Default to appending at the end of the section

                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i].Trim();

                    // Check if it's a section
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        if (sectionFound) // If we were in the right section and found a new one, stop searching
                            break;

                        if (line == $"[{section}]")
                        {
                            sectionFound = true;
                            sectionIndex = i;
                        }
                    }

                    // If in the correct section, look for the key
                    if (sectionFound && line.StartsWith($"{key}="))
                    {
                        lines[i] = $"{key}={value}"; // Overwrite existing key
                        keyUpdated = true;
                        break;
                    }

                    // If in the correct section and we find a blank line, mark where we can insert
                    if (sectionFound && string.IsNullOrWhiteSpace(line) && insertIndex == -1)
                    {
                        insertIndex = i;
                    }
                }

                if (sectionFound && !keyUpdated)
                {
                    // Insert key-value pair inside the section, either at a blank line or at the end of the section
                    if (insertIndex != -1)
                        lines.Insert(insertIndex, $"{key}={value}");
                    else
                        lines.Insert(sectionIndex + 1, $"{key}={value}"); // No blank lines, insert right after section header
                }
                else if (!sectionFound)
                {
                    // Append new section and key-value pair at the end of the file
                    lines.Add($"[{section}]");
                    lines.Add($"{key}={value}");
                }

                File.WriteAllLines(iniFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to INI file: {ex.Message}");
            }
        }

        public void DeleteKeyFromIni(string section, string key)
        {
            string iniFilePath = "gtoptions.ini";

            if (!File.Exists(iniFilePath))
            {
                throw new FileNotFoundException($"The INI file '{iniFilePath}' does not exist.");
            }

            var lines = File.ReadAllLines(iniFilePath).ToList();
            bool inTargetSection = false;
            bool keyDeleted = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].Trim();

                // Check if this line is a section
                if (line.StartsWith('[') && line.EndsWith(']'))
                {
                    inTargetSection = string.Equals(line, $"[{section}]", StringComparison.OrdinalIgnoreCase);
                }
                else if (inTargetSection && line.StartsWith($"{key}=", StringComparison.OrdinalIgnoreCase))
                {
                    // Remove the key if it's in the target section
                    lines.RemoveAt(i);
                    keyDeleted = true;
                    break;
                }
            }

            if (keyDeleted)
            {
                // Write back to the file
                File.WriteAllLines(iniFilePath, lines);
            }
        }


        // Event handler for TextBox text change (to preview color)
        private void BgColorTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Get the color value from the TextBox and ensure it starts with '#'
            string hexColor = BgColorTextBox.Text.Trim();

            // If the color doesn't start with '#', add it
            if (!hexColor.StartsWith('#'))
            {
                hexColor = "#" + hexColor;
            }

            // Validate if the input is a valid hex color string
            if (IsValidHexColor(hexColor))
            {
                try
                {
                    // Convert the hex color string to a Brush and update the color preview
                    var color = (Color)ColorConverter.ConvertFromString(hexColor);
                    BgColorPreview.Background = new SolidColorBrush(color);
                    SaveToIni("Color", "BgColor", hexColor);
                }
                catch (FormatException)
                {
                    // Handle invalid color conversion (if necessary)
                    BgColorPreview.Background = Brushes.Gray; // Show gray if conversion fails
                }
            }
            else
            {
                // If it's not a valid hex color, show a default gray preview
                BgColorPreview.Background = Brushes.Gray;
            }
        }

        // Validation
        private string HandleColor(String colorinput, String key)
        {
            if (!colorinput.StartsWith('#'))
            {
                colorinput = "#" + colorinput;
            }
            if (IsValidHexColor(colorinput))
            {
                SaveToIni("Color", key, colorinput);
                return colorinput;
            }
            else
            {
                return String.Empty;
            }
        }

        // Utility function to check if the hex color is valid
        private bool IsValidHexColor(string hex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hex, "^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})$");
        }

        private void BorderStyleToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (BorderStyleToggleButton.Content.ToString() == "Type A")
                {
                    BorderStyleToggleButton.Content = "Type B";
                    SaveToIni("Color", "BorderType", "B");
                }
                else
                {
                    BorderStyleToggleButton.Content = "Type A";
                    SaveToIni("Color", "BorderType", "A");
                }
            }
        }

        // Very Easy Border Color TextChanged handler
        private void VeryEasyBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(VeryEasyBorderTextBox.Text, "VeryEasyBorder");
            if (color != String.Empty)
            {
                VeryEasyBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Easy Border Color TextChanged handler
        private void EasyBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(EasyBorderTextBox.Text, "EasyBorder");
            if (color != String.Empty)
            {
                EasyBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Normal Border Color TextChanged handler
        private void NormalBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(NormalBorderTextBox.Text, "NormalBorder");
            if (color != String.Empty)
            {
                NormalBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Hard Border Color TextChanged handler
        private void HardBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(HardBorderTextBox.Text, "HardBorder");
            if (color != String.Empty)
            {
                HardBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Tough Border Color TextChanged handler
        private void ToughBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(ToughBorderTextBox.Text, "ToughBorder");
            if (color != String.Empty)
            {
                ToughBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Insane Border Color TextChanged handler
        private void InsaneBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(InsaneBorderTextBox.Text, "InsaneBorder");
            if (color != String.Empty)
            {
                InsaneBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme Border Color TextChanged handler
        private void ExtremeBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(ExtremeBorderTextBox.Text, "ExtremeBorder");
            if (color != String.Empty)
            {
                ExtremeBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme II Border Color TextChanged handler
        private void ExtremeIIBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(ExtremeIIBorderTextBox.Text, "ExtremeIIBorder");
            if (color != String.Empty)
            {
                ExtremeIIBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme III Border Color TextChanged handler
        private void ExtremeIIIBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(ExtremeIIIBorderTextBox.Text, "ExtremeIIIBorder");
            if (color != String.Empty)
            {
                ExtremeIIIBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Impossible Border Color TextChanged handler
        private void ImpossibleBorderTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(ImpossibleBorderTextBox.Text, "ImpossibleBorder");
            if (color != String.Empty)
            {
                ImpossibleBorderPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Very Easy
        private void VeryEasyFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate and set the border color for the preview
            String color = HandleColor(VeryEasyFillTextBox.Text, "VeryEasyFill");
            if (color != String.Empty)
            {
                VeryEasyFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Easy
        private void EasyFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(EasyFillTextBox.Text, "EasyFill");
            if (color != String.Empty)
            {
                EasyFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Normal
        private void NormalFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(NormalFillTextBox.Text, "NormalFill");
            if (color != String.Empty)
            {
                NormalFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Hard
        private void HardFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(HardFillTextBox.Text, "HardFill");
            if (color != String.Empty)
            {
                HardFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Tough
        private void ToughFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(ToughFillTextBox.Text, "ToughFill");
            if (color != String.Empty)
            {
                ToughFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Insane
        private void InsaneFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(InsaneFillTextBox.Text, "InsaneFill");
            if (color != String.Empty)
            {
                InsaneFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme
        private void ExtremeFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(ExtremeFillTextBox.Text, "ExtremeFill");
            if (color != String.Empty)
            {
                ExtremeFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme II
        private void ExtremeIIFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(ExtremeIIFillTextBox.Text, "ExtremeIIFill");
            if (color != String.Empty)
            {
                ExtremeIIFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Extreme III
        private void ExtremeIIIFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(ExtremeIIIFillTextBox.Text, "ExtremeIIIFill");
            if (color != String.Empty)
            {
                ExtremeIIIFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Impossible
        private void ImpossibleFillTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String color = HandleColor(ImpossibleFillTextBox.Text, "ImpossibleFill");
            if (color != String.Empty)
            {
                ImpossibleFillPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }

        // Very Easy
        private void VeryEasyTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "VeryEasyWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "VeryEasyWText", "False");
                }
            }
        }

        // Easy
        private void EasyTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "EasyWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "EasyWText", "False");
                }
            }
        }

        // Normal
        private void NormalTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "NormalWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "NormalWText", "False");
                }
            }
        }

        // Hard
        private void HardTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "HardWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "HardWText", "False");
                }
            }
        }

        // Tough
        private void ToughTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "ToughWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "ToughWText", "False");
                }
            }
        }

        // Insane
        private void InsaneTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "InsaneWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "InsaneWText", "False");
                }
            }
        }

        // Extreme
        private void ExtremeTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "ExtremeWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "ExtremeWText", "False");
                }
            }
        }

        // Extreme II
        private void ExtremeIITColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "ExtremeIIWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "ExtremeIIWText", "False");
                }
            }
        }

        // Extreme III
        private void ExtremeIIITColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "ExtremeIIIWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "ExtremeIIIWText", "False");
                }
            }
        }

        // Impossible
        private void ImpossibleTColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button toggleButton)
            {
                if (toggleButton.Background == Brushes.White)
                {
                    toggleButton.Background = Brushes.Black;
                    toggleButton.Foreground = Brushes.White;
                    SaveToIni("Color", "ImpossibleWText", "True");
                }
                else
                {
                    toggleButton.Background = Brushes.White;
                    toggleButton.Foreground = Brushes.Black;
                    SaveToIni("Color", "ImpossibleWText", "False");
                }
            }
        }

        private void AutoSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoSortButton.IsChecked == true)
            {
                SaveToIni("General", "AutoSort", "True");
            }
            else
            {
                SaveToIni("General", "AutoSort", "False");
            }
        }

        private void MoreTierButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoreTierButton.IsChecked == true)
            {
                SaveToIni("General", "MoreTier", "True");
            }
            else
            {
                SaveToIni("General", "MoreTier", "False");
            }
        }

        private void VeryEasyNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "VeryEasyName", VeryEasyName.Text);
        }

        private void EasyNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "EasyName", EasyName.Text);
        }

        private void NormalNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "NormalName", NormalName.Text);
        }

        private void HardNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "HardName", HardName.Text);
        }

        private void ToughNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "ToughName", ToughName.Text);
        }

        private void InsaneNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "InsaneName", InsaneName.Text);
        }

        private void ExtremeNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "ExtremeName", ExtremeName.Text);
        }

        private void ExtremeIINameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "ExtremeIIName", ExtremeIIName.Text);
        }

        private void ExtremeIIINameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "ExtremeIIIName", ExtremeIIIName.Text);
        }

        private void ImpossibleNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveToIni("Color", "ImpossibleName", ImpossibleName.Text);
        }

        private void PresetButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            var presetSelector = new ColorPresetSelector();
            presetSelector.ShowDialog();
        }
    }
}