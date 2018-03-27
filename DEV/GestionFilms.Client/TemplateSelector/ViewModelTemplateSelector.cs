using GestionFilms.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoursWPF1.TemplateSelector
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template = base.SelectTemplate(item, container);

            if (item != null)
               template = App.Current.TryFindResource(item.GetType().Name + "Template") as DataTemplate;

            return template;
        }
    }
}
