using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ScriptGraphicHelper.Converters
{
    public enum AnchorMode
    {
        None = 0,
        Left = 1,
        Center = 2,
        Right = 3,
    }
    public class AnchorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 设计器模式下返回默认值
            if (Design.IsDesignMode)
                return 1; // 返回 Left 对应的索引

            if (value is null)
            {
                return null;
            }
            var anchor = (AnchorMode)value;
            return anchor switch
            {
                AnchorMode.None => 0,
                AnchorMode.Left => 1,
                AnchorMode.Center => 2,
                AnchorMode.Right => 3,
                _ => 0
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 设计器模式下返回默认值
            if (Design.IsDesignMode)
                return AnchorMode.Left;

            if (value is null)
            {
                return null;
            }
            var anchor = (int)value;
            return anchor switch
            {
                0 => AnchorMode.None,
                1 => AnchorMode.Left,
                2 => AnchorMode.Center,
                3 => AnchorMode.Right,
                _ => AnchorMode.None,
            };
        }
    }
}
