using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;

namespace Goal_Tracker
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Goal> Goals { get; set; }
        private ICollectionView goalsView;

        public MainWindow()
        {
            InitializeComponent();

            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            // Load goals
            Goals = LoadGoalsFromIni("goals.ini");

            DataContext = this;

            // Set up the CollectionView for filtering
            goalsView = CollectionViewSource.GetDefaultView(Goals);
            goalsView.Filter = ShowActiveGoalsFilter; // Default to showing active goals

            // Bind the CollectionView to the GoalsList control
            GoalsList.ItemsSource = goalsView;

            // Attach the closing event
            Application.Current.Exit += OnAppExit;
        }

        // This handler is called when the window is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoadOptionFromIni("Color", "BorderType") == "B")
            GoalsList.ItemTemplate = (DataTemplate)Resources["CompactGoalItemTemplate"];
            
            // Load theme color from options
            string? bgColorHex = LoadOptionFromIni("Color", "BgColor");

            if (!string.IsNullOrWhiteSpace(bgColorHex))
            {
                try
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = converter.ConvertFromString(bgColorHex) as System.Windows.Media.Brush;

                    if (brush != null)
                    {
                        AppGrid.Background = brush;
                    }
                    else
                    {
                        MessageBox.Show($"Invalid color value: {bgColorHex}. Please check your settings.",
                                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error converting color: {bgColorHex}. \n{ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Iterate through the goals and check if completed
            foreach (var goal in Goals)
            {
                if (goal.IsCompleted && goal.CompletionDate.HasValue)
                {
                    // Find the corresponding TextBlock in the DataTemplate and set the visibility
                    var textBlock = FindTextBlockForGoal(goal);
                    if (textBlock != null)
                    {
                        // Set the visibility to visible
                        textBlock.Visibility = Visibility.Visible;

                        // Update the text with the formatted completion date
                        textBlock.Text = $"Completed: {goal.CompletionDate.Value.ToString("g")}";
                    }
                }
            }
        }

        public static string LoadOptionFromIni(string section, string key)
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

        // This method finds the corresponding TextBlock based on the goal
        private TextBlock FindTextBlockForGoal(Goal goal)
        {
            // Find the TextBlock for the goal based on the goal's completion status
            foreach (var item in GoalsList.Items)
            {
                var goalItem = item as Goal;
                if (goalItem != null && goalItem == goal)
                {
                    // Assuming the TextBlock for the completion date is named "CompletionDateTextBlock"
                    var border = (Border)GoalsList.ItemTemplate.LoadContent();
                    var textBlock = (TextBlock)border.FindName("CompletionDateTextBlock");
                    return textBlock;
                }
            }
            return null;
        }

        private bool ShowCompletedGoalsFilter(object item)
        {
            return item is Goal goal && goal.IsCompleted;
        }

        private bool ShowActiveGoalsFilter(object item)
        {
            return item is Goal goal && !goal.IsCompleted;
        }

        private bool ShowAllGoalsFilter(object item)
        {
            return true;
        }

        private void FilterA_Click(object sender, RoutedEventArgs e)
        {
            // Get the text of FilterA to check which filter is currently active
            var filterA = (sender as MenuItem)?.Header.ToString();

            if (filterA == "Show Active Goals") // If FilterA is showing Active Goals, switch to Completed Goals
            {
                // Switch to Show Completed Goals
                goalsView.Filter = ShowActiveGoalsFilter;
                AppWindow.Title = "Goal Tracker";
                // Update the text for the next filter
                FilterA.Header = "Show Completed Goals";
                FilterB.Header = "Show All Goals"; // Set FilterB to show All Goals
            }
            else if (filterA == "Show Completed Goals") // If FilterA is showing Completed Goals, switch to All Goals
            {
                // Switch to Show All Goals
                goalsView.Filter = ShowCompletedGoalsFilter;
                AppWindow.Title = "Goal Tracker (Completed)";
                // Update the text for the next filter
                FilterA.Header = "Show Active Goals";
                FilterB.Header = "Show All Goals"; // Set FilterB back to show Active Goals
            }

            goalsView.Refresh(); // Refresh the view
        }

        private void FilterB_Click(object sender, RoutedEventArgs e)
        {
            // Get the text of FilterB to check which filter is currently active
            var filterB = (sender as MenuItem)?.Header.ToString();

            if (filterB == "Show Completed Goals") // If FilterB is showing Active Goals, switch to Completed Goals
            {
                // Switch to Show Completed Goals
                goalsView.Filter = ShowCompletedGoalsFilter;
                AppWindow.Title = "Goal Tracker (Completed)";
                // Update the text for the next filter
                FilterA.Header = "Show Active Goals";
                FilterB.Header = "Show All Goals"; // Set FilterB to show All Goals
            }
            else if (filterB == "Show All Goals") // If FilterB is showing Completed Goals, switch to All Goals
            {
                // Switch to Show All Goals
                goalsView.Filter = ShowAllGoalsFilter;
                AppWindow.Title = "Goal Tracker (All)";
                // Update the text for the next filter
                FilterA.Header = "Show Active Goals";
                FilterB.Header = "Show Completed Goals"; // Set FilterB back to show Active Goals
            }

            goalsView.Refresh(); // Refresh the view
        }

        // Method to load goals from an INI file using IniFile.ReadIniFile
        private ObservableCollection<Goal> LoadGoalsFromIni(string filePath)
        {
            var goals = new ObservableCollection<Goal>();

            try
            {
                if (File.Exists(filePath))
                {
                    var iniData = IniFile.ReadIniFile(filePath);

                    // Iterate through the sections (each section represents a goal)
                    foreach (var section in iniData)
                    {
                        var goal = new Goal();

                        // Extract and decode the Name (Base64)
                        if (section.Value.ContainsKey("Name"))
                        {
                            string encodedName = section.Value["Name"];
                            byte[] decodedNameBytes = Convert.FromBase64String(encodedName);
                            goal.Name = Encoding.UTF8.GetString(decodedNameBytes);
                        }

                        // Extract and decode the Description (Base64)
                        if (section.Value.ContainsKey("Description"))
                        {
                            string encodedDescription = section.Value["Description"];
                            byte[] decodedDescriptionBytes = Convert.FromBase64String(encodedDescription);
                            goal.Description = Encoding.UTF8.GetString(decodedDescriptionBytes);
                        }

                        // Extract other properties
                        if (section.Value.ContainsKey("Difficulty") && int.TryParse(section.Value["Difficulty"], out var difficulty))
                            goal.Difficulty = difficulty;

                        if (section.Value.ContainsKey("Completed") && bool.TryParse(section.Value["Completed"], out var completed))
                            goal.IsCompleted = completed;
                        // Load CompletionDate if available
                        if (section.Value.ContainsKey("CompletionDate"))
                        {
                            if (DateTime.TryParseExact(section.Value["CompletionDate"], "yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var completionDate))
                            {
                                goal.CompletionDate = completionDate;
                            }
                        }


                        goals.Add(goal);
                    }
                }
                else
                {
                    throw new FileNotFoundException($"The file '{filePath}' was not found.");
                }
            }
            catch
            {
                //MessageBox.Show($"An error occurred while loading the goals: {ex.Message}\nLoading predefined goals.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Welcome to the Goal Tracker! This is the perfect app to keep track of your life (or short term) goals. Right click anywhere on the blank list to get started." + Environment.NewLine + "This app is licensed under the GPL-3.0 license.", "Goal Tracker");

                // Load predefined goals if the INI file is missing or any error occurs
                goals = new ObservableCollection<Goal>
        {
                        // Old difficulties of the presets were 5, 10, 20
            new Goal { Name = "Learn WPF", Description = "Master the basics of WPF.", Difficulty = 3 },
            new Goal { Name = "Build a Tracker", Description = "Develop a goal tracker app.", Difficulty = 5 },
            new Goal { Name = "Skydiving", Description = "Try skydiving at least once.", Difficulty = 8 }
        };
            }

            return goals;
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            SaveGoalsToIni("goals.ini");
        }

        private void AddGoalToTop_Click(object sender, RoutedEventArgs e)
        {
            // Create a new empty goal
            var newGoal = new Goal
            {
                Name = "New Goal",
                Description = "Description",
                Difficulty = 3,
                IsCompleted = false
            };

            if (goalsView.Filter == ShowCompletedGoalsFilter)
            {
                goalsView.Filter = ShowActiveGoalsFilter;
                AppWindow.Title = "Goal Tracker";
                FilterA.Header = "Show Completed Goals";
                FilterB.Header = "Show All Goals";
            }

            // Insert at the top of the list
            Goals.Insert(0, newGoal);

            // Optionally: Update the view if needed
            GoalsList.ItemsSource = null;
            GoalsList.ItemsSource = Goals;
            goalsView.Refresh();
        }

        private void AddGoalToBottom_Click(object sender, RoutedEventArgs e)
        {
            // Create a new empty goal
            var newGoal = new Goal
            {
                Name = "New Goal",
                Description = "Description",
                Difficulty = 3,
                IsCompleted = false
            };

            if (goalsView.Filter == ShowCompletedGoalsFilter)
            {
                goalsView.Filter = ShowActiveGoalsFilter;
                AppWindow.Title = "Goal Tracker";
                FilterA.Header = "Show Completed Goals";
                FilterB.Header = "Show All Goals";
            }

            // Add to the bottom of the list
            Goals.Add(newGoal);

            // Optionally: Update the view if needed
            GoalsList.ItemsSource = null;
            GoalsList.ItemsSource = Goals;
            goalsView.Refresh();
        }

        private void OptionsMenu_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the Options/Settings window
            var optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();  // Open the options window as a dialog
        }

        private void SaveGoalsToIni(string filePath)
        {
            var data = new Dictionary<string, Dictionary<string, string>>();

            // Use IEnumerable<Goal> to avoid type mismatches
            IEnumerable<Goal> goalsToSave = Goals; // Default: Use original ObservableCollection

            if (LoadOptionFromIni("General", "AutoSort") == "True")
            {
                // Sort the goals ONLY for saving, without modifying the ObservableCollection
                goalsToSave = Goals.OrderBy(g => g.IsCompleted) // Sort by Difficulty
                                   .ThenByDescending(g => g.Difficulty); // Keep completed goals at the bottom
            }

            int index = 0;
            foreach (var goal in goalsToSave) // Use sorted or original list
            {
                var sectionName = AlphabeticIdentifier(index++);

                // Step 1: Encode the name and description to Base64
                string encodedName = Convert.ToBase64String(Encoding.UTF8.GetBytes(goal.Name));
                string encodedDescription = Convert.ToBase64String(Encoding.UTF8.GetBytes(goal.Description));

                // Prepare the goal data dictionary
                var goalData = new Dictionary<string, string>
        {
            { "Name", encodedName },
            { "Description", encodedDescription },
            { "Difficulty", goal.Difficulty.ToString() },
            { "Completed", goal.IsCompleted.ToString() }
        };

                // Step 2: Save the completion date if it is available (not null)
                if (goal.CompletionDate.HasValue)
                {
                    goalData["CompletionDate"] = goal.CompletionDate.Value.ToString("yyyy-MM-dd-HH-mm");
                }

                // Add the goal data for this section
                data[sectionName] = goalData;
            }

            // Write the INI file with the sorted data
            IniFile.WriteIniFile(filePath, data);
        }


        private string AlphabeticIdentifier(int index)
        {
            string result = string.Empty;

            do
            {
                result = (char)('A' + (index % 26)) + result;
                index = index / 26 - 1;
            } while (index >= 0);

            return result;
        }

        // Event handler for clicking on a goal
        private void Goal_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Goal selectedGoal)
            {
                // Open the detail view
                var detailView = new GoalDetailView(selectedGoal);
                detailView.ShowDialog();

                // Refresh the GoalsList to reflect any updates to the Goal
                GoalsList.ItemsSource = null; // Clear the binding
                GoalsList.ItemsSource = Goals; // Reassign the collection

                if (selectedGoal.IsDeleted)
                {
                    Goals.Remove(selectedGoal); // Remove the goal from the collection
                }
            }
        }

        private void GoalCompleted_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Goal goal)
            {
                // If the goal is marked as completed, set the completion date
                if (goal.IsCompleted)
                {
                    goal.CompletionDate = DateTime.Now;
                }

                // Update the UI immediately
                GoalsList.Items.Refresh();
            }
        }

    }

    // Goal class
    public class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CompletionDate { get; set; }

        // The color for the border based on difficulty
        public string DifficultyBorderColor
        {
            get
            {
                string iniColor = Difficulty switch
                {
                    1 => MainWindow.LoadOptionFromIni("Color", "VeryEasyBorder"),
                    2 => MainWindow.LoadOptionFromIni("Color", "EasyBorder"),
                    3 => MainWindow.LoadOptionFromIni("Color", "NormalBorder"),
                    4 or 5 => MainWindow.LoadOptionFromIni("Color", "HardBorder"),
                    6 or 7 => MainWindow.LoadOptionFromIni("Color", "ToughBorder"),
                    8 or 9 => MainWindow.LoadOptionFromIni("Color", "InsaneBorder"),
                    10 => MainWindow.LoadOptionFromIni("Color", "ExtremeBorder"),
                    11 or 12 or 13 or 14 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIBorder"),
                    15 or 16 or 17 or 18 or 19 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIIBorder"),
                    20 => MainWindow.LoadOptionFromIni("Color", "ImpossibleBorder"),
                    _ => string.Empty
                };

                if (!string.IsNullOrEmpty(iniColor))
                {
                    return iniColor;
                }

                // Fallback to hardcoded colors if INI value is not found
                return Difficulty switch
                {
                    1 => "#80d0d0", // Very Easy
                    2 => "#40d080", // Easy
                    3 => "#40d040", // Normal
                    4 or 5 => "#f8d000", // Hard
                    6 or 7 => "#f02800", // Tough
                    8 or 9 => "#e048f0", // Insane
                    10 => "#8000a0", // Extreme
                    11 or 12 or 13 or 14 => "#780050", // Extreme Tier 2
                    15 or 16 or 17 or 18 or 19 => "#70001b", // Extreme Tier 3
                    20 => "#d35d6e", // Impossible
                    _ => "#ffffff",  // Unknown fallback
                };
            }
        }

        public string FillDifficultyColor
        {
            get
            {
                string iniColor = Difficulty switch
                {
                    1 => MainWindow.LoadOptionFromIni("Color", "VeryEasyFill"),
                    2 => MainWindow.LoadOptionFromIni("Color", "EasyFill"),
                    3 => MainWindow.LoadOptionFromIni("Color", "NormalFill"),
                    4 or 5 => MainWindow.LoadOptionFromIni("Color", "HardFill"),
                    6 or 7 => MainWindow.LoadOptionFromIni("Color", "ToughFill"),
                    8 or 9 => MainWindow.LoadOptionFromIni("Color", "InsaneFill"),
                    10 => MainWindow.LoadOptionFromIni("Color", "ExtremeFill"),
                    11 or 12 or 13 or 14 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIFill"),
                    15 or 16 or 17 or 18 or 19 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIIFill"),
                    20 => MainWindow.LoadOptionFromIni("Color", "ImpossibleFill"),
                    _ => string.Empty
                };

                if (!string.IsNullOrEmpty(iniColor))
                {
                    return iniColor;
                }

                // Fallback to hardcoded colors if INI value is not found
                return Difficulty switch
                {
                    1 => "#a0f8f8", // Very Easy
                    2 => "#50f8a0", // Easy
                    3 => "#50ff62", // Normal
                    4 or 5 => "#fff840", // Hard
                    6 or 7 => "#ff5046", // Tough
                    8 or 9 => "#ff5aff", // Insane
                    10 => "#a000c8", // Extreme
                    11 or 12 or 13 or 14 => "#960062", // Extreme Tier 2
                    15 or 16 or 17 or 18 or 19 => "#8c0023", // Extreme Tier 3
                    20 => "#000000", // Impossible
                    _ => "#ffffff",  // Unknown fallback
                };
            }
        }

        #region LDifficultyColorDeprecated
        // DEPRECATED: The brightened fill background color for the goal (usually 0.25 adjustment)
        /*public string LighterDifficultyColor
        {
            get
            {
                return Difficulty switch
                {
                    1 => "#a0f8f8", // Very Easy brightened (#80d0d0 -> #99e0e0)
                    2 => "#50f8a0", // Easy brightened (#40d080 -> #66c870)
                    3 => "#50f862", // Normal brightened (#40d050 -> #66c040)
                    4 => "#fff840", // Hard brightened (#f8d000 -> #ffd633)
                    5 => "#fff840", // Hard brightened (#f8d000 -> #ffd633)
                    6 => "#ff5046", // Tough (same)
                    7 => "#ff5046", // Tough (same)
                    8 => "#ff5aff", // Insane brightened (#e048f0 -> #ff5aff)
                    9 => "#ff5aff", // Insane brightened (#e048f0 -> #ff5aff)
                    10 => "#a000c8", // Extreme brightened (#8000a0 -> #b3008f)
                    11 => "#960062", // Extreme Tier 2 brightened (#780050 -> #9b003f)
                    12 => "#960062", // Extreme Tier 2 brightened (#780050 -> #9b003f)
                    13 => "#960062", // Extreme Tier 2 brightened (#780050 -> #9b003f)
                    14 => "#960062", // Extreme Tier 2 brightened (#780050 -> #9b003f)
                    15 => "#8c0023", // Extreme Tier 3 brightened (#70001b -> #8c0016)
                    16 => "#8c0023", // Extreme Tier 3 brightened (#70001b -> #8c0016)
                    17 => "#8c0023", // Extreme Tier 3 brightened (#70001b -> #8c0016)
                    18 => "#8c0023", // Extreme Tier 3 brightened (#70001b -> #8c0016)
                    19 => "#8c0023", // Extreme Tier 3 brightened (#70001b -> #8c0016)
                    20 => "#000000", // Impossible (same)
                    _ => "#ffffff",  // Unknown fallback
                };
            }
        }*/
        #endregion

        // The category name for the goal based on its difficulty
        public string DifficultyCategory => GetDifficultyCategory(Difficulty).Category;

        // The text color for extreme difficulties and above is usually white to remain legible
        public string TextColor
        {
            get
            {
                return Difficulty switch
                {
                    1 => MainWindow.LoadOptionFromIni("Color", "VeryEasyWText") switch
                    {
                        "True" => "White",   // Very Easy, Text Color is White if True
                        "False" => "Black",  // Very Easy, Text Color is Black if False
                        _ => "Black"         // Fallback to Black if no data or invalid
                    },
                    2 => MainWindow.LoadOptionFromIni("Color", "EasyWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "Black"
                    },
                    3 => MainWindow.LoadOptionFromIni("Color", "NormalWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "Black"
                    },
                    4 or 5 => MainWindow.LoadOptionFromIni("Color", "HardWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "Black"
                    },
                    6 or 7 => MainWindow.LoadOptionFromIni("Color", "ToughWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "Black"
                    },
                    8 or 9 => MainWindow.LoadOptionFromIni("Color", "InsaneWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "Black"
                    },
                    10 => MainWindow.LoadOptionFromIni("Color", "ExtremeWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "White"         // Default to White for Extreme
                    },
                    11 or 12 or 13 or 14 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "White"         // Default to White for Extreme Tier II
                    },
                    15 or 16 or 17 or 18 or 19 => MainWindow.LoadOptionFromIni("Color", "ExtremeIIIWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "White"         // Default to White for Extreme Tier III
                    },
                    20 => MainWindow.LoadOptionFromIni("Color", "ImpossibleWText") switch
                    {
                        "True" => "White",
                        "False" => "Black",
                        _ => "White"         // Default to White for Impossible
                    },
                    _ => "Black"  // Fallback for any invalid or unspecified Difficulty
                };
            }
        }

        // Old string - preserve: public string TextColor => (Difficulty >= 10 && Difficulty <= 20) ? "White" : "Black";

        // Determine the difficulty category and color
        private static DifficultyCategory GetDifficultyCategory(int difficulty)
        {
            // Default difficulty names
            string veryEasyName = "Very Easy";
            string easyName = "Easy";
            string normalName = "Normal";
            string hardName = "Hard";
            string toughName = "Tough";
            string insaneName = "Insane";
            string extremeName = "Extreme";
            string extremeIIName = "Extreme II";
            string extremeIIIName = "Extreme III";
            string impossibleName = "Impossible";

            // Override with INI values **only if they are NOT empty**
            string iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "VeryEasyName");
            if (iniValue != string.Empty) veryEasyName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "EasyName");
            if (iniValue != string.Empty) easyName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "NormalName");
            if (iniValue != string.Empty) normalName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "HardName");
            if (iniValue != string.Empty) hardName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "ToughName");
            if (iniValue != string.Empty) toughName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "InsaneName");
            if (iniValue != string.Empty) insaneName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "ExtremeName");
            if (iniValue != string.Empty) extremeName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "ExtremeIIName");
            if (iniValue != string.Empty) extremeIIName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "ExtremeIIIName");
            if (iniValue != string.Empty) extremeIIIName = iniValue;

            iniValue = MainWindow.LoadOptionFromIni("Color", "ImpossibleName");
            if (iniValue != string.Empty) impossibleName = iniValue;

            // Assign category based on difficulty level
            if (difficulty == 1) return new DifficultyCategory(veryEasyName, "#80d0d0");
            if (difficulty == 2) return new DifficultyCategory(easyName, "#40d080");
            if (difficulty == 3) return new DifficultyCategory(normalName, "#40d040");
            if (difficulty == 4 || difficulty == 5) return new DifficultyCategory(hardName, "#f8d000");
            if (difficulty == 6 || difficulty == 7) return new DifficultyCategory(toughName, "#f04038");
            if (difficulty == 8 || difficulty == 9) return new DifficultyCategory(insaneName, "#e048f0");
            if (difficulty == 10) return new DifficultyCategory(extremeName, "#8000a0");
            if (difficulty >= 11 && difficulty <= 14) return new DifficultyCategory(extremeIIName, "#780050");
            if (difficulty >= 15 && difficulty <= 19) return new DifficultyCategory(extremeIIIName, "#70001b");
            if (difficulty == 20) return new DifficultyCategory(impossibleName, "#000000");

            return new DifficultyCategory("Unknown", "#ffffff"); // Default for invalid difficulty levels
        }
    }

    public class DifficultyCategory
    {
        public string Category { get; }
        public string DifficultyColor { get; }

        public DifficultyCategory(string category, string color)
        {
            Category = category;
            DifficultyColor = color;
        }
    }
}
