﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Bindable.Basic
{
	[TestClass]
	public class StringBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty TextProperty = BindableProperty.Create<BindableTestObject, string>(
					defaultValue: string.Empty,
					bindingMode: BindingMode.TwoWay,
					getter: testObject => testObject.Value
				);

			public string Value
			{
				get { return (string)GetValue(TextProperty); }
				set { SetValue(TextProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private string _currentValue = string.Empty;

			public string Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value, nameof(Value)); }
			}
		}

		[TestMethod]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value));
			bindingContext.Value = "NewText";

			Assert.AreEqual(source.Value, bindingContext.Value);
		}

		[TestMethod]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value));
			bindingContext.Value = "NewText";

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = "NewText2";

			Assert.AreNotEqual(bindingContext.Value, source.Value);
		}

		[TestMethod]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = "NewText";

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = "NewText2";

			Assert.AreEqual(bindingContext.Value, source.Value);
		}
	}
}