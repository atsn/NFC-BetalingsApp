using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NFC_BetalingsApp
{
    class NavigationHelper
    {
        private static Frame _frame;

        public static void navigate(Type page)
        {
            try
            {
                _frame = (Window.Current.Content as Frame);
                _frame?.Navigate(page); // Hvis _frame IKKE er null
            }
            catch (Exception e)
            {
                var messageBox = new MessageDialog(e + e.Message);
                messageBox.ShowAsync();
            }
        }
    }
}
