﻿using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryTextColorTests
	{
		[SetUp]
		public void Setup()
		{
			_numberEntryView = new Views.NumberEntryView();
			_numberEntryContext = new NumberEntryContextObject();
			_numberEntryView.BindingContext = _numberEntryContext;
		}

		private Views.NumberEntryView _numberEntryView;
		private NumberEntryContextObject _numberEntryContext;

		[Test]
		public void IsBindable()
		{
			_numberEntryView.TextColor = UIColor.Aquamarine;
			_numberEntryContext.TextColor = UIColor.Beige;
			Assert.That(_numberEntryContext.TextColor != _numberEntryView.TextColor);
			_numberEntryView.Bind(Views.NumberEntryView.TextColorProperty, nameof(_numberEntryContext.TextColor));
			Assert.That(_numberEntryContext.TextColor == _numberEntryView.TextColor);
			_numberEntryContext.TextColor = UIColor.Blue;
			Assert.That(_numberEntryContext.TextColor == _numberEntryView.TextColor);
		}
	}
}