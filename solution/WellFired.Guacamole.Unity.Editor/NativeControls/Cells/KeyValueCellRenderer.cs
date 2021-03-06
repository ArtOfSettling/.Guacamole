﻿using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;

[assembly: CustomRenderer(typeof(KeyValueCell), typeof(KeyValueCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
	public class KeyValueCellRenderer : BaseCellRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var keyValueCell = Control as KeyValueCell;
				// ReSharper disable once PossibleNullReferenceException
				return (Style.CalcSize(new GUIContent(keyValueCell.KeyText)) + keyValueCell.ValueWidth * Vector2.right).ToUISize();
			}
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var keyValueCell = (KeyValueCell) Control;
			var keyRect = new Rect(UnityRect.position, UnityRect.size - Vector2.right * keyValueCell.ValueWidth);
			var valueRect = new Rect(
				UnityRect.position + Vector2.right * keyRect.size.x,
				UnityRect.size - Vector2.right * keyRect.size.x
			);

			EditorGUI.LabelField(keyRect, keyValueCell.KeyText, Style);
			EditorGUI.LabelField(valueRect, keyValueCell.ValueText, Style);
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();

			Style.alignment = TextAnchor.MiddleLeft;

			var keyValueCell = (KeyValueCell) Control;
			Style.focused.textColor = keyValueCell.TextColor.ToUnityColor();
			Style.active.textColor = keyValueCell.TextColor.ToUnityColor();
			Style.hover.textColor = keyValueCell.TextColor.ToUnityColor();
			Style.normal.textColor = keyValueCell.TextColor.ToUnityColor();
		}

		public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnViewPropertyChanged(sender, e);

			var keyValueCell = (KeyValueCell) Control;
			if (e.PropertyName == KeyValueCell.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = keyValueCell.TextColor.ToUnityColor();
				Style.active.textColor = keyValueCell.TextColor.ToUnityColor();
				Style.hover.textColor = keyValueCell.TextColor.ToUnityColor();
				Style.normal.textColor = keyValueCell.TextColor.ToUnityColor();
			}
		}
	}
}