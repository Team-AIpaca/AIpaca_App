using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AIpaca_App.Converters
{
    public class SuccessToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string successValue)
            {
                return successValue switch
                {
                    "Success" => Color.FromArgb("#76db74"),
                    "Failed" => Color.FromArgb("#e8b079"),
                    _ => GetDefaultColor()     // 그 외의 경우 기본 색상 설정
                };
            }
            return GetDefaultColor();
        }

        private Color GetDefaultColor()
        {
            // 다크 모드에 따른 색상 반환
            return Application.Current?.RequestedTheme == AppTheme.Dark ? Color.FromArgb("#2c2c2c") : Colors.White;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one-way binding is supported.");
        }
    }
}
