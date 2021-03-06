﻿using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryNumberTests
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
			_numberEntryView.Number = 0.0f;
			_numberEntryContext.Number = 1.0f;
			Assert.That(Math.Abs(_numberEntryContext.Number - _numberEntryView.Number) > 0.001f);
			_numberEntryView.Bind(Views.NumberEntryView.NumberProperty, nameof(_numberEntryContext.Number));
			Assert.That(Math.Abs(_numberEntryContext.Number - _numberEntryView.Number) < 0.001f);
			_numberEntryContext.Number = 2.0f;
			Assert.That(Math.Abs(_numberEntryContext.Number - _numberEntryView.Number) < 0.001f);
		}
	}
}