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

            Values = new ChartValues<double> { 78, 85, 89,80, 96, 79 };

            DataContext = this;
        }

        public ChartValues<double> Values { get; set; }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }

        private void InitiateStatistic()
        {
            using (StatisticContext db = new StatisticContext())
            {
                // создаем два объекта User
                Statistic statistic = new Statistic
                {
                    Date = DateTime.Now,
                    type = this.type,
                    seconds = this.seconds,
                    errors = this.errors,
                    letters_count = clicks,
                    clicks_in_second = ClicksPerSecond
                };
                db.Statistics.Add(statistic);
                db.SaveChanges();


                // получаем объекты из бд и выводим на консоль
                var statistic = db.Statistics.ToList();
                Console.WriteLine("Список объектов:");
                foreach (stat u in statistic)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
        }
    }
}