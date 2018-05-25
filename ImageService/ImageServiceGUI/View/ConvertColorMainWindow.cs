using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageServiceGUI.View
{
    public class ConvertColorMainWindow :IValueConverter 
    {
        public object Convert(Object value, Type target, object param, CultureInfo info)
        {
            if (target.Name != "Brush")
            {
                throw new Exception("Error - should be a brush");
            }
            bool isConnect = (bool)value;
            if (isConnect)
            {
                return "Transparent";
            }
            else
            {
                return "Gray";
            }
        }
           public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}
