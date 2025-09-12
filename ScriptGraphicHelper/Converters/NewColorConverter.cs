using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace ScriptGraphicHelper.Converters
{
    public class NewColor2BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Design.IsDesignMode)
                return Brushes.Red;

            if (value is Color color)
                return new SolidColorBrush(color);

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Design.IsDesignMode)
                return Colors.Red;

            if (value is SolidColorBrush brush)
                return brush.Color;

            return Colors.Transparent;
        }
    }
}