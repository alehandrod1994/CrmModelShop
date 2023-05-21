using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmShopModel.UI
{
    public abstract class Checker
    {
        public static List<Control> CheckControlsOnNull(Control.ControlCollection controls)
        {
            var failedControls = new List<Control>();

            foreach (Control control in controls)
            {
                if (control is TextBox || control is NumericUpDown)
                {
                    if (string.IsNullOrWhiteSpace(control.Text))
                    {
                        failedControls.Add(control);                       
                    }
                }
            }
         
            return failedControls;
        }      
    }
}
