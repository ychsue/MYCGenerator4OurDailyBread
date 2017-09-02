using MYCGenerator4OurDailyBread.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace MemorizeYC.Helpers
{
    public class BoolToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(Visibility) && value.GetType() == typeof(bool))
                return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// true: *
    /// false: Auto
    /// </summary>
    public class BoolToGridDefConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(bool) && targetType == typeof(GridLength))
            {
                if ((bool)value == true)
                    return new GridLength(1, GridUnitType.Star);
                else
                    return new GridLength(1, GridUnitType.Auto);
            }
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class Power2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double result;
            if (value.GetType() == typeof(double))
                result = Math.Pow(2, (double)value);
            else
                return DependencyProperty.UnsetValue;

            if (targetType == typeof(double))
                return result;
            else if (targetType == typeof(string))
                return result.ToString("0.000");
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double result;
            if (value.GetType() == typeof(double))
                result = Math.Log((double)value, 2);
            else
                return DependencyProperty.UnsetValue;

            if (targetType == typeof(double))
                return result;
            else if (targetType == typeof(string))
                return result.ToString("0.000");
            else
                return DependencyProperty.UnsetValue;
        }
    }

    public class Log2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double result;
            if (value.GetType() == typeof(double))
                result = Math.Log((double)value, 2);
            else
                return DependencyProperty.UnsetValue;

            if (targetType == typeof(double))
                return result;
            else if (targetType == typeof(string))
                return result.ToString("0.000");
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double result; //Pow(2,a)
            if (value.GetType() == typeof(double))
                result = Math.Pow(2,(double)value);
            else
                return DependencyProperty.UnsetValue;

            if (targetType == typeof(double))
                return result;
            else if (targetType == typeof(string))
                return result.ToString("0.000");
            else
                return DependencyProperty.UnsetValue;
        }
    }

    public class DoubleAlgebraConverter : IValueConverter
    {
        /// <summary>
        /// +-*/ of an input value
        /// </summary>
        /// <param name="value">its type should be double</param>
        /// <param name="targetType">It should be double, too</param>
        /// <param name="parameter">"string" like "+20", "-24", "*2", "/2"</param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string param = parameter as string;
            double newValue = double.NaN;
            if (param == null)
                return DependencyProperty.UnsetValue;
            try
            {
                if (param.Length < 2)
                    return DependencyProperty.UnsetValue;

                string op = param.Substring(0, 1);
                double number = System.Convert.ToDouble(param.Substring(1));
                if (op == "+")
                    newValue = (double)value + number;
                else if (op == "-")
                    newValue = (double)value - number;
                else if (op == "*")
                    newValue = (double)value * number;
                else if (op == "/")
                    newValue = (double)value / number;
                else
                    return DependencyProperty.UnsetValue;
            }
            catch (Exception exc)
            {
                return DependencyProperty.UnsetValue;
            }

            return (double.IsNaN(newValue) || newValue < 0) ? double.NaN : newValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(string) && value.GetType() == typeof(TimeSpan))
            {
                var input = (TimeSpan)value;
                return input.Hours.ToString("00") + " : " + input.Minutes.ToString("00") + " : " + input.Seconds.ToString("00")+"." + input.Milliseconds.ToString("000") ;
            }
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public class NotBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(bool) && targetType == typeof(bool))
            {
                return !(bool)value;
            }
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(bool) && targetType == typeof(bool))
            {
                return !(bool)value;
            }
            else
                return DependencyProperty.UnsetValue;
        }
    }

    public class ThicknessToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(Thickness) && targetType == typeof(string))
            {
                var margin = (Thickness)value;
                return GetStringFromThickness(margin);
            }
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(string))
            {
                Thickness margin = GetThicknessFromString((string)value);
                return margin;
            }
            else
                return LocalSettingsHelper.GetPairMargin();
        }

        public static Thickness GetThicknessFromString(string value, bool isDefault = false)
        {
            var split = value.Split(',');
            double[] num = new double[4];
            if (split.Length == 1 && double.TryParse(value, out num[0]))
                return new Thickness(num[0]);
            else if (split.Length == 4)
            {
                bool isAThickness = true;
                for (int i0 = 0; i0 < 4; i0++)
                {
                    if (double.TryParse(split[i0], out num[i0]) == false)
                    {
                        isAThickness = false;
                        break;
                    }
                }
                if (isAThickness)
                    return new Thickness(num[0], num[1], num[2], num[3]);
                else
                {
                    if (isDefault)
                        return LocalSettingsHelper.DefaultPairMargin;
                    return LocalSettingsHelper.GetPairMargin();
                }
            }
            else
            {
                if (isDefault)
                    return LocalSettingsHelper.DefaultPairMargin;
                return LocalSettingsHelper.GetPairMargin();
            }
        }

        public static string GetStringFromThickness(Thickness margin)
        {
            if (margin.Bottom == margin.Top && margin.Top == margin.Left && margin.Left == margin.Right)
                return margin.Bottom.ToString();
            else
                return margin.Left.ToString() + "," + margin.Top.ToString() + "," + margin.Right.ToString() + "," + margin.Bottom.ToString();
        }
    }

    public class TrueToHorizontalOrientation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && targetType == typeof(Orientation))
                return ((bool)value) ? Orientation.Horizontal : Orientation.Vertical;
            else
                return (LocalSettingsHelper.GetIsHorizontalPair()) ? Orientation.Horizontal : Orientation.Vertical;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Orientation && targetType == typeof(bool))
                return (Orientation)value == Orientation.Horizontal;
            else
                return LocalSettingsHelper.GetIsHorizontalPair();
        }
    }
}
