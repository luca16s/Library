namespace Desktop.Components
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class ButtonComponent : UserControl
    {
        public ButtonComponent()
        {
            InitializeComponent();
        }

        public object Image
        {
            get
            {
                string data = GetValue(ImageProperty).ToString() ?? string.Empty;

                return string.IsNullOrWhiteSpace(data)
                    ? DependencyProperty.UnsetValue
                    : data;
            }
            set => SetValue(ImageProperty, value);
        }

        public object ImageColor
        {
            get
            {
                string data = GetValue(ImageColorProperty).ToString() ?? string.Empty;

                return string.IsNullOrWhiteSpace(data)
                    ? DependencyProperty.UnsetValue
                    : data;
            }
            set => SetValue(ImageColorProperty, value);
        }

        private static readonly DependencyProperty ImageColorProperty =
           DependencyProperty.Register(
               nameof(ImageColor),
               typeof(object),
               typeof(ButtonComponent));

        private static readonly DependencyProperty ImageProperty =
           DependencyProperty.Register(
               nameof(Image),
               typeof(object),
               typeof(ButtonComponent));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(Command);
            set => SetValue(Command, value);
        }

        private static readonly DependencyProperty Command =
            DependencyProperty.Register(
                nameof(ButtonCommand),
                typeof(ICommand),
                typeof(ButtonComponent));

        public object ButtonCommandParameters
        {
            get => GetValue(CommandParameters);
            set => SetValue(CommandParameters, value);
        }

        private static readonly DependencyProperty CommandParameters =
            DependencyProperty.Register(
                nameof(ButtonCommandParameters),
                typeof(object),
                typeof(ButtonComponent));

        public event RoutedEventHandler Click
        {
            add { Button.Click += value; }
            remove { Button.Click += value; }
        }
    }
}
