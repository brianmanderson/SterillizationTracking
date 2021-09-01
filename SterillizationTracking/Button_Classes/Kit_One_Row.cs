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

namespace SterillizationTracking.Button_Classes
{
    class Kit_One_Row
    {
        public void build_row(StackPanel panel, BaseOnePartKit new_kit)
        {
            panel.Orientation = Orientation.Horizontal;
            Label kit_label = new Label();
            kit_label.Content = new_kit.name;
            panel.Children.Add(kit_label);

            Label number_of_uses_label = new Label();
            number_of_uses_label.Content = new_kit.current_use.ToString();
            panel.Children.Add(number_of_uses_label);

            Label number_of_sterlizations_label = new Label();
            number_of_sterlizations_label.Content = new_kit.current_steralization.ToString();
            panel.Children.Add(number_of_sterlizations_label);

            Button add_use = new Button();

        }
    }
}
