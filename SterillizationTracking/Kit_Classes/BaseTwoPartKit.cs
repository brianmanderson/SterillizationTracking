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
    public class BaseTwoPartKit : INotifyPropertyChanged
    {
        private int currentUse_metal;
        private int currentUse_plastic;
        private System.Windows.Media.Brush statusColor_Metal, statusColor_Plastic;
        private string name;
        private string kitnumber;
        private string useFileLocation;
        private string _present;
        private bool can_reorder_metal;
        private bool can_reorder_plastic;
        private int usesLeft_metal;
        private int usesLeft_plastic;

        public int total_uses_metal;
        public int total_uses_plastic;
        public int warning_uses_metal;
        public int warning_uses_plastic;
        public string file_path = @"\\ro-ariaimg-v\va_data$\HDR\Kit_Status";
        public string KitDirectoryPath;
        public string ReorderDirectoryPath;

        public string Present
        {
            get { return _present; }
            set
            {
                _present = value;
                OnPropertyChanged("Present");
            }
        }
        public int CurrentUseMetal
        {
            get { return currentUse_metal; }
            set
            {
                currentUse_metal = value;
                OnPropertyChanged("CurrentUseMetal");
            }
        }
        public int CurrentUsePlastic
        {
            get { return currentUse_plastic; }
            set
            {
                currentUse_plastic = value;
                OnPropertyChanged("CurrentUsePlastic");
            }
        }
        public int UsesLeftMetal
        {
            get { return usesLeft_metal; }
            set
            {
                usesLeft_metal = value;
                OnPropertyChanged("UsesLeftMetal");
            }
        }
        public int UsesLeftPlastic
        {
            get { return usesLeft_plastic; }
            set
            {
                usesLeft_plastic = value;
                OnPropertyChanged("UsesLeftPlastic");
            }
        }
        public bool CanReorderMetal
        {
            get { return can_reorder_metal; }
            set
            {
                can_reorder_metal = value;
                OnPropertyChanged("CanReorderMetal");
            }
        }
        public bool CanReorderPlastic
        {
            get { return can_reorder_plastic; }
            set
            {
                can_reorder_plastic = value;
                OnPropertyChanged("CanReorderPlastic");
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
        public System.Windows.Media.Brush StatusColor_Metal
        {
            get { return statusColor_Metal; }
            set
            {
                statusColor_Metal = value;
                OnPropertyChanged("StatusColor_Metal");
            }
        }
        public System.Windows.Media.Brush StatusColor_Plastic
        {
            get { return statusColor_Plastic; }
            set
            {
                statusColor_Plastic = value;
                OnPropertyChanged("StatusColor_Plastic");
            }
        }
        public BaseTwoPartKit(string name, string kitnumber) //string name, int allowed_steralizaitons, int warning_use
        {
            Name = name;
            StatusColor_Metal = statusColor_Metal;
            StatusColor_Plastic = statusColor_Plastic;
            KitNumber = $"Kit #: {kitnumber}";
            KitDirectoryPath = Path.Combine(file_path, name, $"Kit {kitnumber}");
            UseFileLocation = Path.Combine(KitDirectoryPath, "Uses.txt");

            warning_uses_metal = 80;
            warning_uses_plastic = 80;
            total_uses_metal = 100;
            total_uses_plastic = 100;
            CanReorderMetal = false;
            CanReorderPlastic = false;
            if (name == "Tandem and Ring")
            {
                total_uses_metal = 10;
                total_uses_plastic = 10;
                warning_uses_metal = 7;
                warning_uses_plastic = 7;
            }
            build_read_use_file();
        }

        public void build_read_use_file()
        {
            if (File.Exists(UseFileLocation))
            {
                string[] lines = File.ReadAllLines(UseFileLocation);
                CurrentUseMetal = Convert.ToInt32(lines[0].Split("Current Use_Metal:")[1]);
                CurrentUsePlastic = Convert.ToInt32(lines[1].Split("Current Use_Plastic:")[1]);
                total_uses_metal = Convert.ToInt32(lines[2].Split("Total Uses_Metal:")[1]);
                total_uses_plastic = Convert.ToInt32(lines[3].Split("Total Uses_Plastic:")[1]);
                warning_uses_metal = Convert.ToInt32(lines[4].Split("Warning Uses_Metal:")[1]);
                warning_uses_plastic = Convert.ToInt32(lines[5].Split("Warning Uses_Plastic:")[1]);
                Present = lines[6].Split("updated:")[1];
            }
            else
            {
                CurrentUseMetal = 0;
                CurrentUsePlastic = 0;
                string[] info = { $"Current Use_Metal:{0}", $"Current Use_Plastic:{0}", $"Total Uses_Metal:{total_uses_metal}",
                    $"Total Uses_Plastic:{total_uses_plastic}", $"Warning Uses_Metal:{warning_uses_metal}", $"Warning Uses_Plastic:{warning_uses_plastic}",
                    $"Last updated:{Present}" };
                if (!Directory.Exists(KitDirectoryPath))
                {
                    Directory.CreateDirectory(KitDirectoryPath);
                }
                File.WriteAllLines(UseFileLocation, info);
            }
            check_status();
            UsesLeftMetal = total_uses_metal - CurrentUseMetal;
            UsesLeftPlastic = total_uses_plastic - CurrentUsePlastic;
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
            string[] info = { $"Current Use_Metal:{CurrentUseMetal}", $"Current Use_Plastic:{CurrentUsePlastic}", $"Total Uses_Metal:{total_uses_metal}",
                    $"Total Uses_Plastic:{total_uses_plastic}", $"Warning Uses_Metal:{warning_uses_metal}", $"Warning Uses_Plastic:{warning_uses_plastic}",
                    $"Last updated:{Present}" };
            if (!Directory.Exists(KitDirectoryPath))
            {
                Directory.CreateDirectory(KitDirectoryPath);
            }
            File.WriteAllLines(UseFileLocation, info);
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

        public void update_useage()
        {
            UsesLeftMetal = total_uses_metal - CurrentUseMetal;
            UsesLeftPlastic = total_uses_plastic - CurrentUsePlastic;
        }
        public void add_use(object sender, RoutedEventArgs e)
        {
            DateTime moment = DateTime.Now;
            Present = moment.ToLongDateString() + " " + moment.ToLongTimeString();
            CurrentUseMetal += 1;
            CurrentUsePlastic += 1;
            update_useage();
            update_file();
            check_status();
        }

        public void remove_use(object sender, RoutedEventArgs e)
        {
            CurrentUseMetal -= 1;
            CurrentUsePlastic -= 1;
            update_useage();
            update_file();
            check_status();
        }

        public void reorder_metal(object sender, RoutedEventArgs e)
        {
            CurrentUseMetal = 0;
            update_useage();
            update_file();
            create_reorder_file();
            check_status();
        }
        public void reorder_plastic(object sender, RoutedEventArgs e)
        {
            CurrentUsePlastic = 0;
            update_useage();
            update_file();
            create_reorder_file();
            check_status();
        }

        public void check_status()
        {
            if (CurrentUseMetal >= total_uses_metal)
            {
                StatusColor_Metal = System.Windows.Media.Brushes.Red;
                CanReorderMetal = true;
            }
            else if (CurrentUseMetal >= warning_uses_metal * 0.75)
            {
                StatusColor_Metal = System.Windows.Media.Brushes.Yellow;
                CanReorderMetal = false;
            }
            else
            {
                StatusColor_Metal = System.Windows.Media.Brushes.Green;
                CanReorderMetal = false;
            }
            if (CurrentUsePlastic >= total_uses_plastic)
            {
                StatusColor_Plastic = System.Windows.Media.Brushes.Red;
                CanReorderMetal = true;
            }
            else if (CurrentUsePlastic >= warning_uses_metal * 0.75)
            {
                StatusColor_Plastic = System.Windows.Media.Brushes.Yellow;
                CanReorderMetal = false;
            }
            else
            {
                StatusColor_Plastic = System.Windows.Media.Brushes.Green;
                CanReorderMetal = false;
            }
        }
    }
}
