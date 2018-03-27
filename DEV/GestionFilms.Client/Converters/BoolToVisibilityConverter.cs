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
    class BoolToVisibilityConverter : IValueConverter //une interface utilisée pour implémenter des classes
    {
        /// <summary>
        ///     Méthode de conversion de la source vers la cible
        /// </summary>
        /// <param name="value"> valeur de la propriété source</param>
        /// <param name="targetType">type de la propriété cible</param>
        /// <param name="parameter">paramètre du binding</param>
        /// <param name="culture">culture utilisée dans le binding</param>
        /// <returns>valeur convertie</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;

            if (value is bool boolValue && boolValue == true)
                visibility = Visibility.Visible;

            if (parameter is string parameterstring && parameterstring == "!")
                visibility = (visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed);

            return visibility;
        }

        /// <summary>
        ///     Méthode de conversion de la cible vers la source
        /// </summary>
        /// <param name="value"> valeur de la propriété cible</param>
        /// <param name="targetType">type de la propriété source</param>
        /// <param name="parameter">paramètre du binding</param>
        /// <param name="culture">culture utilisée dans le binding</param>
        /// <returns>valeur originale</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;

            return visibility;
        }
    }
}
