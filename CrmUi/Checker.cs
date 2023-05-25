using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmShopModel.UI
{
    /// <summary>
    /// Проверка введённых данных на валидность.
    /// </summary>
    public abstract class Checker
    {
        /// <summary>
        /// Проверяет контролы на null.
        /// </summary>
        /// <param name="controls"> Контролы. </param>
        /// <returns> Контролы, не прошедшие проверку. </returns>
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
