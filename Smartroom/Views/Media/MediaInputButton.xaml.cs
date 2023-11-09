using System;
using System.Collections.Generic;
using System.Windows.Input;
using Smartroom.Models;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaInputButton : ContentView
    {
        public MediaInputButton()
        {
            InitializeComponent();
            this.Content.BindingContext = this;
        }

        public static readonly BindableProperty IconLabelTextProperty = BindableProperty.Create(nameof(IconLabelText), typeof(string), typeof(MediaInputButton));
        public string IconLabelText
        {
            get => (string)GetValue(IconLabelTextProperty);
            set => SetValue(IconLabelTextProperty, value);
        }

        public static readonly BindableProperty SelectedIconImageProperty = BindableProperty.Create(nameof(SelectedIconImage), typeof(string), typeof(MediaInputButton));
        public string SelectedIconImage
        {
            get => (string)GetValue(SelectedIconImageProperty);
            set => SetValue(SelectedIconImageProperty, value);
        }

        public static readonly BindableProperty DeSelectedIconImageProperty = BindableProperty.Create(nameof(DeSelectedIconImage), typeof(string), typeof(MediaInputButton));
        public string DeSelectedIconImage
        {
            get => (string)GetValue(DeSelectedIconImageProperty);
            set => SetValue(DeSelectedIconImageProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MediaInputButton), false);
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MediaInputButton));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MediaInputButton));
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler<object> ItemClicked;

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Command?.Execute(CommandParameter);
            ItemClicked?.Invoke(this, new EventWithCommandParameterArg { CommandParameter = CommandParameter });
        }
    }
}