using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SterillizationTracking.StackPanelClasses
{
    class AddTopRow : StackPanel
    {
        public string[] label_names = { "Kit Name", "Number of Uses", "Number of Sterilizations", "Add Use", "Add Steralization" };
        public void build()
        {
            Orientation = Orientation.Horizontal;
            foreach (string label in label_names)
            {
                Label kitLabel = new Label();
                kitLabel.Content = label;
                Children.Add(kitLabel);
            }
        }
    }
}
