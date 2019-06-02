using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GoldenAnvil.Utility;
using GoldenAnvil.Utility.Windows;

namespace CrystalEditor.Controls.EnumEditor
{
    public sealed class EnumEditor : Control
    {
	    public static readonly DependencyProperty SelectedValueProperty = DependencyPropertyUtility<EnumEditor>.Register(x => x.SelectedValue, OnSelectedValueChanged, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);

	    public object SelectedValue
	    {
		    get => GetValue(SelectedValueProperty);
		    set => SetValue(SelectedValueProperty, value);
	    }

	    private static readonly DependencyPropertyKey s_availableValuesPropertyKey = DependencyPropertyUtility<EnumEditor>.RegisterReadOnly(x => x.AvailableValues);
	    public static readonly DependencyProperty AvailableValuesProperty = s_availableValuesPropertyKey.DependencyProperty;

	    public IReadOnlyList<object> AvailableValues
	    {
		    get => (IReadOnlyList<object>) GetValue(AvailableValuesProperty);
		    private set => SetValue(s_availableValuesPropertyKey, value);
	    }

	    private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	    {
		    var oldEnumType = e.OldValue?.GetType();
		    var newEnumType = e.NewValue?.GetType();
		    if (oldEnumType == newEnumType)
			    return;

		    ((EnumEditor) d).AvailableValues = newEnumType is null
			    ? null
			    : Enum.GetValues(newEnumType)
				    .Cast<object>()
				    .OrderBy(x => EnumToDisplayStringConverter.Instance.Convert(x, newEnumType, OurResources.ResourceManager, CultureInfo.CurrentCulture))
				    .ToList();
	    }
    }
}
