using System;
using System.Collections.Generic;
using System.Windows.Input;
using Smartroom.Models;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class SidebarItem : ContentView
    {
        public SidebarItem()
        {
            InitializeComponent();
            this.Content.BindingContext = this;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SidebarItem));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(nameof(SelectedIcon), typeof(ImageSource), typeof(SidebarItem));
        public ImageSource SelectedIcon
        {
            get => (ImageSource)GetValue(SelectedIconProperty);
            set => SetValue(SelectedIconProperty, value);
        }

        public static readonly BindableProperty DeSelectedIconProperty = BindableProperty.Create(nameof(DeSelectedIcon), typeof(ImageSource), typeof(SidebarItem));
        public ImageSource DeSelectedIcon
        {
            get => (ImageSource)GetValue(DeSelectedIconProperty);
            set => SetValue(DeSelectedIconProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SidebarItem), false);
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SidebarItem));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SidebarItem));
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler<object> ItemClicked;

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Command?.Execute(CommandParameter);
            ItemClicked?.Invoke(this, new EventWithCommandParameterArg { CommandParameter=CommandParameter });
        }
    }
}