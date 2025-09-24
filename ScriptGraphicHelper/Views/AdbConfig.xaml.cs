using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using ScriptGraphicHelper.Models;
using System;
using System.IO;

namespace ScriptGraphicHelper.Views
{
    public class AdbConfig : Window
    {
        private static string LastAddress;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public AdbConfig()
        {
            InitializeComponent();
        }

        private void WindowOpened(object sender, EventArgs e)
        {
            var address = Settings.Instance.AdbConfig.AdbAddress;
            this.FindControl<TextBox>("Address").Text = address ?? "192.168.";
            var port = Settings.Instance.AdbConfig.AdbPort;
            this.FindControl<TextBox>("Port").Text = port.ToString();
        }

        private void Ok_Tapped(object sender, RoutedEventArgs e)
        {
            var address = this.FindControl<TextBox>("Address").Text.Trim();
            LastAddress = address;
            var port = int.Parse(this.FindControl<TextBox>("Port").Text.Trim());

            // 缓存上次的地址和端口
            Settings.Instance.AdbConfig.AdbAddress = address;
            Settings.Instance.AdbConfig.AdbPort = port;
            var settingStr = JsonConvert.SerializeObject(Settings.Instance, Formatting.Indented);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"assets\settings.json", settingStr);

            Close((address, port));
        }

        private void Skip_Tapped(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            switch (key)
            {
                case Key.Enter:
                    var address = this.FindControl<TextBox>("Address").Text.Trim();
                    LastAddress = address;
                    var port = int.Parse(this.FindControl<TextBox>("Port").Text.Trim());
                    Close((address, port));
                    break;

                case Key.Escape: Close(); break;

                default: return;
            }
            e.Handled = true;
        }

    }
}
