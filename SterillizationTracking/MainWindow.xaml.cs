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
            Kit_ComboBox.SelectedItem = 0;
            for (int i = 0; i < 10; i ++)
            {
                Label new_label = new Label();
                new_label.Content = $"Test_{i}";
                Kit_Grid.Children.Add(new_label);
            }
        }

        private void Kit_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
