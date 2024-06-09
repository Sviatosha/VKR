using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using VKR.src.Database;

namespace VKR.View
{
    /// <summary>
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        private double _lastLecture;
        private double _trend;
        public StatisticWindow()
        {
            InitializeComponent();
            Values = new ChartValues<double> { };
            Date = new DateTime();


            InitiateStatistic();



            DataContext = this;
        }

        public ChartValues<double> Values { get; set; }
        public DateTime Date { get; set; }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }

        private void InitiateStatistic()
        {
            Values.Clear();
            using (StatisticContext db = new StatisticContext())
            {
                // получаем объекты из бд и выводим на консоль
                var statistic = db.Statistics.OrderBy(e => e.Id).LastOrDefault();
                var list=statistic.clicksInSecond.ToList();
                foreach (var item in list)
                {
                    Values.Add(item);
                }
                Date=statistic.Date;
                TableType.Text = "Нажатия в секунду";
            }
        }
        private void allStatistic()
        {
            using (StatisticContext db = new StatisticContext())
            {
                // получаем объекты из бд и выводим на консоль
                db.Statistics.LastOrDefault();
                var statistic = db.Statistics.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Statistic s in statistic)
                {
                    Console.WriteLine($"{s.type} - {s.seconds}");
                }
            }
        }
    }
}