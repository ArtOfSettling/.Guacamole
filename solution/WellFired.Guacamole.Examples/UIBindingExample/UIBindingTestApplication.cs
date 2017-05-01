﻿using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.UIBindingExample
{
	[UsedImplicitly]
	public class UIBindingTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/UIBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<UIBindingTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "UIBindingTest");
		}
	}
}