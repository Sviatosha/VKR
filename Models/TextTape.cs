﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection.Emit;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using VKR.src.Database;

namespace VKR.Models
{
    class TextTape //: INotifyPropertyChanged
    {
        private string textPath { get; set; }
        private string tape { get; set; }
        private string state { get; set; }

        public string TextPath
        {
            get { return textPath; }
            set
            {
                textPath = value;
                OnPropertyChanged("TextPath");
            }
        }
        public string Tape
        {
            get { return tape; }
            set
            {
                tape = value;
                OnPropertyChanged("Tape");
            }
        }
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public TextTape()
        {
            TextPath = "";
            this.Tape = "";
            State = "Created";
            //классы обьявить
        }
        public void textTapeWait()
        {
            Tape = "          " + " Enter-начать Bcsp-пауза cntl+Bcsp-Остановить";
            State = "Wait";
        }

        public void textTapePause()
        {
            State = "Paused";
        }
        public void textTapeContinue()
        {
            State = "Taping";
        }

        public void getExercise(string Path)
        {
            TextPath = Path;
            string? buffer;
            string? oldbuffer = "";
            string line = "";
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    while ((buffer = sr.ReadLine()) != null)
                    {
                        buffer = buffer.Trim();
                        while (oldbuffer != buffer)
                        { //Удаление множественных пробелов
                            oldbuffer = buffer;
                            buffer = buffer?.Replace("  ", " ");
                        }
                        line += buffer + " ";
                    }
                }
                line = line.Trim();
                Tape = "          " + line;
                State = "Taping";
            }
            catch (Exception e)
            {
                Tape = "          " + e.Message;
                State = "Taping";
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        public void initiateFileTape(string Path)
        {
            TextPath = Path;
            string? buffer;
            string? oldbuffer = "";
            string line = "";
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    while ((buffer = sr.ReadLine()) != null)
                    {
                        buffer = buffer.Trim();
                        while (oldbuffer != buffer)
                        { //Удаление множественных пробелов
                            oldbuffer = buffer;
                            buffer = buffer?.Replace("  ", " ");
                        }
                        line += buffer + " ";
                    }
                }
                line = line.Trim();
                Tape = "          " + line;
                State = "Taping";
            }
            catch (Exception e)
            {
                Tape = "          " + e.Message;
                State = "Taping";
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public void initiateWorkOnErrors()//WIP
        {
            string line = "";
            using (ErrorContext db = new ErrorContext())
            {

                var err = db.Errors.ToList();

                if (err.Count > 0)
                {
                    Console.WriteLine("Список объектов:");
                    foreach (Error e in err)
                    {
                        line += e + " ";
                    }
                    line = line.Trim();
                    Tape = "          " + line;
                    State = "Taping";
                    //очистить бд
                }
                else
                {
                    Tape = "          " + "Пусто, ошибок нет!";
                    State = "Taping";
                }
            }
        }

            public void tapeScroll()//при нажатии на кнопку сравниваемый символ перемещается к следующему
        {
            if (Tape.Length > 11)
            {
                Tape = Tape.Remove(0, 1);
            }
            else
            {
                exerciseEnd();
            }
        }

        public void exerciseEnd()
        {
            textTapeWait();
            State = "End";
        }

        public void keyClick(object sender, TextCompositionEventArgs e)
        {
            if (State == "Taping")
            {
                if (String.Equals(Tape[10].ToString(), e.Text.ToString()))
                {
                    tapeScroll();
                }
                else
                {

                }
            }
        }
    }
}
