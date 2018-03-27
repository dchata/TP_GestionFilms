using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MVVMLib.WPF
{
    public class SelectedItemsHelper
    {
        #region Dependencies Properties
        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value) => obj.SetValue(SelectedItemsProperty, value);

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(SelectedItemsHelper), new PropertyMetadata(null, OnSelectedItemsChanged));

        #endregion

        #region Methods
        private static void OnSelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is MultiSelector multiSelector && e.NewValue is IList selectedItems)
            {
                multiSelector.SelectionChanged -= MultiSelector_SelectionChanged;
                multiSelector.SelectionChanged += MultiSelector_SelectionChanged;
            }
            else if(sender is MultiSelector multi && (e.NewValue == null || e.NewValue == DependencyProperty.UnsetValue))
                multi.SelectionChanged -= MultiSelector_SelectionChanged;
        }

        private static void MultiSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is MultiSelector multiSelector && GetSelectedItems(multiSelector) is IList selectedItems)
                {
                    foreach (object item in multiSelector.SelectedItems.Cast<object>().Except(selectedItems.Cast<object>()).ToList())
                        selectedItems.Add(item);

                    foreach (object item in selectedItems.Cast<object>().Except(multiSelector.SelectedItems.Cast<object>()).ToList())
                        selectedItems.Remove(item);
                }
            }
            catch (System.Exception) { }
        }
        #endregion
    }
}
