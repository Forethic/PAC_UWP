using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CharacterMap.Helpers.Converters
{
    public class ZoomBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null != value)
            {
                var count = int.Parse(value.ToString());
                if (count > 0)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 0, 144, 188));
                }
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}