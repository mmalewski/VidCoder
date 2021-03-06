﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VidCoder.Services;
using VidCoder.ViewModel;

namespace VidCoder.Controls
{
    public class PresetComboBox : ComboBox
    {
	    private PresetTreeView treeView;

		protected override void OnMouseWheel(MouseWheelEventArgs e)
	    {
		    // don't call the method of the base class
	    }

	    public override void OnApplyTemplate()
	    {
		    base.OnApplyTemplate();

		    var treeViewContainer = (PresetTreeViewContainer)this.GetTemplateChild("treeViewContainer");
		    this.treeView = treeViewContainer.PresetTreeView;
		    this.treeView.OnHierarchyMouseUp += this.OnTreeViewHierarchyMouseUp;
	    }

		public static bool PresetComboOpen { get; set; }

	    protected override void OnDropDownOpened(EventArgs e)
	    {
		    base.OnDropDownOpened(e);
		    PresetComboOpen = true;
	    }

	    protected override void OnDropDownClosed(EventArgs e)
	    {
		    base.OnDropDownClosed(e);
		    this.SelectedItem = this.treeView.SelectedItem;
		    PresetComboOpen = false;
	    }

		/// <summary>
		/// Handles clicks on any item in the tree view
		/// </summary>
		private void OnTreeViewHierarchyMouseUp(object sender, MouseEventArgs e)
	    {
		    // This line isn't obligatory because it is executed in the OnDropDownClosed method, but be it so
		    //this.SelectedItem = this.treeView.SelectedItem;

		    this.IsDropDownOpen = false;
	    }

		/// <summary>
		/// Selected item of the TreeView
		/// </summary>
		public new object SelectedItem
	    {
		    get { return (object)GetValue(SelectedItemProperty); }
		    set { SetValue(SelectedItemProperty, value); }
	    }

	    public new static readonly DependencyProperty SelectedItemProperty =
		    DependencyProperty.Register("SelectedItem", typeof(object), typeof(PresetComboBox), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));

	    private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	    {
		    ((PresetComboBox)sender).UpdateSelectedItem();
	    }

		private void UpdateSelectedItem()
	    {
	    }
	}
}
