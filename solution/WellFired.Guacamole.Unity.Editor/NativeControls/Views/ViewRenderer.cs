﻿using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(View), typeof(ViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class ViewRenderer : BaseRenderer
    {
        private GUIStyle Style { get; set; }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            if (Style == null)
                Style = new GUIStyle();

            Style.focused.background = BackgroundTexture;
            Style.active.background = BackgroundTexture;
            Style.hover.background = BackgroundTexture;
            Style.normal.background = BackgroundTexture;

            var offset = (float) Control.CornerRadius;
            var smallest =
                (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
            smallest = Mathf.Max(smallest, 2);
            Style.border = new RectOffset(smallest, smallest, smallest, smallest);
            Style.padding = new RectOffset(smallest, smallest, 0, 0);

            GUI.Box(renderRect.ToUnityRect(), "", Style);
        }
    }
}