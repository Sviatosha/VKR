using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VKR.View
{
    /// <summary>
    /// Логика взаимодействия для KeyboardSettings.xaml
    /// </summary>
    public partial class KeyboardSettings : Window
    {
        private MainWindow mainWindow;
        public KeyboardSettings(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        /*private void KeyboardUpdate(object sender, RoutedEventArgs e)
        {
            Button pressed = (Button)sender;
            if (pressed.Name == "FirstRow0") {
               
                foreach (string key in GetKeys("FirstRow0"))
                {
                    colors[key] = fingerCollor(CurrentFinger);
                }

            }
            ...
            colors[key] = fingerCollor;
            switch (key)
            {
                case "1":
                    FirstRow1.Background = (Brush)bc.ConvertFrom(colors[key]);
                    break;
                case "2":
                    FirstRow2.Background = (Brush)bc.ConvertFrom(colors[key]);
                    break;
                case "3":
                    FirstRow3.Background = (Brush)bc.ConvertFrom(colors[key]);
                    break;
                default:
                    break;
            }
        }*/
        private string[] GetKeys(string smth)
        {
            return new string[] { "1", "2", "3" };
        }
    }
}
