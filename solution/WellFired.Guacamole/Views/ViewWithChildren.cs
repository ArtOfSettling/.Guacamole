﻿using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ViewWithChildren : View, IHasChildren
    {
        protected ViewWithChildren()
        {
            Children = new List<ILayoutable>();
        }
        
        public override void Render(UIRect parentRect)
        {
            base.Render(parentRect);
            
            var resetToOriginContentRenderRect = FinalContentRenderRect;
            if (NativeRenderer.PushMaskStack(FinalRenderRect))
            {
                resetToOriginContentRenderRect.X = 0;
                resetToOriginContentRenderRect.Y = 0;
            }

            foreach (var child in Children)
                (child as View)?.Render(resetToOriginContentRenderRect);
            
            NativeRenderer.PopMaskStack();
        }

        public override void InvalidateRectRequest()
        {
            base.InvalidateRectRequest();

            foreach (var child in Children)
                (child as View)?.InvalidateRectRequest();
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == ChildrenProperty.PropertyName || e.PropertyName == BindingContextProperty.PropertyName)
                SetupChildBindingContext();
        }

        private void SetupChildBindingContext()
        {
            foreach (var child in Children)
            {
                var view = child as View;
                if (view != null)
                    view.BindingContext = BindingContext;
            }
        }

        public override void SetStyleDictionary(IStyleDictionary styleDictionary)
        {
            base.SetStyleDictionary(styleDictionary);

            foreach (var child in Children)
                (child as View)?.SetStyleDictionary(styleDictionary);
        }
    }
}