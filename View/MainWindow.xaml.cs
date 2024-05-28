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
using static System.Net.Mime.MediaTypeNames;

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
            initiateExercise();
            Wait();

            timerCreate();

        }
        private static TextTapeSingleton myTextTape = TextTapeSingleton.GetInstance();
        private DispatcherTimer timer = null;
        private int seconds, clicks;

        private void timerCreate()
        {
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) }; // 1 секунда
            timer.Tick += timerTick;
            timerForm();
        }
        private void timerStart()
        {
            timer.Start();
            timerForm();
        }
        private void timerEnd()
        {
            timer.Stop();
            seconds = 0;
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

            string timeInterval = ts.ToString();

            Timer.Text = timeInterval;

        }
        
        private void Wait()//Экран ожидания
        {
            myTextTape[0].textTapeWait();
            Tape.Text = myTextTape[0].Tape;
        }
        private void KeyEvents(object sender, KeyEventArgs e)
        {

        }
        private void startLetters()
        {
            string pressedLetter, letterToPress;
            letterToPress = myTextTape[0].Tape[10].ToString();

            if (letterToPress == " ")
            {
                letterToPress = "Пробел";
            }

            TapeNextLetters.Text = $"Следующая клавиша: {letterToPress}";
            TapeInputLetters.Text = $" Нажатая клавиша: ";
        }
        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {
            if (myTextTape[0].Tape[10].ToString() != e.Text)
            {
                TapeInputLetters.Foreground = Brushes.Red;

            }
            else
            {
                TapeInputLetters.Foreground = Brushes.Black;
            }
            myTextTape[0].keyClick(sender, e);

            string pressedLetter, letterToPress;
            letterToPress = myTextTape[0].Tape[10].ToString();
            pressedLetter = e.Text;
            if (pressedLetter == " ")
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
            clicks++;
            changeVision();
        }

        private void changeVision()
        {
            if (myTextTape[0].IsTaping == true)
            {
                TapeNextLetters.Visibility = Visibility.Visible;
                TapeInputLetters.Visibility = Visibility.Visible;
                timerStart();
            }
            else if (myTextTape[0].IsTaping == false)
            {
                TapeNextLetters.Visibility = Visibility.Hidden;
                TapeInputLetters.Visibility = Visibility.Hidden;
                timerEnd();
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem.Header.ToString() == "Свой текст")
            {
                loadFile();
            }
            else if (menuItem.Header.ToString() == "Упражнения")
            {
                
            }
            else
            {
                getExercise(menuItem.Header.ToString());
            }
        }
        private void loadFile()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                initiateTape(filename);

            }
        }
        private void initiateTape(string textPath)
        {
            myTextTape[0].initiateFileTape(textPath);
            Tape.Text = myTextTape[0].Tape;
            startLetters();
            timerStart();
        }
        private void initiateExercise()//WIP
        {
            string[] allfiles = Directory.GetFiles(@"..\..\..\src\Exercises");
            foreach (string filename in allfiles)
            {
                MenuItem exerc = new MenuItem();
                exerc.Header = filename;
                exerc.Click += MenuItem_Click;
                ExerciseMenu.Items.Add(exerc);
            }
            
        }
        private void getExercise(string Path)
        {
            myTextTape[0].getExercise(Path);
            Tape.Text = myTextTape[0].Tape;
            startLetters();
            timerStart();
        }
    }
}