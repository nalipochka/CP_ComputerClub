using CP_ComputerClub.Classes;
using CP_ComputerClub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace CP_ComputerClub
{
    /// <summary>
    /// Логика взаимодействия для Min_Or_Max_Window.xaml
    /// </summary>
    public partial class Min_Or_Max_Window : Window
    {
        public Min_Or_Max_Window()
        {
            InitializeComponent();
        }

        private async void min_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (beginning_DatePick.SelectedDate == null || end_DatePick.SelectedDate == null)
                    throw new Exception("Choose both dates!");
                if (end_DatePick.SelectedDate > DateTime.Now)
                    throw new Exception("The end date of the period must not be greater than the current date!");
                using (ComputerClubContext context = new ComputerClubContext())
                {
                    await context.Histories.LoadAsync();
                    var selectPeriod = context.Histories.Select(t => t).Where(t => t.TimeStart.Date >= beginning_DatePick.SelectedDate.Value.Date &&
                    t.TimeEnd.Date <= end_DatePick.SelectedDate.Value.Date).ToList();
                    var selectPeriodGroup = selectPeriod.GroupBy(t => t.TimeStart.Date).
                        SelectMany(t => t.Select(q => new { Date = q.TimeStart.Date, Count = t.Count()})).OrderBy(t=>t.Count);
                    var min = selectPeriodGroup.First();
                    MessageBox.Show($"The minimum number of visitors was: {min.Date.ToString("dd.MM.yyyy")}. In quantity: {min.Count}");
                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("No results found!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private async void max_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (beginning_DatePick.SelectedDate == null || end_DatePick.SelectedDate == null)
                    throw new Exception("Choose both dates!");
                if (end_DatePick.SelectedDate > DateTime.Now)
                    throw new Exception("The end date of the period must not be greater than the current date!");
                using (ComputerClubContext context = new ComputerClubContext())
                {
                    await context.Histories.LoadAsync();
                    var selectPeriod = context.Histories.Select(t => t).Where(t => t.TimeStart.Date >= beginning_DatePick.SelectedDate.Value.Date &&
                    t.TimeEnd.Date <= end_DatePick.SelectedDate.Value.Date).ToList();
                    var selectPeriodGroup = selectPeriod.GroupBy(t => t.TimeStart.Date).
                        SelectMany(t => t.Select(q => new { Date = q.TimeStart.Date, Count = t.Count() })).OrderByDescending(t => t.Count);
                    var max = selectPeriodGroup.First();
                    MessageBox.Show($"The maximum number of visitors was: {max.Date.ToString("dd.MM.yyyy")}. In quantity: {max.Count}");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("No results found!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
