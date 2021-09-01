using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterillizationTracking.Kit_Classes
{
    class BaseOnePartKit
    {
        public int total_uses;
        public int current_use = 0;
        public int warning_use;
        public int allowed_steralizations;
        public int current_steralization = 0;
        public string status_color;
        public string name;
        public void add_use(int uses)
        {
            current_use += uses;
        }
        public void add_sterilization()
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
