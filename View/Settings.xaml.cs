using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VKR.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public int numb_car;
        private MainWindow mainWindow;
        public Settings(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KeyboardSettings keyboardSettings = new KeyboardSettings(mainWindow);
            keyboardSettings.Owner = this.Owner;
            keyboardSettings.Show();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string select = FontSize.SelectedValue.ToString();
            if (select == "Мелкий")
            {
                mainWindow.Tape.Height = 50;
            }
            if (select == "Маленький")
            {
                mainWindow.Tape.Height = 70;
            }
        }
        private void PWI_Checked(object sender, RoutedEventArgs e) // Меню
        {
            RadioButton pressed = (RadioButton)sender;
            //MessageBox.Show(pressed.Content.ToString());
        }
    }
}
