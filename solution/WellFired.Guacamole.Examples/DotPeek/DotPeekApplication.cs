using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.DotPeek
{
    [UsedImplicitly]
    public class DotPeekApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/DotPeek")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<DotPeekWindow>(
                uiRect: UIRect.With(50, 50, 600, 600),
                minSize: UISize.Of(600, 600),
                title: "DotPeek");
        }
    }
}