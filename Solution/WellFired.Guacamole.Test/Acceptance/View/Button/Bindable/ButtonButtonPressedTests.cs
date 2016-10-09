﻿using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	[TestFixture]
	public class ButtonButtonPressedTests
	{
		[SetUp]
		public void OneTimeSetup()
		{
			_buttonView = new Guacamole.View.Button();
			_labelContext = new ButtonContextObject();
			_buttonView.BindingContext = _labelContext;
		}

		private Guacamole.View.Button _buttonView;
		private ButtonContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			var buttonPressed1 = new Command();
			var buttonPressed2 = new Command();
			var buttonPressed3 = new Command();
			_buttonView.ButtonPressedCommand = buttonPressed1;
			_labelContext.ButtonPressedCommand = buttonPressed2;
			Assert.That(_labelContext.ButtonPressedCommand != _buttonView.ButtonPressedCommand);
			_buttonView.Bind(Guacamole.View.Button.ButtonPressedCommandProperty, nameof(_labelContext.ButtonPressedCommand));
			Assert.That(_labelContext.ButtonPressedCommand == _buttonView.ButtonPressedCommand);
			_labelContext.ButtonPressedCommand = buttonPressed3;
			Assert.That(_labelContext.ButtonPressedCommand == _buttonView.ButtonPressedCommand);
		}
	}
}