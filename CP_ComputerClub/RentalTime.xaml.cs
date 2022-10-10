using CP_ComputerClub.Classes;
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
    /// Логика взаимодействия для RentalTime.xaml
    /// </summary>
    public partial class RentalTime : Window
    {
        public RentalTime(RentalTimeClass rental)
        {
            InitializeComponent();
            rentalTime = rental;
        }
        RentalTimeClass rentalTime;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtBox_startTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtBox_endTime.Text, out DateTime dt))
                {
                    if (DateTime.TryParse(txtBox_startTime.Text, out DateTime dt1))
                    {
                        if(dt > dt1)
                        {
                            rentalTime.TimeStart = dt1;
                            rentalTime.TimeEnd = dt;
                            this.DialogResult = true;
                        }
                        else
                        {
                            throw new Exception("Error! The end time cannot be less than the start time!");
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Please enter the date and time correctly!");
                    }
                }
                else
                {
                    throw new Exception("Please enter the date and time correctly!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
