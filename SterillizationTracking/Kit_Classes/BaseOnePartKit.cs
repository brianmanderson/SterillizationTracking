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
        private int currentUse = 0;
        private int currentSteralization = 0;
        private string statusColor = "Green";

        public int allowed_steralizations;
        public int total_uses;
        public int warning_use;


        public BaseOnePartKit()
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
        public int CurrentSteralization
        {
            get { return currentSteralization; }
            set
            {
                currentSteralization = value;
                OnPropertyChanged("CurrentSteralization");
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
        public void add_steralization(object sender, RoutedEventArgs e)
        {
            CurrentSteralization += 1;
        }
        public void check_status()
        {
            if ((CurrentUse >= warning_use) && (CurrentSteralization < allowed_steralizations))
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
