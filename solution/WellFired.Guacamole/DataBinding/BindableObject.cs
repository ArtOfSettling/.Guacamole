using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.DataBinding.Converter;
using WellFired.Guacamole.DataBinding.Exceptions;

namespace WellFired.Guacamole.DataBinding
{
	public class BindableObject : INotifyPropertyChanged, IBindableObject
	{
		protected static readonly BindableProperty BindingContextProperty =
			BindableProperty.Create<BindableObject, INotifyPropertyChanged>(null, BindingMode.OneWay,
				bindableObject => bindableObject.BindingContext);

		/// <summary>
		/// Each bindable property of the bindable object has a bindable context where its value is stored
		/// and where value exchange with the data source happens.
		/// </summary>
		private readonly Dictionary<BindableProperty, BindableContext> _contexts =
			new Dictionary<BindableProperty, BindableContext>();

		/// <summary>
		/// List of the bindable contexts whose bindable property is bound to the source.
		/// </summary>
		private readonly List<BindableContext> _boundContexts = new List<BindableContext>();

		/// <summary>
		/// This dictionary is only used for performance reason to have faster access to our bindable context
		/// when the source informed us of a property change.
		/// </summary>
		private readonly Dictionary<string, BindableContext> _srcPropertyToContexts =
			new Dictionary<string, BindableContext>();

		private INotifyPropertyChanged _bindingContext;

		public INotifyPropertyChanged BindingContext
		{
			get => _bindingContext;
			set
			{
				// Here we check for equality so we can avoid recursion.
				if (Equals(_bindingContext, value))
					return;

				if (_bindingContext != null)
					_bindingContext.PropertyChanged -= OnSourcePropertyChanged;

				_bindingContext = value;
				foreach (var context in _boundContexts)
				{
					context.SourceObject = _bindingContext;
					SetValueFromSource(context);
				}

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(BindingContextProperty.PropertyName));

				_bindingContext.PropertyChanged += OnSourcePropertyChanged;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void Bind(BindableProperty bindableProperty, string sourceProperty, IValueConverter converter)
		{
			Bind(bindableProperty, sourceProperty, null, converter);
		}

		/// <summary>
		/// Bind a Property on an object to this Property.
		/// </summary>
		/// <param name="bindableProperty"></param>
		/// <param name="sourceProperty"></param>
		/// <param name="bindingMode">If this is not passed, we will default to using the binding monde on the passed property.</param>
		/// <param name="converter">An optional converter that will convert from dource type to dest type and vice versa</param>
		/// <exception cref="BindingExistsException">The Binding already exists.</exception>
		public void Bind(BindableProperty bindableProperty, string sourceProperty, BindingMode? bindingMode = null, IValueConverter converter = null)
		{
			var context = GetOrCreateBindableContext(bindableProperty);

			if (_boundContexts.Contains(context))
				throw new BindingExistsException(bindableProperty.PropertyName, sourceProperty, context.SourcePropertyName);

			_boundContexts.Add(context);

			context.SourceObject = BindingContext;
			context.SourcePropertyName = sourceProperty;

			// Override our InstancedBindingMode, but only if we have provided one to this method.
			// otherwise, we take the BindingMode from the BindableProperty
			if (bindingMode != null)
				context.InstancedBindingMode = bindingMode.Value;

			// Override our InstancedConverter, but only if we have provided one to this method.
			// otherwise, we take the Converter from the default context
			if (converter != null)
				context.InstancedConverter = converter;

			_srcPropertyToContexts[sourceProperty] = context;

			if (BindingContext != null)
				SetValueFromSource(context);
		}

		public object GetValue(BindableProperty bindableProperty)
		{
			return GetOrCreateBindableContext(bindableProperty).Value;
		}

		private void SetValueFromSource(BindableContext context)
		{
			if (!context.SetValueFromSource())
				return;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(context.BindableProperty.PropertyName));
		}

		public bool SetValue(BindableProperty bindableProperty, object value)
		{
			var context = GetOrCreateBindableContext(bindableProperty);

			if (context.InstancedBindingMode == BindingMode.ReadOnly)
				return false;

			if (!context.SetValueFromDest(value))
				return false;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(bindableProperty.PropertyName));
			return true;
		}

		private BindableContext GetOrCreateBindableContext(BindableProperty bindableProperty)
		{
			var bindablePropertyContext = GetContext(bindableProperty) ?? CreateAndAddContext(bindableProperty);
			return bindablePropertyContext;
		}

		private BindableContext GetContext(BindableProperty bindableProperty)
		{
			return _contexts.ContainsKey(bindableProperty) ? _contexts[bindableProperty] : null;
		}

		private BindableContext CreateAndAddContext(BindableProperty bindableProperty)
		{
			var bindablePropertyContext = new BindableContext(bindableProperty.DefaultValue) {
				BindableProperty = bindableProperty,
				InstancedBindingMode = bindableProperty.BindingMode,
				SourceObject = _bindingContext
			};

			_contexts[bindableProperty] = bindablePropertyContext;
			return bindablePropertyContext;
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_srcPropertyToContexts.TryGetValue(e.PropertyName, out var context);
			if (context == default(BindableContext))
				return;

			SetValueFromSource(context);
		}
	}
}