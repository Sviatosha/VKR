using System;
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

namespace VKR.Models
{
    class TextTape //: INotifyPropertyChanged
    {
        private string textPath { get; set; }
        private string tape { get; set; }
        private bool isTaping { get; set; }

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
        public bool IsTaping
        {
            get { return isTaping; }
            set
            {
                isTaping = value;
                OnPropertyChanged("isTaping");
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
            IsTaping = false;
            //классы обьявить
        }
        public void textTapeWait()
        {
            Tape = "          " + " Enter-начать Bcsp-пауза cntl+Bcsp-Остановить";
            IsTaping = false;
        }

        public void textTapePause()
        {
            
        }
        public void textTapeContinue()
        {
            
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
                IsTaping = true;
            }
            catch (Exception e)
            {
                Tape = "          " + e.Message;
                IsTaping = true;
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
                IsTaping = true;
            }
            catch (Exception e)
            {
                Tape = "          " + e.Message;
                IsTaping = true;
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
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
                textTapeWait();
            }
        }

        public void keyClick(object sender, TextCompositionEventArgs e)
        {
            if (IsTaping)
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
