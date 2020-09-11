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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VacationTimeWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FirstDay.Text = DateTime.Now.ToString();
            FinishDay.Text = DateTime.Now.ToString();
        }

        private void FirstDay_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FinishDay.SelectedDate < FirstDay.SelectedDate)
                FinishDay.Text = FirstDay.SelectedDate.ToString();
        }
        private void FinishDay_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FinishDay.SelectedDate<FirstDay.SelectedDate)
                FinishDay.Text = FirstDay.SelectedDate.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string datePeriod;
            VacationPeriodCalendar.DisplayDateStart = FirstDay.SelectedDate;
            VacationPeriodCalendar.DisplayDateEnd = FinishDay.SelectedDate;
            datePeriod = (((DateTime)FinishDay.SelectedDate)
                        .Subtract((DateTime)FirstDay.SelectedDate)
                        .Days+2).ToString();

            VacationPeriodTextBox.Text = datePeriod;
        }
    }
}
