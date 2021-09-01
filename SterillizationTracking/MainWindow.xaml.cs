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
using SterillizationTracking.StackPanelClasses;

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
                new_cylinder.name = "Cylinder";
                AddKitRow new_row = new AddKitRow();
                new_row.build_row(new_cylinder);
                KitStackPanel.Children.Add(new_row);
                // new_button.Click += new_cylinder.add_use(1);

            }
            Kit_ComboBox.SelectedIndex = 0;

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
