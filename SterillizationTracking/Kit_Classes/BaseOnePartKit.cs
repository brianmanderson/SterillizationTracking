using System;
using System.Collections.Generic;
using System.Windows;
using SterillizationTracking.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SterillizationTracking.Kit_Classes
{
    class BaseOnePartKit : INotifyPropertyChange
    {
        public int total_uses;
        public int warning_use;
        public int allowed_steralizations;
        public int current_use;
        public int current_steralization;
        public string status_color;
        public string name;

        public void add_use(object sender, RoutedEventArgs e)
        {
            current_use += 1;
        }

        public void add_sterilization(object sender, RoutedEventArgs e)
        {
            current_steralization += 1;
            current_use = 0;
        }
        public void check_status()
        {
            if ((current_use >= warning_use) && (current_steralization < allowed_steralizations))
            {
                status_color = "Yellow";
            }
            else if (current_use >= warning_use)
            {
                status_color = "Red";
            }
            else
            {
                status_color = "Green";
            }
        }
    }
}
