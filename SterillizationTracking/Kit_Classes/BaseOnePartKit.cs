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
        private int currentUse;
        private string statusColor = "Green";
        private string name;
        private string kitnumber;

        public int total_uses;
        public int warning_uses;
        public string file_path = @"\\ro-ariaimg-v\va_data$\HDR\Kit_Status";


        public BaseOnePartKit(string name, string kitnumber) //string name, int allowed_steralizaitons, int warning_use
        {
            Name = name;
            StatusColor = statusColor;
            KitNumber = $"Kit #: {kitnumber}";
            warning_uses = 80;
            total_uses = 100;
            if (name == "Cylinder")
            {
                total_uses = 10;
                warning_uses = 5;
            }
            else if (name == "Tandem and Ovoid")
            {
                total_uses = 100;
                warning_uses = 80;
            }
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


        public string KitNumber
        {
            get { return kitnumber; }
            set
            {
                kitnumber = value;
                OnPropertyChanged("KitNumber");
            }
        }

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String StatusColor
        {
            get { return statusColor; }
            set
            {
                statusColor = value;
                OnPropertyChanged("StatusColor");
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
            check_status();
        }

        public void remove_use(object sender, RoutedEventArgs e)
        {
            CurrentUse -= 1;
            check_status();
        }

        public void check_status()
        {
            if (CurrentUse >= warning_uses * 0.75)
            {
                StatusColor = "Yellow";
            }
            else if (CurrentUse >= warning_uses)
            {
                StatusColor = "Red";
            }
            else
            {
                StatusColor = "Green";
            }
        }
    }
}
