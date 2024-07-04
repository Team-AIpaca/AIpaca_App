using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AIpaca_App.Converters
{
    public class SuccessToColorConverter : IValueConverter
    {
        //LogPage의 성공여부에 따른 색상을 반환하는 컨버터
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string successValue)
            {
                return successValue switch
                {
                    "Success" => Color.FromArgb("#bcffbe"),
                    "Failed" => Color.FromArgb("#ffd3a7"),
                    _ => GetDefaultColor()     // 그 외의 경우 기본 색상 설정
                };
            }
            return GetDefaultColor();
        }
        
        private Color GetDefaultColor()
        {
            // 다크 모드에 따른 색상 반환
            return Application.Current?.RequestedTheme == AppTheme.Dark ? Color.FromArgb("#2c2c2c") : Color.FromArgb("#929292");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one-way binding is supported.");
        }
    }
}
