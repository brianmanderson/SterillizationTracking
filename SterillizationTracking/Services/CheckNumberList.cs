using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterillizationTracking.Services
{
    class CheckNumberList
    {
        public string file_path = @"\\ro-ariaimg-v\va_data$\HDR\Kit_Status";
        public List<string> out_list;
        public List<string> return_list(string kit_name)
        {
            out_list = new List<String> { "Select a number"};
            string kit_directory = Path.Combine(file_path, kit_name);
            if (!Directory.Exists(kit_directory))
            {
                Directory.CreateDirectory(kit_directory);
            }
            string[] kit_inventory_list = Directory.GetDirectories(kit_directory);
            for (int i = 1; i < 999; i ++)
            {
                string new_directory = Path.Combine(kit_directory, $"Kit {i}");
                if (!kit_inventory_list.Contains(new_directory))
                {
                    out_list.Add($"{i}");
                    return out_list;
                }
            }
            return out_list;
        }

    }
}
