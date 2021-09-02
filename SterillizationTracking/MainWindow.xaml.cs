﻿using System;
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
using SterillizationTracking.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SterillizationTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<string> _kit_numbers = new List<string> { "" };
        private List<string> _kit_names = new List<string> { "Select a kit", "Cylinder", "Cervix Applicator Set", "Needle Kit", "Segmented Cylinder", 
            "Tandem and Ovoid", "Tandem and Ring", "Y Applicator"};
        public List<string> Kit_Numbers
        {
            get {
                return _kit_numbers; 
            }
            set
            {
                _kit_numbers = value;
                OnPropertyChanged("Kit_Numbers");
            }
        }
        public List<string> Kit_Names
        {
            get
            {
                return _kit_names;
            }
            set
            {
                _kit_names = value;
                OnPropertyChanged("Kit_Names");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Binding number_binding = new Binding("Kit_Numbers");
            number_binding.Source = this;
            KitNumber_ComboBox.SetBinding(ComboBox.ItemsSourceProperty, number_binding);

            Binding kit_name_binding = new Binding("Kit_Names");
            Kit_Names = _kit_names;
            kit_name_binding.Source = this;
            Kit_ComboBox.SetBinding(ComboBox.ItemsSourceProperty, kit_name_binding);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        private void Add_Kit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Kit_ComboBox.SelectedItem).Contains("Cylinder"))
            {
                string number = KitNumber_ComboBox.SelectedItem.ToString();
                Cylinder new_cylinder = new Cylinder();
                new_cylinder.Name = "Cylinder";
                AddKitRow new_row = new AddKitRow(new_cylinder);
                KitStackPanel.Children.Add(new_row);
                // new_button.Click += new_cylinder.add_use(1);

            }
            Kit_ComboBox.SelectedIndex = 0;
        }
        private void Kit_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string kit_name = Kit_Names[Kit_ComboBox.SelectedIndex];
            if (kit_name.Contains("Select a kit"))
            {
                if (KitNumber_ComboBox != null)
                {
                    KitNumber_ComboBox.IsEnabled = false;
                    Kit_Numbers = { "" };
                }
                Add_Kit_Button.IsEnabled = false;
            }
            else
            {
                KitNumber_ComboBox.IsEnabled = true;
                CheckNumberList number_returner = new CheckNumberList();
                Kit_Numbers = number_returner.return_list(kit_name);
                KitNumber_ComboBox.SelectedIndex = 0;
            }
        }

        private void KitNumber_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string kitnumber_selection_info = Convert.ToString(KitNumber_ComboBox.SelectedItem);
            if (kitnumber_selection_info.Contains("Select"))
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
