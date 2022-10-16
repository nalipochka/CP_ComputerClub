using CP_ComputerClub.Classes;
using CP_ComputerClub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

//17.Программа для компьютерного клуба.
//    Суть программы:
//    Программа предназначена для хранения информации об оборудовании компьютерного клуба, учета занятости компьютеров, ремонта компьютеров. 
//    Программа будет позволять заказывать рабочее место за раннее и на определенное время.
//    Программа будет также определять наиболее доходное время работы и делать статистику посещаемости за месяц.

//    Требования:
//    База данных содержит информацию окомпьютерах, о занятых и свободных машинах, о времени заказа.

//    Программа должна позволять заказать машину на определенное время.Программа должна учитывать ремонт машин.

//    Программа должна осуществлять анализ посещаемости за месяц и определять наиболее доходное время работы.

//    Программа  должна  определять  минимальное  и  максимальное  количество посетителей за определенный период.

//    Список компьютеров отображается в ListView.Добавление и редактирование осуществляется в отдельных окнах.
namespace CP_ComputerClub
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
        }
        DispatcherTimer timer;
        List<ViewComputerData>? DataList;


        private void UpdateListViewData(ComputerClubContext context)
        {
            DataList = context.Computers.Select(t => new ViewComputerData { Id = t.Id, TypePC = t.TypePc.TypePc, Broken = t.IsBroken, Busy = t.IsBusy, IsBusy = "", IsBroken = "", DateTimeStart = t.TimeStart, DateTimeEnd = t.TimeEnd }).ToList();
            listView_Computers.ItemsSource = null;
            listView_Computers.ItemsSource = DataList;
            
        }
        private void UpdateHistoryBox(string text)
        {
            StringBuilder sb = new StringBuilder(txtBox_history.Text);
            sb.AppendLine($"{DateTime.Now.ToString("[dd.MM.yyyy] HH:mm")}: {text}");
            txtBox_history.Text = sb.ToString();
        }
        private async Task AddDataToHistoryAsync(DateTime? TimeStart, DateTime? TimeEnd, double price, int TypePcid)
        {
            using (ComputerClubContext context = new ComputerClubContext())
            {

                TimeSpan timeSpan = TimeEnd!.Value - TimeStart!.Value;
                double hours = timeSpan.TotalHours;
                double totalSum = Math.Round((price * hours), 2);
                History history = new History { TimeStart = TimeStart!.Value, TimeEnd = TimeEnd!.Value, TotalSum = (decimal)totalSum, TypePcid = TypePcid };
                await context.Histories.AddAsync(history);
                await context.SaveChangesAsync();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_start.IsEnabled = false;
            btn_stop.IsEnabled = false;
            using (ComputerClubContext context = new ComputerClubContext())
            {
                UpdateListViewData(context);
            }
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private async void Timer_Tick(object? sender, EventArgs e)
        {
            using (ComputerClubContext context = new ComputerClubContext())
            {
                await context.Computers.Include(t => t.TypePc).LoadAsync();
                int count = -1;
                foreach (var item in context.Computers)
                {
                    if (item.TimeEnd != null && item.TimeStart != null)
                    {
                        if (item.TimeEnd <= DateTime.Now)
                        {
                            await AddDataToHistoryAsync(item.TimeStart, item.TimeEnd, (double)item.TypePc.Price, item.TypePcid);
                            item.TimeStart = null;
                            item.TimeEnd = null;
                            item.IsBusy = false;
                            UpdateHistoryBox($"Computer #{item.Id}. Time is over. PC is free.");
                            count = 1;
                        }
                    }
                }
                if (count != -1)
                {
                    await context.SaveChangesAsync();
                    UpdateListViewData(context);
                }
            }
        }


        private void listView_Computers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView_Computers.SelectedItem != null)
            {
                ViewComputerData? data = listView_Computers.SelectedItem as ViewComputerData;
                if (data!.Broken == true || data.Busy == true)
                {
                    btn_start.IsEnabled = false;
                    btn_stop.IsEnabled = true;
                }
                else
                {
                    btn_start.IsEnabled = true;
                    btn_stop.IsEnabled = false;
                }
            }
            else
            {
                btn_start.IsEnabled = false;
                btn_stop.IsEnabled = false;
            }
        }


        private async void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (listView_Computers.SelectedItem != null)
            {
                ViewComputerData? data = listView_Computers.SelectedItem as ViewComputerData;
                RentalTimeClass rentalTime = new RentalTimeClass();
                RentalTime rental = new RentalTime(rentalTime);


                if (rental.ShowDialog() == true)
                {
                    TimeSpan tmpTime = rentalTime.TimeEnd - rentalTime.TimeStart;
                    double hours = tmpTime.TotalHours;
                    using (ComputerClubContext context = new ComputerClubContext())
                    {
                        await context.Computers.Include(t => t.TypePc).LoadAsync();
                        foreach (var item in context.Computers)
                        {
                            if (item.Id == data!.Id)
                            {
                                rentalTime.TotalPrice = Math.Round((Convert.ToDouble(item.TypePc.Price) * hours), 2);
                                item.TimeStart = rentalTime.TimeStart;
                                item.TimeEnd = rentalTime.TimeEnd;
                                item.IsBusy = true;
                                UpdateHistoryBox($"Computer #{item.Id}. Time is running. PC busy. TotalPrice:{rentalTime.TotalPrice} UAH");
                                break;
                            }
                        }
                        await context.SaveChangesAsync();
                        UpdateListViewData(context);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please, select anyone unoccupied  computer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            if (listView_Computers.SelectedItem != null)
            {
                ViewComputerData? data = listView_Computers.SelectedItem as ViewComputerData;
                using (ComputerClubContext context = new ComputerClubContext())
                {
                    await context.Computers.Include(t => t.TypePc).LoadAsync();
                    foreach (var item in context.Computers)
                    {
                        if (item.Id == data!.Id)
                        {
                            await AddDataToHistoryAsync(item.TimeStart, DateTime.Now, (double)item.TypePc.Price, item.TypePcid);
                            item.TimeStart = null;
                            item.TimeEnd = null;
                            item.IsBusy = false;
                            UpdateHistoryBox($"Computer #{item.Id}. Forced rental stop. PC is free.");
                            break;
                        }
                    }
                    await context.SaveChangesAsync();
                    UpdateListViewData(context);
                }
            }
        }

        private async void mounth_analysis_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerClubContext context = new ComputerClubContext())
            {
                await context.Histories.LoadAsync();
                var lastMonthClients = context.Histories.Select(t => t).Where(t => t.TimeStart.Month == 9 /*DateTime.Now.Month*/);

                /*07:00 - 12:00*/
                var morningClients = lastMonthClients.Select(t => t).
                  Where(t => t.TimeStart.Hour >= 7 && t.TimeStart.Hour < 12).Count();
                /*12:00 - 21:00*/
                var dayClients = lastMonthClients.Select(t => t).
                  Where(t => t.TimeStart.Hour >= 12 && t.TimeStart.Hour < 21).Count(); ;
                /*21:00 - 07:00*/
                var nightClients = lastMonthClients.Select(t => t).
                  Where(t => t.TimeStart.Hour >= 21 || t.TimeStart.Hour < 07).Count(); ;

                int max = morningClients;
                if (dayClients > max)
                    max = dayClients;
                if (nightClients > max)
                    max = nightClients;


                if (max == morningClients)
                    MessageBox.Show("Based on the analysis for the last month, the effective working time is:\r\n\"Morning (07:00 - 12:00)\"",
                         "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (max == dayClients)
                    MessageBox.Show("Based on the analysis for the last month, the effective working time is:\r\n\"Day (12:00 - 21:00)\"",
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Based on the analysis for the last month, the effective working time is:\r\n\"Night (21:00 - 07:00)\"",
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void min_or_max_quantity_Click(object sender, RoutedEventArgs e)
        {
            Min_Or_Max_Window window = new Min_Or_Max_Window();
            window.Show();
        }
    }
}
