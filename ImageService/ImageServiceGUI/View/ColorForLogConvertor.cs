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
    class ColorForLogConvertor : IValueConverter
    {
        public object Convert(Object value, Type target, object param, CultureInfo info)
        {
            if (target.Name != "Brush")
            {
                throw new Exception("Error - should be a brush");
            }
            MessageTypeEnum msg = (MessageTypeEnum)value;
            if (msg == MessageTypeEnum.WARNING)
            {
                return "Yellow";
            } else if (msg == MessageTypeEnum.INFO)
            {
                return "Green";
            } else
            {
                return "Red";
            }
        }
        public object ConvertBack(object value, Type target, object param, CultureInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
