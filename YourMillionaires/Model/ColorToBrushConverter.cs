using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using YourMillionaires.ViewModel;

namespace YourMillionaires.Model
{
    public class ColorToBrushConverter : IValueConverter
    {
        public bool Check { get; set; }

        public ColorToBrushConverter()
        {
            Messenger.Default.Register<AutoReplyMessage>(this, this.GetMvvmMessage);
        }

        void GetMvvmMessage(AutoReplyMessage message)
        {
            this.Check = message.Message;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = new Color();

            if (this.Check)
            {
                var test = (Answer)value;

                if (test.IsOk)
                {
                    color = Colors.Green;
                }
                else
                {
                    color = Colors.Red;
                }
            }
            else
            {
                color = Colors.PowderBlue;
            }            

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Colors.Red;
        }
    }
}
