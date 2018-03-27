using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GestionFilms.Client.Converters
{
    class ReferenceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;

            if (value != null && value != DependencyProperty.UnsetValue)
            {
                visibility = Visibility.Visible;
            }

            if (parameter is string parameterstring && parameterstring == "!")
            {
                visibility = (visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed);
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
