using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterillizationTracking.Kit_Classes
{
    class Cylinder : BaseOnePartKit
    {
        new public int total_uses = 5;
        new public int allowed_steralizations = 3;
        new public int warning_use = 3;
    }
}
