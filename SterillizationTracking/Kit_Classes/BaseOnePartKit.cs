using System;
using System.IO;
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
        private string currentUse_string, usesLeft_string;
        private System.Windows.Media.Brush statusColor;
        private string name;
        private string kitnumber;
        private string useFileLocation;
        private string _present;
        private bool can_reorder;
        private int usesLeft;

        public int total_uses;
        public int warning_uses;
        public string file_path = @"\\ro-ariaimg-v\va_data$\HDR\Kit_Status";
        public string KitDirectoryPath;
        public string ReorderDirectoryPath;

        public string CurrentUseString
        {
            get { return currentUse_string; }
            set
            {
                currentUse_string = value;
                OnPropertyChanged("CurrentUseString");
            }
        }
        public string UsesLeftString
        {
            get { return usesLeft_string; }
            set
            {
                usesLeft_string = value;
                OnPropertyChanged("UsesLeftString");
            }
        }
        public string Present
        {
            get { return _present; }
            set
            {
                _present = value;
                OnPropertyChanged("Present");
            }
        }
        public BaseOnePartKit(string name, string kitnumber) //string name, int allowed_steralizaitons, int warning_use
        {
            Name = name;
            StatusColor = statusColor;
            KitNumber = $"Kit #: {kitnumber}";
            KitDirectoryPath = Path.Combine(file_path, name, $"Kit {kitnumber}");
            UseFileLocation = Path.Combine(KitDirectoryPath, "Uses.txt");

            warning_uses = 80;
            total_uses = 100;
            CanReorder = false;
            if (name == "Cylinder")
            {
                total_uses = 10;
                warning_uses = 7;
            }
            else if (name == "Tandem and Ovoid")
            {
                total_uses = 100;
                warning_uses = 80;
            }
            build_read_use_file();
        }

        public void build_read_use_file()
        {
            if (File.Exists(UseFileLocation))
            {
                string[] lines = File.ReadAllLines(UseFileLocation);
                CurrentUse = Convert.ToInt32(lines[0].Split("Use:")[1]);
                total_uses = Convert.ToInt32(lines[1].Split("Uses:")[1]);
                warning_uses = Convert.ToInt32(lines[2].Split("Uses:")[1]);
                Present = lines[3].Split("updated:")[1];
            }
            else
            {
                CurrentUse = 0;
                string[] info ={ $"Current Use:{0}", $"Total Uses:{total_uses}", $"Warning Uses:{warning_uses}", $"Last updated:{Present}" };
                if (!Directory.Exists(KitDirectoryPath))
                {
                    Directory.CreateDirectory(KitDirectoryPath);
                }
                File.WriteAllLines(UseFileLocation, info);
            }
            check_status();
            CurrentUseString = $"Current use: {CurrentUse}";
            UsesLeft = total_uses - CurrentUse;
            UsesLeftString = $"Uses left: {UsesLeft}";
        }

        public void create_reorder_file()
        {
            ReorderDirectoryPath = Path.Combine(KitDirectoryPath, "Reorders");
            if (!Directory.Exists(ReorderDirectoryPath))
            {
                Directory.CreateDirectory(ReorderDirectoryPath);
            }
            DateTime moment = DateTime.Now;
            Present = (moment.ToLongDateString() + " " + moment.ToLongTimeString()).Replace(":", ".");
            string file_path = Path.Combine(ReorderDirectoryPath, Present.Replace(":", ".") + ".txt");
            File.Create(file_path);
        }
        public void update_file()
        {
            string[] info = { $"Current Use:{CurrentUse}", $"Total Uses:{total_uses}", $"Warning Uses:{warning_uses}", $"Last updated:{Present}" };
            if (!Directory.Exists(KitDirectoryPath))
            {
                Directory.CreateDirectory(KitDirectoryPath);
            }
            File.WriteAllLines(UseFileLocation, info);
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

        public int UsesLeft
        {
            get { return usesLeft; }
            set
            {
                usesLeft = value;
                OnPropertyChanged("UsesLeft");
            }
        }

        public bool CanReorder
        {
            get { return can_reorder; }
            set
            {
                can_reorder = value;
                OnPropertyChanged("CanReorder");
            }
        }

        public string UseFileLocation
        {
            get { return useFileLocation; }
            set
            {
                useFileLocation = value;
                OnPropertyChanged("UseFileLocation");
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

        public System.Windows.Media.Brush StatusColor
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
            DateTime moment = DateTime.Now;
            Present = moment.ToLongDateString() + " " + moment.ToLongTimeString();
            CurrentUse += 1;
            CurrentUseString = $"Current use: {CurrentUse}";
            UsesLeft = total_uses - CurrentUse;
            UsesLeftString = $"Uses left: {UsesLeft}";
            update_file();
            check_status();
        }

        public void remove_use(object sender, RoutedEventArgs e)
        {
            CurrentUse -= 1;
            CurrentUseString = $"Current use: {CurrentUse}";
            UsesLeft = total_uses - CurrentUse;
            UsesLeftString = $"Uses left: {UsesLeft}";
            update_file();
            check_status();
        }

        public void reorder(object sender, RoutedEventArgs e)
        {
            CurrentUse = 0;
            CurrentUseString = $"Current use: {CurrentUse}";
            UsesLeft = total_uses - CurrentUse;
            UsesLeftString = $"Uses left: {UsesLeft}";
            update_file();
            create_reorder_file();
            check_status();
        }

        public void check_status()
        {
            if (CurrentUse >= total_uses)
            {
                StatusColor = System.Windows.Media.Brushes.Red;
                CanReorder = true;
            }
            else if (CurrentUse >= warning_uses * 0.75)
            {
                StatusColor = System.Windows.Media.Brushes.Yellow;
                CanReorder = false;
            }
            else
            {
                StatusColor = System.Windows.Media.Brushes.Green;
                CanReorder = false;
            }
        }
    }
}
