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
                StackPanel new_stack = new StackPanel();
                Label label = new Label();
                label
                Cylinder new_cylinder = new Cylinder();
                Button new_button = new Button();
                // new_button.Click += new_cylinder.add_use(1);

            }
            Kit_ComboBox.SelectedIndex = 0;
            int marginx = 5;
            int marginy = 5;
            for (int i = 0; i < 50; i ++)
            {
                StackPanel new_stack = new StackPanel();
                Label new_label2 = new Label();
                new_label2.Margin = new Thickness(marginx, marginy, 0, 0);
                new_label2.Content = $"Test_{i}";
                new_label2.HorizontalAlignment = HorizontalAlignment.Left;
                new_label2.VerticalAlignment = VerticalAlignment.Top;
                new_stack.Children.Add(new_label2);
                KitStackPanel.Children.Add(new_stack);
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
