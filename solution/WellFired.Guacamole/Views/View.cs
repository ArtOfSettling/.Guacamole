using System;using System.ComponentModel;using WellFired.Guacamole.Data;using JetBrains.Annotations;using WellFired.Guacamole.DataBinding;using WellFired.Guacamole.Event;using WellFired.Guacamole.Exceptions;using WellFired.Guacamole.Renderer;using WellFired.Guacamole.Styling;namespace WellFired.Guacamole.Views{	public partial class View : BindableObject, IView	{	    protected UIRect FinalRenderRect = default(UIRect);		protected UIRect FinalContentRenderRect = default(UIRect);	    private INativeRenderer _nativeRenderer;		private Style _style;		private UIRect _validRectRequest;		protected IStyleDictionary StyleDictionary;		private IView _content;		public IView Content		{			get => _content;			set			{				_content = value;				if (BindingContext != null && (_content as View)?.BindingContext == null)					((View)_content).BindingContext = BindingContext;			}		}	    public View()		{			RectRequest = UIRect.Min;			Id = Guid.NewGuid().ToString();						// Here we have an attempt to get the native renderer and ensure it's ready for use.			var nativeRender = NativeRenderer;			if(nativeRender == null)				PropertyChanged += OnBindablePropertyChanged;		}		public UIRect RectRequest 		{ 			get => _validRectRequest;			set => _validRectRequest = value;		}				public UIRect ContentRectRequest { get; set; }		public bool ValidRectRequest { get; set; }	    public string Id { get; set; }	    public INativeRenderer NativeRenderer		{			get			{				if (_nativeRenderer != default(INativeRenderer))					return _nativeRenderer;				INativeRenderer newNativeRenderer;				try				{					newNativeRenderer = NativeRendererHelper.CreateNativeRendererFor(GetType());					if (newNativeRenderer == null)						throw new Exception();				}				catch (Exception)				{					return null;				}				if (_nativeRenderer != null && newNativeRenderer != _nativeRenderer)				{					PropertyChanged -= OnBindablePropertyChanged;					PropertyChanged -= _nativeRenderer.OnViewPropertyChanged;				}				_nativeRenderer = newNativeRenderer;			    if (_nativeRenderer == null)			        return _nativeRenderer;			    PropertyChanged += _nativeRenderer.OnViewPropertyChanged;			    PropertyChanged += OnBindablePropertyChanged;			    _nativeRenderer.Control = this;			    _nativeRenderer.Create();			    return _nativeRenderer;			}		}		public virtual void Render(UIRect parentRect)		{			try			{				FinalRenderRect.X = parentRect.X + X;				FinalRenderRect.Y = parentRect.Y + Y;				FinalRenderRect.Width = RectRequest.Width;				FinalRenderRect.Height = RectRequest.Height;					FinalContentRenderRect = FinalRenderRect;				FinalContentRenderRect.X += ContentRectRequest.X;				FinalContentRenderRect.Y += ContentRectRequest.Y;				FinalContentRenderRect.Width = ContentRectRequest.Width;				FinalContentRenderRect.Height = ContentRectRequest.Height;					NativeRenderer.Render(FinalContentRenderRect);				(Content as View)?.Render(FinalContentRenderRect);			}			catch (Exception e)			{				if (e is GuacamoleUserFacingException) throw;								var renderingException = new ViewRenderingException(GetType(), Id, e.Message, e.StackTrace);				throw renderingException;			}		}		[PublicAPI]		public virtual void InvalidateRectRequest()		{			ValidRectRequest = false;		    (Content as View)?.InvalidateRectRequest();		}	    protected virtual void OnBindablePropertyChanged(object sender, PropertyChangedEventArgs e)		{			if (e.PropertyName == BindingContextProperty.PropertyName)			{				if (Content is View view)				{					if(view.BindingContext == null)						view.BindingContext = BindingContext;				}			}			if(Style != null)				StyleHelper.ProcessTriggers(Style.Triggers, this, e.PropertyName);						(Content as View)?.OnBindablePropertyChanged(sender, e);		}		private void UpdateNewStyle(IStyle style)		{			foreach (var setter in style.Setters)				SetValue(setter.Property, setter.Value);		}		public void RaiseEvent(IEvent raisedEvent)		{			var clickEvent = raisedEvent as ClickEvent;			if (clickEvent != null)			{				var clickable = this as IClickable;				clickable?.Click(clickEvent.Button);			}			var typeEvent = raisedEvent as TypeEvent;			if (typeEvent == null)				return;			var typeable = this as ITypeable;			typeable?.Type(typeEvent.Key);		}		public void FocusControl()		{			NativeRenderer.FocusControl();		}		public virtual void SetStyleDictionary(IStyleDictionary styleDictionary)		{			StyleDictionary = styleDictionary;			Content?.SetStyleDictionary(StyleDictionary);			var style = StyleDictionary?.Get(GetType());			if (style == default(Style)) 				return;						Style = style;		}		public virtual void ResetBindingContext(INotifyPropertyChanged newBindingContext)		{			BindingContext = newBindingContext;			((View) Content)?.ResetBindingContext(newBindingContext);		}	}}