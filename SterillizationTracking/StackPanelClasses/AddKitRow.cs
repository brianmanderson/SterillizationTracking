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
            TextBlock kit_textblock = new TextBlock();
            kit_textblock.Text = new_kit.name;
            kit_textblock.Padding = new Thickness(5, 10, 10, 5);
            Children.Add(kit_textblock);

            TextBlock myText = new TextBlock();
            NewBaseOnePart myDataObject = new NewBaseOnePart();
            Binding myBinding = new Binding("CurrentUse");
            myBinding.Source = myDataObject;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            myText.SetBinding(TextBlock.TextProperty, myBinding);
            Children.Add(myText);

            TextBlock number_of_uses_textblock = new TextBlock();
            Binding mybinding = new Binding("CurrentUse");
            mybinding.Source = new_kit;
            number_of_uses_textblock.SetBinding(TextBlock.TextProperty, mybinding.ToString());
            // number_of_uses_textblock.Text = $"Number of uses: {new_kit.CurrentUse}";
            number_of_uses_textblock.Padding = new Thickness(5, 10, 10, 5);
            Children.Add(number_of_uses_textblock);

            TextBlock number_of_sterlizations_textblock = new TextBlock();
            // number_of_sterlizations_textblock.Text = $"Number of steralizations: {new_kit.current_steralization}";
            // var mybinding = new Binding(new_kit.current_steralization.ToString());
            number_of_sterlizations_textblock.SetBinding(TextBlock.TextProperty, new_kit.current_steralization_string);
            number_of_sterlizations_textblock.Padding = new Thickness(5, 10, 10, 5);
            Children.Add(number_of_sterlizations_textblock);

            Button add_use_button = new Button();
            add_use_button.Click += myDataObject.add_use;
            add_use_button.Content = "Add use";
            add_use_button.Padding = new Thickness(10);
            Children.Add(add_use_button);

            Button add_steralization_button = new Button();
            add_steralization_button.Click += new_kit.add_sterilization;
            add_steralization_button.Content = "Add steralization";
            add_steralization_button.Padding = new Thickness(10);
            Children.Add(add_steralization_button);
        }
    }
}
