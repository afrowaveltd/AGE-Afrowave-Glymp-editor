using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.Converters;

public sealed class BoolToBrushConverter : IValueConverter
{
   public static readonly BoolToBrushConverter Instance = new();

   public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
       => (value is true) ? Brushes.White : Brushes.Black;

   public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
       => throw new NotSupportedException();
}