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

namespace VKR
{
    class TextTape
    {
        public string textPath { get; set; }
        public string tape { get; set; }
        private char letter { get; set; }


        public TextTape()
        {
            textPath = "";
            this.tape = "";
            //классы обьявить
        }
        public void textTapeWait()
        {
            tape = "          " + " Enter-начать Bcsp-пауза cntl+Bcsp-Остановить";
            letter = tape[10];
        }
        public void initiateFileTape(string Path)
        {
            textPath = Path;
            string? buffer;
            string? oldbuffer = "";
            string line = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader sr = new StreamReader(textPath))
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
                tape = "          " + line;
                letter = tape[10];
                
                //Tape.Text = tape;
            }
            catch (Exception e)
            {
                tape = "          " + e.Message;
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
        public void tapeScroll()
        {
            tape=tape.Remove(0);
            letter = tape[10];
            //при нажатии на кнопку сравниваемый символ перемещается к следующему
        }

        public void keyClick(object sender, KeyEventArgs e)
        {

            //обработчик нажатия
            //start timer
            tapeScroll();
        }

        public void changeText()
        {
            //обработчик нажатия
        }
        
        private void textBox_Click(object sender, EventArgs e)
        {

        }
    }
}
