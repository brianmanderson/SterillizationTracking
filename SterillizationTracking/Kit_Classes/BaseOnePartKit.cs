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
        public int warning_use;


        public BaseOnePartKit(string name, string kitnumber, int currentUse) //string name, int allowed_steralizaitons, int warning_use
        {
            Name = name;
            KitNumber = $"Kit #: {kitnumber}";
            CurrentUse = currentUse;
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
        }

        public void remove_use(object sender, RoutedEventArgs e)
        {
            CurrentUse -= 1;
        }

        public void check_status()
        {
            if (CurrentUse >= warning_use * 0.75)
            {
                StatusColor = "Yellow";
            }
            else if (CurrentUse >= warning_use)
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
