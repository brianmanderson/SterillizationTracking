using System;
using System.Collections.Generic;
using System.Windows;
using SterillizationTracking.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SterillizationTracking.Kit_Classes
{
    public class BaseOnePartKit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int current_use = 0;

        public int total_uses;
        public int warning_use;
        public int allowed_steralizations;
        public int current_steralization;
        public string status_color;
        public string name;
        public string current_use_string = "Current use: 0";
        public string current_steralization_string = "Current steralization: 0";

        public BaseOnePartKit()
        {
        }

        public BaseOnePartKit(int value)
        {
            current_use = value;
        }
        public int CurrentUse
        {
            get 
            {
                return current_use;
            }
            set
            {
                current_use = value;
                OnPropertyChanged("CurrentUse");
            }
        }
        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
        public void add_use(object sender, RoutedEventArgs e)
        {
            CurrentUse += 1;
            current_use_string = $"Current use: {CurrentUse}";
        }

        public void add_sterilization(object sender, RoutedEventArgs e)
        {
            current_steralization += 1;
            current_steralization_string = $"Current steralization: {current_steralization}";
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

    public class NewBaseOnePart : INotifyPropertyChanged
    {
        private int currentUse = 0;

        public NewBaseOnePart()
        { 
        }

        public int CurrentUse
        {
            get { return currentUse; }
            set
            {
                currentUse = value;
                OnPropertyChanged("CurrentUse");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void add_use(object sender, RoutedEventArgs e)
        {
            CurrentUse += 1;
        }
    }
}
