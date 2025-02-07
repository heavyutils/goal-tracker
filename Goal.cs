using Goal_Tracker;
using System.ComponentModel;

public class Goal : INotifyPropertyChanged
{
    public int _difficulty { get; set;  }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CompletionDate { get; set; }

    // Implement INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public int Difficulty
    {
        get => _difficulty;
        set
        {
            if (_difficulty != value)
            {
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty)); // Notify change for Difficulty
                OnPropertyChanged(nameof(DifficultyBorderColor)); // Notify border color change
                OnPropertyChanged(nameof(LighterDifficultyColor)); // Notify fill color change
                OnPropertyChanged(nameof(DifficultyCategory)); // Notify category change
                OnPropertyChanged(nameof(TextColor)); // Notify text color change
            }
        }
    }

    public string DifficultyBorderColor
    {
        get
        {
            return Difficulty switch
            {
                1 => "#80d0d0", // Very Easy
                2 => "#40d080", // Easy
                3 => "#40d040", // Normal
                4 => "#f8d000", // Hard
                5 => "#f8d000", // Hard
                6 => "#F02800", // Tough (custom)
                7 => "#F02800", // Tough (custom)
                8 => "#e048f0", // Insane
                9 => "#e048f0", // Insane
                10 => "#8000a0", // Extreme
                11 => "#780050", // Extreme Tier 2
                12 => "#780050", // Extreme Tier 2
                13 => "#780050", // Extreme Tier 2
                14 => "#780050", // Extreme Tier 2
                15 => "#70001b", // Extreme Tier 3
                16 => "#70001b", // Extreme Tier 3
                17 => "#70001b", // Extreme Tier 3
                18 => "#70001b", // Extreme Tier 3
                19 => "#70001b", // Extreme Tier 3
                20 => "#D35D6E", // Impossible (custom)
                _ => "#ffffff",  // Unknown fallback
            };
        }
    }

    public string LighterDifficultyColor
    {
        get
        {
            return Difficulty switch
            {
                1 => "#a0f8f8", // Very Easy brightened (#80d0d0 -> #99e0e0)
                2 => "#50f8a0", // Easy brightened (#40d080 -> #66c870)
                3 => "#50ff62", // Normal brightened (#40d050 -> #66c040)
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
    }

    public string DifficultyCategory => GetDifficultyCategory(Difficulty).Category;

    public string TextColor => Difficulty >= 10 ? "White" : "Black";

    private static DifficultyCategory GetDifficultyCategory(int difficulty)
    {
        if (difficulty == 1) return new DifficultyCategory("Very Easy", "#80d0d0");
        if (difficulty == 2) return new DifficultyCategory("Easy", "#40d080");
        if (difficulty == 3) return new DifficultyCategory("Normal", "#40d040");
        if (difficulty == 4 || difficulty == 5) return new DifficultyCategory("Hard", "#f8d000");
        if (difficulty == 6 || difficulty == 7) return new DifficultyCategory("Tough", "#f04038");
        if (difficulty == 8 || difficulty == 9) return new DifficultyCategory("Insane", "#e048f0");
        if (difficulty == 10) return new DifficultyCategory("Extreme", "#8000a0");
        if (difficulty >= 11 && difficulty <= 14) return new DifficultyCategory("Extreme II", "#780050");
        if (difficulty >= 15 && difficulty <= 19) return new DifficultyCategory("Extreme III", "#70001b");
        if (difficulty == 20) return new DifficultyCategory("Impossible", "#000000");
        return new DifficultyCategory("Unknown", "#ffffff");
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}