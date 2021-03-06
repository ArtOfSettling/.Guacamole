﻿using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextTests
	{
		[SetUp]
		public void Setup()
		{
			_textEntryView = new Views.TextEntryView();
			_context = new ContextObject();
			_textEntryView.BindingContext = _context;
		}

		private Views.TextEntryView _textEntryView;
		private ContextObject _context;

		[Test]
		public void IsBindable()
		{
			_textEntryView.Text = "a";
			_context.Text = "b";
			Assert.That(_context.Text != _textEntryView.Text);
			_textEntryView.Bind(Views.TextEntryView.TextProperty, nameof(_context.Text));
			Assert.That(_context.Text == _textEntryView.Text);
			_context.Text = "c";
			Assert.That(_context.Text == _textEntryView.Text);
		}
	}
}