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
        public void initiateFileTape(string Path)
        {
            TextPath = Path;
            string? buffer;
            string? oldbuffer = "";
            string line = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader sr = new StreamReader(TextPath))
                {
                    while ((buffer = sr.ReadLine()) != null)
                    {

                        buffer = buffer.Trim();
                        while (oldbuffer != buffer)
                        { //Удаление множественных пробелов
                            oldbuffer = buffer;
                            buffer = buffer?.Replace("  ", " ");
                            Console.WriteLine("buffer:" + buffer);
                        }
                        line += buffer + " ";
                        Console.WriteLine("line:" + line);
                    }
                }
                Tape = "                    " + line;
                IsTaping = true;
            }
            catch (Exception e)
            {
                Tape = "                    " + e.Message;
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public void clockStart()
        {
            //clock start
            //при нажатии на кнопку сравниваемый символ перемещается к следующему
        }
        public void tapeScroll()//при нажатии на кнопку сравниваемый символ перемещается к следующему
        {
            if (Tape.Length > 21)
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

            if (String.Equals(Tape[20].ToString(), e.Text.ToString()))
            {
                tapeScroll();
            }
            else
            {

            }
            //обработчик нажатия
            //start timer
        }

    }
}
