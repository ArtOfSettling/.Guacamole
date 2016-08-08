﻿using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public class Slider : ViewBase
    {
        [PublicAPI]
        public static readonly BindableProperty MinValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MinValue
		);

        [PublicAPI]
        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 1.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MaxValue
		);

        [PublicAPI]
        public static readonly BindableProperty ValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.Value
		);

        [PublicAPI]
        public double MinValue
		{
			get { return (double)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

        [PublicAPI]
        public double MaxValue
		{
			get { return (double)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

        [PublicAPI]
        public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public Slider()
		{
			// Set some nice defaults
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
			HoverBackgroundColor = UIColor.FromRGB(160, 160, 160);
			ActiveBackgroundColor = UIColor.FromRGB(64, 124, 191);
			OutlineColor = UIColor.FromRGB(125, 125, 125);
			HorizontalLayout = LayoutOptions.Fill;
			Padding = 5;
			CornerRadius = 8;
		}
	}
}