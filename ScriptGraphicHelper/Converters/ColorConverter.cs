using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace ScriptGraphicHelper.Converters
{
    public class Color2HexConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //// 设计器模式下返回默认值
            //if (Design.IsDesignMode)
            //    return null;

            //if (value is null)
            //{
            //    return null;
            //}
            //var color = (Color)value;
            //return string.Format("#{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));

            // 设计器模式下返回默认值
            if (Design.IsDesignMode)
                return "#FF0000"; // 设计器模式下返回红色

            if (value is null or not Color)
                return null;

            var color = (Color)value;
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //var str = (string)value;
            //if (str.IndexOf('#') == -1)
            //{
            //    str = "#" + str;
            //}
            //return Color.Parse(str.PadRight(7, '0'));

            if (Design.IsDesignMode)
                return Colors.Red;

            if (value is string str)
            {
                try
                {
                    var input = str.Trim();
                    if (!input.StartsWith("#"))
                        input = "#" + input;

                    return Color.Parse(input);
                }
                catch
                {
                    return Colors.Black;
                }
            }

            return null;
        }
    }

    // 使用NewColorConverter.cs来处理这个类，不知道为什么，放一起会导致页面编译失败
    public class Color2BrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 设计器模式下返回默认值
            if (Design.IsDesignMode)
                return null;

            if (value is null)
            {
                return null;
            }

            if (value is Color color)
                return new SolidColorBrush(color);

            return null;
            //var color = (Color)value;
            //if (color.ToString() == "" )
            //{
            //    return null;
            //}

            //return Brush.Parse(string.Format("#{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2")));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 设计器模式下返回默认值
            if (Design.IsDesignMode)
                return null;

            if (value is SolidColorBrush brush)
                return brush.Color;

            return null;

            //if (value is null)
            //{
            //    return null;
            //}
            //var brush = (Brush)value;
            //return Color.Parse(brush.ToString());
        }
    }
}
