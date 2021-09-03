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
        private string statusColor = "Green";
        private string name;
        private string kitnumber;
        private string useFileLocation;
        private bool can_reorder;

        public int total_uses;
        public int warning_uses;
        public string file_path = @"\\ro-ariaimg-v\va_data$\HDR\Kit_Status";
        public string KitDirectoryPath;
        public string ReorderDirectoryPath;

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
                warning_uses = 5;
            }
            else if (name == "Tandem and Ovoid")
            {
                total_uses = 100;
                warning_uses = 80;
            }
            CurrentUse = 0;
            build_read_use_file();
        }

        public void build_read_use_file()
        {
            if (File.Exists(UseFileLocation))
            {
                string[] lines = File.ReadAllLines(UseFileLocation);
                CurrentUse = Convert.ToInt32(lines[0].Split(':')[1]);
                total_uses = Convert.ToInt32(lines[1].Split(':')[1]);
                warning_uses = Convert.ToInt32(lines[2].Split(':')[1]);
            }
            else
            {
                string[] info ={ $"Current Use:{0}", $"Total Uses:{total_uses}", $"Warning Uses:{warning_uses}" };
                if (!Directory.Exists(KitDirectoryPath))
                {
                    Directory.CreateDirectory(KitDirectoryPath);
                }
                File.WriteAllLines(UseFileLocation, info);
            }
            check_status();
        }

        public void create_reorder_file()
        {
            ReorderDirectoryPath = Path.Combine(KitDirectoryPath, "Reorders");
            if (!Directory.Exists(ReorderDirectoryPath))
            {
                Directory.CreateDirectory(ReorderDirectoryPath);
            }
            DateTime moment = DateTime.Now;
            string moment_string = moment.ToLongDateString() + " " + moment.ToLongTimeString();
            string file_path = Path.Combine(ReorderDirectoryPath, moment_string.Replace(":", ".") + ".txt");
            File.Create(file_path);
        }
        public void update_file()
        {
            string[] info = { $"Current Use:{CurrentUse}", $"Total Uses:{total_uses}", $"Warning Uses:{warning_uses}" };
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
            update_file();
            check_status();
        }

        public void remove_use(object sender, RoutedEventArgs e)
        {
            CurrentUse -= 1;
            update_file();
            check_status();
        }

        public void reorder(object sender, RoutedEventArgs e)
        {
            CurrentUse = 0;
            update_file();
            create_reorder_file();
            check_status();
        }

        public void check_status()
        {
            if (CurrentUse >= total_uses)
            {
                StatusColor = "Red";
                CanReorder = true;
            }
            else if (CurrentUse >= warning_uses * 0.75)
            {
                StatusColor = "Yellow";
                CanReorder = false;
            }
            else
            {
                StatusColor = "Green";
                CanReorder = false;
            }
        }
    }
}
