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
using System.Collections;
using static System.Net.WebRequestMethods;
using System.Reflection.Emit;
using VKR.Models;
using System.Printing;

namespace VKR.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            TextTape tape = new TextTape();
            tape.textTapeWait();
            //this.DataContext = myTextTape[0];
            InitializeComponent();
            myTextTape.Add(tape);

            string textPath = @"Files\TextFiles\TestLesson.txt";

            initiateTapeTest(textPath, tape);
            Tape.Text = myTextTape[0].Tape;

        }
    private static TextTapeSingleton myTextTape = TextTapeSingleton.GetInstance();
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
        private void KeyEvents(object sender, KeyEventArgs e)
        {


            /*string s = "Event" + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
            ProgramName.Text = s;*/

           /* myTextTape[0].keyClick(sender, e);

            Tape.Text = myTextTape[0].Tape;*/

            /* if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;
             i++;
             string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
             lbxEvents.Items.Add(s);*/

        }

        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {

            myTextTape[0].keyClick(sender, e);
            ProgramName.Text = myTextTape[0].IsTaping.ToString()+" "+ myTextTape[0].Tape[10] + " " +e.Text;

            Tape.Text = myTextTape[0].Tape;
        }

    }
}
