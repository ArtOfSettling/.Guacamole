﻿using JetBrains.Annotations;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Pages
{
    public class Page : View
    {
        [PublicAPI] public static readonly BindableProperty TitleProperty = BindableProperty.Create<Page, string>(
            "Page",
            BindingMode.TwoWay,
            page => page.Title
        );
        
        public string Title 
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public Page()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Fill;
        }
    }
}