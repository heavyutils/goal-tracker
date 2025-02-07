using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Goal_Tracker
{
    /// <summary>
    /// Interaction logic for ColorPresetSelector.xaml
    /// </summary>
    public partial class ColorPresetSelector : Window
    {
        public ColorPresetSelector()
        {
            InitializeComponent();
        }

        private void PresetA_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.SaveToIni("Color", "BgColor", "#f5f5f5");
            OptionsWindow.SaveToIni("Color", "VeryEasyBorder", "#80d0d0");
            OptionsWindow.SaveToIni("Color", "EasyBorder", "#40d080");
            OptionsWindow.SaveToIni("Color", "NormalBorder", "#40d040");
            OptionsWindow.SaveToIni("Color", "HardBorder", "#f8d000");
            OptionsWindow.SaveToIni("Color", "ToughBorder", "#f02800");
            OptionsWindow.SaveToIni("Color", "InsaneBorder", "#e048f0");
            OptionsWindow.SaveToIni("Color", "ExtremeBorder", "#8000a0");
            OptionsWindow.SaveToIni("Color", "ExtremeIIBorder", "#780050");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIBorder", "#70001b");
            OptionsWindow.SaveToIni("Color", "ImpossibleBorder", "#d35d78");

            OptionsWindow.SaveToIni("Color", "VeryEasyFill", "#a0f8f8");
            OptionsWindow.SaveToIni("Color", "EasyFill", "#50f8a0");
            OptionsWindow.SaveToIni("Color", "NormalFill", "#50f862");
            OptionsWindow.SaveToIni("Color", "HardFill", "#fff840");
            OptionsWindow.SaveToIni("Color", "ToughFill", "#ff5046");
            OptionsWindow.SaveToIni("Color", "InsaneFill", "#ff5aff");
            OptionsWindow.SaveToIni("Color", "ExtremeFill", "#a000c8");
            OptionsWindow.SaveToIni("Color", "ExtremeIIFill", "#960062");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIFill", "#8c0023");
            OptionsWindow.SaveToIni("Color", "ImpossibleFill", "#000000");

            OptionsWindow.SaveToIni("Color", "VeryEasyWText", "False");
            OptionsWindow.SaveToIni("Color", "EasyWText", "False");
            OptionsWindow.SaveToIni("Color", "NormalWText", "False");
            OptionsWindow.SaveToIni("Color", "HardWText", "False");
            OptionsWindow.SaveToIni("Color", "ToughWText", "False");
            OptionsWindow.SaveToIni("Color", "InsaneWText", "False");
            OptionsWindow.SaveToIni("Color", "ExtremeWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ImpossibleWText", "True");

            Close();
            var optionsMenu = new OptionsWindow();
            optionsMenu.ShowDialog();
        }

        private void PresetB_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.SaveToIni("Color", "BgColor", "#f5f5f5");
            OptionsWindow.SaveToIni("Color", "VeryEasyBorder", "#80d0d0");
            OptionsWindow.SaveToIni("Color", "EasyBorder", "#40d080");
            OptionsWindow.SaveToIni("Color", "NormalBorder", "#40d040");
            OptionsWindow.SaveToIni("Color", "HardBorder", "#f8d000");
            OptionsWindow.SaveToIni("Color", "ToughBorder", "#f02800");
            OptionsWindow.SaveToIni("Color", "InsaneBorder", "#e048f0");
            OptionsWindow.SaveToIni("Color", "ExtremeBorder", "#8000a0");
            OptionsWindow.SaveToIni("Color", "ExtremeIIBorder", "#780050");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIBorder", "#70001b");
            OptionsWindow.SaveToIni("Color", "ImpossibleBorder", "#d35d78");

            Close();
            var optionsMenu = new OptionsWindow();
            optionsMenu.ShowDialog();

        }

        private void PresetC_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.SaveToIni("Color", "BgColor", "#202020");
            OptionsWindow.SaveToIni("Color", "VeryEasyBorder", "#80c0d0");
            OptionsWindow.SaveToIni("Color", "EasyBorder", "#40c080");
            OptionsWindow.SaveToIni("Color", "NormalBorder", "#40d040");
            OptionsWindow.SaveToIni("Color", "HardBorder", "#f8d000");
            OptionsWindow.SaveToIni("Color", "ToughBorder", "#f02800");
            OptionsWindow.SaveToIni("Color", "InsaneBorder", "#e048f0");
            OptionsWindow.SaveToIni("Color", "ExtremeBorder", "#8000a0");
            OptionsWindow.SaveToIni("Color", "ExtremeIIBorder", "#780050");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIBorder", "#70001b");
            OptionsWindow.SaveToIni("Color", "ImpossibleBorder", "#d35d6e");

            OptionsWindow.SaveToIni("Color", "VeryEasyFill", "#283e3e");
            OptionsWindow.SaveToIni("Color", "EasyFill", "#142b28");
            OptionsWindow.SaveToIni("Color", "NormalFill", "#143e19");
            OptionsWindow.SaveToIni("Color", "HardFill", "#403e10");
            OptionsWindow.SaveToIni("Color", "ToughFill", "#401411");
            OptionsWindow.SaveToIni("Color", "InsaneFill", "#401640");
            OptionsWindow.SaveToIni("Color", "ExtremeFill", "#a000c8");
            OptionsWindow.SaveToIni("Color", "ExtremeIIFill", "#960062");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIFill", "#8c0023");
            OptionsWindow.SaveToIni("Color", "ImpossibleFill", "#000000");

            OptionsWindow.SaveToIni("Color", "VeryEasyWText", "True");
            OptionsWindow.SaveToIni("Color", "EasyWText", "True");
            OptionsWindow.SaveToIni("Color", "NormalWText", "True");
            OptionsWindow.SaveToIni("Color", "HardWText", "True");
            OptionsWindow.SaveToIni("Color", "ToughWText", "True");
            OptionsWindow.SaveToIni("Color", "InsaneWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ImpossibleWText", "True");

            Close();
            var optionsMenu = new OptionsWindow();
            optionsMenu.ShowDialog();
        }

        private void PresetD_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.SaveToIni("Color", "BgColor", "#202020");
            OptionsWindow.SaveToIni("Color", "VeryEasyBorder", "#80c0d0");
            OptionsWindow.SaveToIni("Color", "EasyBorder", "#40c080");
            OptionsWindow.SaveToIni("Color", "NormalBorder", "#40d040");
            OptionsWindow.SaveToIni("Color", "HardBorder", "#f8d000");
            OptionsWindow.SaveToIni("Color", "ToughBorder", "#f02800");
            OptionsWindow.SaveToIni("Color", "InsaneBorder", "#e048f0");
            OptionsWindow.SaveToIni("Color", "ExtremeBorder", "#8000a0");
            OptionsWindow.SaveToIni("Color", "ExtremeIIBorder", "#780050");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIBorder", "#70001b");
            OptionsWindow.SaveToIni("Color", "ImpossibleBorder", "#d35d6e");

            OptionsWindow.SaveToIni("Color", "VeryEasyFill", "#283e3e");
            OptionsWindow.SaveToIni("Color", "EasyFill", "#143e28");
            OptionsWindow.SaveToIni("Color", "NormalFill", "#143e19");
            OptionsWindow.SaveToIni("Color", "HardFill", "#403e10");
            OptionsWindow.SaveToIni("Color", "ToughFill", "#401411");
            OptionsWindow.SaveToIni("Color", "InsaneFill", "#401640");
            OptionsWindow.SaveToIni("Color", "ExtremeFill", "#280832");
            OptionsWindow.SaveToIni("Color", "ExtremeIIFill", "#250818");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIFill", "#1c0006");
            OptionsWindow.SaveToIni("Color", "ImpossibleFill", "#000000");

            OptionsWindow.SaveToIni("Color", "VeryEasyWText", "True");
            OptionsWindow.SaveToIni("Color", "EasyWText", "True");
            OptionsWindow.SaveToIni("Color", "NormalWText", "True");
            OptionsWindow.SaveToIni("Color", "HardWText", "True");
            OptionsWindow.SaveToIni("Color", "ToughWText", "True");
            OptionsWindow.SaveToIni("Color", "InsaneWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ExtremeIIIWText", "True");
            OptionsWindow.SaveToIni("Color", "ImpossibleWText", "True");

            Close();
            var optionsMenu = new OptionsWindow();
            optionsMenu.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
            var optionsMenu = new OptionsWindow();
            optionsMenu.ShowDialog();
        }
    }
}
