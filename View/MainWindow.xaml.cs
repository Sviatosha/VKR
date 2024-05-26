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
using System.Windows.Threading;

namespace VKR.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        /*DispatcherTimer timer;*/
        public MainWindow()
        {
            TextTape tape = new TextTape();
            myTextTape.Add(tape);
            InitializeComponent();
            myTextTape[0].textTapeWait();


            string textPath = @"Files\TextFiles\TestLesson.txt";

            initiateTape(textPath);
            Tape.Text = myTextTape[0].Tape;

            timerStart();
        }
        private static TextTapeSingleton myTextTape = TextTapeSingleton.GetInstance();
        private DispatcherTimer timer = null;
        private int seconds;
        private void timerStart()
        {
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) }; // 1 секунда
            timer.Tick += timerTick;
            timer.Start();
            timerForm();
        }
        private void timerEnd()
        {

            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) }; // 1 секунда
            timer.Tick += timerTick;
            timer.Start();
            timerForm();
        }
        private void timerTick(object sender, EventArgs e)
        {
            seconds++;
            timerForm();
        }
        private void timerForm()
        {
            var ts = TimeSpan.FromSeconds(seconds);

            /* Timer.Text = $"{ts.Hours} ч. {ts.Minutes} м. {ts.Seconds} с.";*/

            string timeInterval = "Время: "+ts.ToString();

            Timer.Text = timeInterval;

        }
        private void initiateTape(string textPath)
        {
            myTextTape[0].initiateFileTape(textPath);
            startLetters();
        }
        private void Wait()
        {
            myTextTape[0].textTapeWait();
        }
        private void KeyEvents(object sender, KeyEventArgs e)
        {

        }
        private void startLetters()
        {
            string pressedLetter, letterToPress;
            letterToPress = myTextTape[0].Tape[20].ToString();

            if (letterToPress == " ")
            {
                letterToPress = "Пробел";
            }

            TapeNextLetters.Text = $"Следующая клавиша: {letterToPress}";
            TapeInputLetters.Text = $" Нажатая клавиша: ";
        }
        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {
            if(myTextTape[0].Tape[20].ToString()!= e.Text)
            {
                TapeInputLetters.Foreground = Brushes.Red; 

            }
            else
            {
                TapeInputLetters.Foreground = Brushes.Black;
            }
            myTextTape[0].keyClick(sender, e);

            string pressedLetter, letterToPress;
            letterToPress = myTextTape[0].Tape[20].ToString();
            pressedLetter = e.Text;
            if (pressedLetter==" ")
            {
                pressedLetter = "Пробел";
            }
            if (letterToPress == " ")
            {
                letterToPress = "Пробел";
            }

            TapeNextLetters.Text = $"Следующая клавиша: {letterToPress}";
            TapeInputLetters.Text = $" Нажатая клавиша: {pressedLetter}";

            Tape.Text = myTextTape[0].Tape;
        }
    }
}
