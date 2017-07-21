﻿using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.TwoWayBindingExample
{
	[UsedImplicitly]
	public class TwoWayBindingTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/TwoWayBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<TwoWayBindingTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "TwoWayBindingTest Test");
		}
	}
}