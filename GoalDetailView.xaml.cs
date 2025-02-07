using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Interop;

namespace Goal_Tracker
{
    public partial class GoalDetailView : Window
    {
        public Goal SelectedGoal { get; set; }

        // The collection of goals to remove the goal from
        public ObservableCollection<Goal> GoalsCollection { get; set; }

        public GoalDetailView(Goal goal)
        {
            InitializeComponent();
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            SelectedGoal = goal;
            this.DataContext = SelectedGoal; // Bind the data to the view

            // Populate ComboBox with difficulty levels (1 to 20)
            if (MainWindow.LoadOptionFromIni("Color", "MoreTier") == "True")
            {
                GoalDifficultyComboBox.ItemsSource = new int[]
                {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                };
            }
            else
            {
                GoalDifficultyComboBox.ItemsSource = new int[]
                {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                };
            }
        }

        // Handle save button click (you can implement logic to save changes here)
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // For now, we just close the window after saving
            // Later, you can add logic to persist changes to your goal
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Remove the selected goal from the GoalsCollection
            SelectedGoal.IsDeleted = true;

            // Close the detail view window
            this.Close();
        }

        // Handle close button click
        /*private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}
