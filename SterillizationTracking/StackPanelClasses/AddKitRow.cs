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
        public void build_row(BaseOnePartKit new_kit)
        {
            Orientation = Orientation.Horizontal;
            Label kit_label = new Label();
            kit_label.Content = new_kit.name;
            Children.Add(kit_label);

            Label number_of_uses_label = new Label();
            number_of_uses_label.Content = new_kit.current_use.ToString();
            Children.Add(number_of_uses_label);

            Label number_of_sterlizations_label = new Label();
            number_of_sterlizations_label.Content = new_kit.current_steralization.ToString();
            Children.Add(number_of_sterlizations_label);

            Button add_use_button = new Button();
            add_use_button.Click += new_kit.add_use;
            add_use_button.Content = "Add use";
            Children.Add(add_use_button);

            Button add_steralization_button = new Button();
            add_steralization_button.Click += new_kit.add_sterilization;
            add_steralization_button.Content = "Add steralization";
            Children.Add(add_steralization_button);
        }
    }
}
