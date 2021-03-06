﻿using System;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Pages.MasterDetailPage
{
	public class MasterPageItem : ObservableBase, IDefaultCellContext
	{
		public MasterPageItem(string title, Type targetType, bool isSelected)
		{
			CellLabelText = title;
			TargetType = targetType;
			IsSelected = isSelected;
		}
		
		public MasterPageItem(string title, Type targetType)
		{
			CellLabelText = title;
			TargetType = targetType;
		}

		private string _cellLabelText;
		private bool _isSelected;

		public string CellLabelText
		{
			get => _cellLabelText;
			set => SetProperty(ref _cellLabelText, value);
		}

		public bool IsSelected
		{
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		public Type TargetType
		{
			get; 
			set;
		}
	}
}