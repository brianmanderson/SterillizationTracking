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
        public AddKitRow(BaseOnePartKit new_kit)
        {
            Orientation = Orientation.Horizontal;

            Label kit_label = new Label();
            Binding label_binding = new Binding("Name");
            label_binding.Source = new_kit;
            kit_label.SetBinding(Label.ContentProperty, label_binding);
            kit_label.Padding = new Thickness(10);
            Children.Add(kit_label);


            Label current_use_label = new Label();
            Binding myBinding = new Binding("CurrentUse");
            myBinding.Source = new_kit;
            current_use_label.SetBinding(Label.ContentProperty, myBinding);
            current_use_label.Padding = new Thickness(10);
            Children.Add(current_use_label);

            Button add_use_button = new Button();
            add_use_button.Click += new_kit.add_use;
            add_use_button.Content = "Add use";
            add_use_button.Padding = new Thickness(10);
            Children.Add(add_use_button);
        }
    }
}
