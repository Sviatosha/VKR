using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace VKR.Models
{
    internal class TextTapeSingleton : CollectionBase
    {
        private static TextTapeSingleton instance = new TextTapeSingleton();

        public TextTapeSingleton() { }

        public static TextTapeSingleton GetInstance()
        {
            return instance;
        }

        //МЕТОДЫ КЛАССА
        public void Add(TextTape newRPU)
        {
            List.Add(newRPU); //Метод добавить элемент
        }
        public void Remove(TextTape oldRPU)
        {
            List.Add(oldRPU); //Метод удалить элемент
        }
        public TextTape this[int IndexRPU] //Индексатор в коллекции
        {
            get
            {
                return (TextTape)List[IndexRPU];
            }
            set
            {
                List[IndexRPU] = value;
            }
        }
    }
}
