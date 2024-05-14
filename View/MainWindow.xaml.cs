using System;
using System.Data;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using System.Reflection.Emit;
using VKR.Models;

namespace VKR.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
/*
            TextTape tape = new TextTape();
            tape.textTapeWait();

            this.DataContext = tape;

            string textPath = @"Files\TextFiles\TestLesson.txt";

            initiateTapeTest(textPath,tape);*/

        }
        private void initiateTapeTest(string textPath,TextTape tape)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string filePath = System.IO.Path.Combine(currentPath, textPath);

            tape.initiateFileTape(filePath);
        }
        private void Wait(TextTape tape)
        {
            tape.textTapeWait();
        }
        private void KeyDownEvents(object sender, KeyEventArgs e)
        {

            /* string s = "Event" + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
             ProgramName.Text += s;

             if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;
             i++;
             string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
             lbxEvents.Items.Add(s);*/
        }
        /*
                
                private void Clear_Click(object sender, RoutedEventArgs e)
                {
                    lbxEvents.Items.Clear();
                    txtContent.Clear();
                    i = 0;
                }

                protected int i = 0;
                private void KeyEvents(object sender, KeyEventArgs e)
                {
                    if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;
                    i++;
                    string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
                    lbxEvents.Items.Add(s);
                }

                private void TextInputEvent(object sender, TextCompositionEventArgs e)
                {
                    i++;
                    string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Text;
                    lbxEvents.Items.Add(s);
                }
        */
    }
}
