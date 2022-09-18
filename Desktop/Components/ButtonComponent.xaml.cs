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

        public object Color
        {
            get
            {
                string data = GetValue(ColorProperty).ToString() ?? string.Empty;

                return string.IsNullOrWhiteSpace(data)
                    ? DependencyProperty.UnsetValue
                    : data;
            }
            set => SetValue(ColorProperty, value);
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

        private static readonly DependencyProperty ColorProperty =
           DependencyProperty.Register(
               nameof(Color),
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
            get => (object)GetValue(CommandParameters);
            set => SetValue(CommandParameters, value);
        }

        private static readonly DependencyProperty CommandParameters =
            DependencyProperty.Register(
                nameof(ButtonCommandParameters),
                typeof(object),
                typeof(ButtonComponent));

        public event RoutedEventHandler Click
        {
            add { ButtonBase.Click += value; }
            remove { ButtonBase.Click += value; }
        }
    }
}
