using System.Windows;
using System.Windows.Controls;

namespace HabitTracker.UI.Controls;

public class PlaceholderTextBox : TextBox
{
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox),
            new PropertyMetadata(string.Empty));

    private static readonly DependencyPropertyKey IsEmptyPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(IsEmpty), typeof(bool), typeof(PlaceholderTextBox),
            new PropertyMetadata(true));

    private static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;

    static PlaceholderTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox),
            new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
    }

    public bool IsEmpty
    {
        get => (bool)GetValue(IsEmptyProperty);
        set => SetValue(IsEmptyPropertyKey, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        IsEmpty = string.IsNullOrEmpty(Text);
        base.OnTextChanged(e);
    }
}