using LiveCharts;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            Values = new ChartValues<double> { };
            NowType = "bsc";
            NowTime = "now";
            InitializeComponent();


            /* Values.Clear();
             Values= new ChartValues<double> {380, 360, 440 };*/

            TableType.Text = "Нет нажатий";
            DataContext = this;
            Date.Text = "";
            InitiateStatistic();
        }

        public ChartValues<double> Values { get; set; }
        
        public string NowType { get; set; }
        public string NowTime { get; set; }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }
        private void Update()
        {
            Chart.Update(true);
        }
        private void Trainer_Checked(object sender, RoutedEventArgs e) // тренажёры
        {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString() == "без")
            {
                NowType = "bsc";
            }
            if (pressed.Content.ToString() == "На скорость")
            {
                NowType = "speed";
            }
            if (pressed.Content.ToString() == "На выносливость")
            {
                NowType = "end";
            }
            if (pressed.Content.ToString() == "На точность")
            {
                NowType = "acc";
            }
            StatisticUpdate();
        }

        private void Time_Checked(object sender, RoutedEventArgs e) // тренажёры
        {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString() == "Последняя запись")
            {
                NowTime = "now";
            }
            if (pressed.Content.ToString() == "День")
            {
                NowTime = "day";
            }
            if (pressed.Content.ToString() == "Неделя")
            {
                NowTime = "week";
            }
            if (pressed.Content.ToString() == "Месяц")
            {
                NowTime = "month";
            }
            if (pressed.Content.ToString() == "Год")
            {
                NowTime = "year";
            }
            if (pressed.Content.ToString() == "Всё время")
            {
                NowTime = "all";
                
            }
            StatisticUpdate();
        }
        private void StatisticUpdate()
        {
            InitiateStatistic();
        }

        private void StatisticExeption()
        {
            MessageBox.Show("Таких записей нет");
        }
        private void InitiateStatistic()
        {
            using (StatisticContext db = new StatisticContext())
            {
                if (NowTime == "now")
                {
                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType
                                     select Statistic).LastOrDefault();
                    if (statistic != null)
                    {
                        Values.Clear();
                        var list = statistic.clicksInMinute.ToList();
                        foreach (var item in list)
                        {
                            Values.Add(item);
                        }
                        Date.Text = statistic.Date.ToString();
                        TableType.Text = "Нажатия в минуту";
                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else if (NowTime == "day")
                {
                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType && (Statistic.Date >= DateTime.Today && Statistic.Date <= DateTime.Today.AddDays(1))
                                     select Statistic).ToList();
                    if (statistic.Count != 0)
                    {
                        Values.Clear();
                        int statTapings;
                        foreach (var stat in statistic)
                        {
                            statTapings = 0;
                            var list = stat.clicksInMinute.ToList();
                            foreach (var item in list)
                            {
                                statTapings += item;
                            }
                            Values.Add(statTapings);
                        }
                        Date.Text = DateTime.Today.ToString("d");
                        TableType.Text = "Нажатия в упражнении";

                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else if (NowTime == "week")
                {
                    var dateValue = DateTime.Today; // Текущая дата без времени
                    var culture = CultureInfo.CurrentCulture; // Текущая культура
                    var weekOffset = culture.DateTimeFormat.FirstDayOfWeek - dateValue.DayOfWeek; // Разница между началом недели, и текущим днем.
                    var startOfWeek = dateValue.AddDays(weekOffset); // Получаем дату начала недели указанной даты
                    var endOfWeek = startOfWeek.AddDays(7); // Получаем дату начала недели указанной даты

                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType && (Statistic.Date >= startOfWeek && Statistic.Date <= endOfWeek)
                                     select Statistic).ToList();
                    if (statistic.Count != 0)
                    {
                        Values.Clear();
                        int statTapings;
                        foreach (var stat in statistic)
                        {
                            statTapings = 0;
                            var list = stat.clicksInMinute.ToList();
                            foreach (var item in list)
                            {
                                statTapings += item;
                            }
                            Values.Add(statTapings);
                        }
                        Date.Text = startOfWeek.ToString("d") + "-" + endOfWeek.AddDays(-1).ToString("d");
                        TableType.Text = "Нажатия в упражнении";

                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else if (NowTime == "month")
                {
                    var dateValue = DateTime.Today; // Текущая дата без времени
                    var startOfMonth = new DateTime(dateValue.Year, dateValue.Month, 1);
                    var endOfMonth = new DateTime(dateValue.Year, dateValue.Month + 1, 1);

                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType && (Statistic.Date >= startOfMonth && Statistic.Date <= endOfMonth)
                                     select Statistic).ToList();
                    if (statistic.Count != 0)
                    {
                        Values.Clear();
                        int statTapings;
                        foreach (var stat in statistic)
                        {
                            statTapings = 0;
                            var list = stat.clicksInMinute.ToList();
                            foreach (var item in list)
                            {
                                statTapings += item;
                            }
                            Values.Add(statTapings);
                        }
                        Date.Text = startOfMonth.ToString("d") + "-" + endOfMonth.AddDays(-1).ToString("d");
                        TableType.Text = "Нажатия в упражнении";

                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else if (NowTime == "year")
                {
                    var dateValue = DateTime.Today; // Текущая дата без времени
                    var startOfYear = new DateTime(dateValue.Year, 1, 1);
                    var endOfYear = new DateTime(dateValue.Year + 1 ,1, 1);

                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType && (Statistic.Date >= startOfYear && Statistic.Date <= endOfYear)
                                     select Statistic).ToList();
                    if (statistic.Count != 0)
                    {
                        Values.Clear();
                        int statTapings;
                        foreach (var stat in statistic)
                        {
                            statTapings = 0;
                            var list = stat.clicksInMinute.ToList();
                            foreach (var item in list)
                            {
                                statTapings += item;
                            }
                            Values.Add(statTapings);
                        }
                        Date.Text = startOfYear.ToString("d") + "-" + endOfYear.AddDays(-1).ToString("d");
                        TableType.Text = "Нажатия в упражнении";

                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else if (NowTime == "all")
                {
                    var statistic = (from Statistic in db.Statistics.OrderBy(e => e.Id)
                                     where Statistic.type == NowType
                                     select Statistic).ToList();
                    if (statistic.Count != 0)
                    {
                        Values.Clear();
                        int statTapings;
                        foreach (var stat in statistic)
                        {
                            statTapings = 0;
                            var list = stat.clicksInMinute.ToList();
                            foreach (var item in list)
                            {
                                statTapings += item;
                            }
                            Values.Add(statTapings);
                        }

                        var firstday = statistic.First().Date;
                        var lastday = statistic.Last().Date;
                        Date.Text = firstday.ToString("d") + "-" + lastday.ToString("d");
                        TableType.Text = "Нажатия в упражнении";

                    }
                    else
                    {
                        StatisticExeption();
                    }
                }
                else
                {
                    StatisticExeption();
                }
            }
            DataContext = this;
            Update();
        }
    }
}