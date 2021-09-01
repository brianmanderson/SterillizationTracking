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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SterillizationTracking.Kit_Classes;

namespace SterillizationTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Kit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Kit_ComboBox.SelectedItem).Contains("Cylinder"))
            {
                Cylinder new_cylinder = new Cylinder();
                
            }
            Kit_ComboBox.SelectedIndex = 0;
            int marginx = 5;
            int marginy = 5;
            for (int i = 0; i < 50; i ++)
            {
                Button new_label = new Button();
                new_label.Margin = new Thickness(marginx, marginy, 0, 0);
                new_label.Content = $"Test_{i}";
                new_label.HorizontalAlignment = HorizontalAlignment.Left;
                new_label.VerticalAlignment = VerticalAlignment.Top;
                new_label.Click += on_click;
                KitStackPanel.Children.Add(new_label);
            }
        }
        private void on_click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked!");
        }
        private void Kit_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToString(Kit_ComboBox.SelectedItem).Contains("Select a kit"))
            {
                Add_Kit_Button.IsEnabled = false;
            }
            else
            {
                Add_Kit_Button.IsEnabled = true;
            }
        }
    }
}
