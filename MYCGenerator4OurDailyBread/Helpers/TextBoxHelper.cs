using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public static class TextBoxHelper
    {
        /// <summary>
        /// Because the binding of text of a TextBox will not update automatically when the user click on a button, let me write this code to force it to update it.
        /// </summary>
        public static void UpdateTextBinding()
        {
            var focusObj = FocusManager.GetFocusedElement();
            if (focusObj != null && focusObj is TextBox)
            {
                ((TextBox)focusObj).GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            }
        }
    }
}
