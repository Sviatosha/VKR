using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VKR.Models;
using VKR.src.Database;
using VKR.View.About;
using VKR.View.Help;
using VKR.View.Lessons;
using WMPLib;

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
            initiateExercise();//
            Wait();// 
            timerCreate();// Создать таймер

            var colors = new Dictionary<string, string>()
            {
                { "0", "#FF2400"},
                { "1", "#FFB841"},
                { "2", "#FFB841"},
            };

        }
        private static TextTapeSingleton myTextTape = TextTapeSingleton.GetInstance();//singleton
        private DispatcherTimer timer = null;// Обьявление таймера
        private int seconds = 0, clicks = 0, errors = 0, cpm = 0;//секунды и нажатия
        private List<string> Errors = new List<string>();
        private List<int> ClicksPerMinute = new List<int>();
        private string type = "bsc";


        /*BrushConverter bc = new BrushConverter();
        string error_collor = "#2F353B";

        private void KeyboardActivate(string key)
        {

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
        }
        private void KeyboardError(string key)
        {
            switch (key)
            {
                case "1":
                    FirstRow1.Background = (Brush)bc.ConvertFrom(error_collor);
                    break;
                case "2":
                    FirstRow2.Background = (Brush)bc.ConvertFrom(error_collor);
                    break;
                case "3":
                    FirstRow3.Background = (Brush)bc.ConvertFrom(error_collor);
                    break;
                default:
                    break;
            }
        }*/

        /*
        * Обработка таймера
        */
        private void timerCreate() //Создание таймера
        {
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) }; // 1 секунда
            timer.Tick += timerTick;
            timerForm();
        }
        private void timerStart() //Запуск таймера
        {
            timer.Start();
            timerForm();
        }
        private void timerEnd() //Остановка таймера
        {
            timer.Stop();
            seconds = 0;
        }

        private void timerBackwards() // WIP Запуск таймера обратного отчёта
        {

        }
        private void timerTick(object sender, EventArgs e) // Обработка изменения времени в таймере
        {
            seconds++;

            if (seconds % 60 == 0)
            {
                ClicksPerMinute.Add(cpm);
                cpm = 0;
            }
            timerForm();
        }
        private void timerForm() // Обработка изменения времени в таймере
        {
            var ts = TimeSpan.FromSeconds(seconds);
            string timeInterval = ts.ToString();
            Timer.Text = timeInterval;
        }

        /*
        * Обработка Текстовой ленты
        */

        private void startLetters() // Инициализация нажатой и следующей буквы
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

        private void changeVision()// Видимость нажатой и следующей буквы
        {
            if (myTextTape[0].State == "Start")
            {
                TapeNextLetters.Visibility = Visibility.Visible;
                TapeInputLetters.Visibility = Visibility.Visible;
                timerStart();
                myTextTape[0].State = "Taping";
            }
            else if (myTextTape[0].State == "Taping")
            {

            }
            else if (myTextTape[0].State == "Wait")
            {
                TapeNextLetters.Visibility = Visibility.Collapsed;
                TapeInputLetters.Visibility = Visibility.Collapsed;
            }
            else if (myTextTape[0].State == "End")
            {
                Wait();
                TapeNextLetters.Visibility = Visibility.Collapsed;
                TapeInputLetters.Visibility = Visibility.Collapsed;
                ExerciseEnd();
            }
        }

        /*private void AccuracyTrain() //Доработка
        {
            if (errors > 10)
            {
                MessageBox.Show("Упражнение провалено");
                ExerciseEnd();
            }
        }
        private void DexterityTrain()
        {
            if (errors > 10)
            {
                ExerciseEnd();
            }
        }
        private void SpeedTrain()
        {
            if (errors > 10)
            {
                ExerciseEnd();
            }
        }*/

        private void Wait() //Экран ожидания
        {
            myTextTape[0].textTapeWait();
            Tape.Text = myTextTape[0].Tape;
        }

        private void TextInputEvent(object sender, TextCompositionEventArgs e) //обработка нажатия для текстовой ленты
        {
            if (myTextTape[0].State == "Taping" || myTextTape[0].State == "Start")
            {
                clicks++;
                cpm++;
                if (myTextTape[0].Tape[10].ToString() != e.Text)
                {
                    errors++;
                    Errors.Add(e.Text);
                    TapeInputLetters.Foreground = Brushes.Red;
                    //WrongKeyShow();
                }
                else
                {
                    TapeInputLetters.Foreground = Brushes.Black;
                    //RightKeyShow();
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
                changeVision();
                soundPlay();
            }
        }

        private void loadFile() //загрузка своего файла
        {
            type = "free";
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
        private void initiateTape(string textPath) //инициализация Текстовой ленты
        {
            myTextTape[0].initiateFileTape(textPath);
            Tape.Text = myTextTape[0].Tape;
            startLetters();
            timerStart();
        }

        private void ErrorWork()
        {
            myTextTape[0].initiateWorkOnErrors();
            Tape.Text = myTextTape[0].Tape;
            startLetters();
            changeVision();
            timerStart();
        }

        /*
        * Обработка переключателей и меню
        */
        private void Trainer_Checked(object sender, RoutedEventArgs e) // тренажёры
        {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString() == "без")
            {
                TrainerSelected.Text = "";
                TrainerSelected.Visibility = Visibility.Hidden;
                type = "bsc";
            }
            if (pressed.Content.ToString() == "На скорость")
            {
                TrainerSelected.Text = "Тренажёр: " + pressed.Content.ToString();
                TrainerSelected.Visibility = Visibility.Visible;
                type = "speed";
            }
            if (pressed.Content.ToString() == "На выносливость")
            {
                TrainerSelected.Text = "Тренажёр: " + pressed.Content.ToString();
                TrainerSelected.Visibility = Visibility.Visible;
                type = "end";
            }
            if (pressed.Content.ToString() == "На точность")
            {
                TrainerSelected.Text = "Тренажёр: " + pressed.Content.ToString();
                TrainerSelected.Visibility = Visibility.Visible;
                type = "acc";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) // Меню WIP
        {
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Header.ToString() == "Свободная печать")
            {
                loadFile();
            }
            else if (menuItem.Header.ToString() == "Работа над ошибками")
            {
                ErrorWork();
            }
            else if (menuItem.Header.ToString() == "Статистика")
            {
                StatisticOpen();
            }
            else if (menuItem.Header.ToString() == "Настройки")
            {
                Settings settingsWindow = new Settings(this);
                /*settingsWindow.Owner = this;*/
                settingsWindow.Show();
            }
            else if (menuItem.Header.ToString() == "Помощь")
            {
                HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
            }
            else if (menuItem.Header.ToString() == "О программе")
            {
                AboutWindow aboutWindow = new AboutWindow();
                aboutWindow.Show();
            }
        }

        private void StatisticOpen()
        {
            StatisticWindow statisticWindow = new StatisticWindow();
            statisticWindow.Show();
        }

        private void Lessons_Click(object sender, RoutedEventArgs e) // Меню WIP
        {
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Header.ToString() == "Что такое метод слепой печати")
            {
                LessonsBook LessonsWindow = new LessonsBook();
                LessonsWindow.Show();
            }
            else if (menuItem.Header.ToString() == "Перед печатью")
            {
                Lesson2 lesson2 = new Lesson2();
                lesson2.Show();
            }
            else if (menuItem.Header.ToString() == "Метод десятепальцевой слепой печати")
            {
                Lesson3 lesson3 = new Lesson3();
                lesson3.Show();
            }
            else if (menuItem.Header.ToString() == "Для людей с ограниченными возможностями")
            {
                Lesson4 lesson4 = new Lesson4();
                lesson4.Show();
            }
        }
        private void Exercises_Click(object sender, RoutedEventArgs e) // Меню WIP
        {
            MenuItem menuItem = (MenuItem)sender;

            getExercise(menuItem.Header.ToString());

        }
        private void initiateExercise()//WIP инициировать упражнение
        {
            string path = @"..\..\..\src\Exercises";

            string[] languagueDirectories = Directory.GetDirectories(path);
            foreach (string ldirectory in languagueDirectories)
            {
                MenuItem lang = new MenuItem();
                string langName = System.IO.Path.GetFileName(ldirectory);
                lang.Header = langName;

                string[] allDerectories = Directory.GetDirectories(ldirectory);
                foreach (string directory in allDerectories)
                {
                    MenuItem dir = new MenuItem();
                    string dirName = System.IO.Path.GetFileName(directory);
                    dir.Header = dirName;
                    string[] allfiles = Directory.GetFiles(directory);

                    foreach (string filename in allfiles)
                    {
                        MenuItem exerc = new MenuItem();
                        string fName = System.IO.Path.GetFileName(filename);

                        exerc.Header = fName;
                        exerc.Click += Exercises_Click;

                        dir.Items.Add(exerc);
                    }
                    lang.Items.Add(dir);
                }
                ExerciseMenu.Items.Add(lang);
            }
        }
        private void getExercise(string fileName) //инициализация Упражнений
        {
            string path = @"..\..\..\src\Exercises";// Путь к Директории Exercises

            string[] languagueDirectories = Directory.GetDirectories(path);
            foreach (string ldirectory in languagueDirectories)
            {
                string languagueName = System.IO.Path.GetFileName(ldirectory);
                string[] allDirectories = Directory.GetDirectories(ldirectory); // Список путей к папкам в директории Exercises

                foreach (string directory in allDirectories)// Для каждой подпапки Exercises
                {
                    string dirName = System.IO.Path.GetFileName(directory);// Путь к подпапке Exercises
                    string[] allfiles = Directory.GetFiles(directory);// Список путей к подпапке в подпапке директории Exercises
                    foreach (string filename in allfiles)
                    {
                        string fName = System.IO.Path.GetFileName(filename);

                        if (fName == fileName)
                        {
                            myTextTape[0].getExercise(filename);
                            Tape.Text = myTextTape[0].Tape;
                            startLetters();
                            timerStart();
                        }
                    }
                }
            }
        }

        private void ExerciseEnd()
        {
            timerEnd();
            myTextTape[0].textTapeWait();

            if (cpm != 0)
            {
                ClicksPerMinute.Add(cpm);
                cpm = 0;
            }
            using (StatisticContext db = new StatisticContext())
            {

                Statistic statistic = new Statistic
                {
                    Date = DateTime.Now,
                    type = this.type,
                    seconds = this.seconds,
                    errors = this.errors,
                    letters_count = clicks,
                    clicksInMinute = ClicksPerMinute.ToArray()
                };
                db.Statistics.Add(statistic);
                db.SaveChanges();
            }
            using (ErrorContext db = new ErrorContext())
            {
                foreach (var err in Errors)
                {
                    Error e = new Error
                    {
                        error = err
                    };
                    db.Errors.Add(e);
                }
                db.SaveChanges();
            }
            counterNull();
            StatisticOpen();
        }
        private void counterNull()
        {
            //rbNone.IsChecked = true;
            seconds = 0; clicks = 0; errors = 0; cpm = 0;

            Errors.Clear();
            ClicksPerMinute.Clear();
        }

        /*
        * Обработка Горячих клавиш
        */
        private void KeyEvents(object sender, KeyEventArgs e)
        {
            /*if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F1)
                Pause();
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F2)
                Continue();*/
            if (e.Key == System.Windows.Input.Key.Back && myTextTape[0].State == "Taping")
                ExerciseEnd();
        }
        /*
        * Обработка звуков
        */
        WindowsMediaPlayer wmp = new WindowsMediaPlayer();
        string fullPath = Path.GetFullPath(@"..\..\..\src\sounds\keyboardClick.mp3");
        private void soundPlay() //
        {

            wmp.URL = fullPath;
            wmp.controls.play();
        }
        private void keyboardValidation() //
        {

        }
        private void keyboardActivate() //
        {

        }

    }
}