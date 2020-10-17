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

namespace AddHolidays4VTWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addHolidaysButton_Click(object sender, RoutedEventArgs e)
        {
            string newDate;
            List<string> datesList=new List<string>();
            foreach (string s in holidayList.Items)
            {
                datesList.Add(s);
            }                
            List<DateTime> newDatesList = holidayCalendar.SelectedDates.ToList<DateTime>();            
            foreach (DateTime date in newDatesList)
            {
                newDate = date.Date.ToString("dd/MM/yyyy");
                if (holidayList.Items.IndexOf(newDate)<0)
                    datesList.Add(newDate);
            }
            datesList.Sort();
            holidayList.Items.Clear();
            foreach (string s in datesList)
            {
                holidayList.Items.Add(s);
            }
        }
    }
}
