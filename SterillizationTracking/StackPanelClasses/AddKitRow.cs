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

namespace SterillizationTracking.StackPanelClasses
{
    class AddKitRow : StackPanel
    {
        public Button add_use_button, remove_use_button;
        public Label current_use_label, kit_label, override_label;
        public CheckBox override_checkbox;
        public AddKitRow(BaseOnePartKit new_kit)
        {
            Orientation = Orientation.Horizontal;

            kit_label = new Label();
            Binding label_binding = new Binding("Name");
            label_binding.Source = new_kit;
            kit_label.SetBinding(Label.ContentProperty, label_binding);
            kit_label.Padding = new Thickness(10);
            Children.Add(kit_label);


            current_use_label = new Label();
            Binding myBinding = new Binding("CurrentUse");
            myBinding.Source = new_kit;
            current_use_label.SetBinding(Label.ContentProperty, myBinding);
            current_use_label.Padding = new Thickness(10);
            Children.Add(current_use_label);

            add_use_button = new Button();
            add_use_button.Click += new_kit.add_use;
            add_use_button.Click += disable_add_use_button;
            add_use_button.Content = "Add use";
            add_use_button.Padding = new Thickness(10);
            Children.Add(add_use_button);

            remove_use_button = new Button();
            remove_use_button.Click += new_kit.remove_use;
            remove_use_button.Click += disable_remove_use_button;
            remove_use_button.Content = "Remove use";
            remove_use_button.Padding = new Thickness(10);
            Children.Add(remove_use_button);

            override_label = new Label();
            override_label.Content = "Override?";
            override_label.Padding = new Thickness(10);
            Children.Add(override_label);

            override_checkbox = new CheckBox();
            override_checkbox.Padding = new Thickness(10);
            override_checkbox.Checked += CheckBox_Checked;
            Children.Add(override_checkbox);


        }
        public void disable_add_use_button(object sender, RoutedEventArgs e)
        {
            add_use_button.IsEnabled = false;
            CheckBox_Checked(sender, e);
        }
        public void disable_remove_use_button(object sender, RoutedEventArgs e)
        {
            remove_use_button.IsEnabled = false;
            CheckBox_Checked(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool check = override_checkbox.IsChecked ?? false;
            if (check)
            {
                add_use_button.IsEnabled = true;
                remove_use_button.IsEnabled = true;
            }
        }
    }
}
