using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            //загружаем из файла даты праздников 
            if (Holydays.LoadHolyDays("holidays.txt", out List<string> fileHolydaysList))
            {
                foreach (string s in fileHolydaysList)
                {
                    holidayList.Items.Add(s);
                }    
            }
        }

        private void addHolidaysButton_Click(object sender, RoutedEventArgs e)
        {
            string stringDate;
            List<DateTime> datesList = new List<DateTime>();
            foreach (string s in holidayList.Items)
            {
                datesList.Add(DateTime.Parse(s));
            }
            List<DateTime> newDatesList = holidayCalendar.SelectedDates.ToList<DateTime>();
            foreach (DateTime date in newDatesList)
            {
                stringDate = date.Date.ToString("dd/MM/yyyy");
                if (holidayList.Items.IndexOf(stringDate) < 0)
                    datesList.Add(date);
            }
            datesList.Sort();
            holidayList.Items.Clear();
            foreach (DateTime date in datesList)
            {
                stringDate = date.Date.ToString("dd/MM/yyyy");
                holidayList.Items.Add(stringDate);
            }
        }

        private void removeHolidaysButton_Click(object sender, RoutedEventArgs e)
        {
            if (holidayList.SelectedItems.Count>0)
            {
                var selectedItems = holidayList.SelectedItems;
                for (int i=holidayList.SelectedItems.Count-1;i>=0;i--)
                {
                    holidayList.Items.Remove(selectedItems[i]);
                }
                removeHolidaysButton.IsEnabled = false;
            }
        }
        private void saveHolidaysButton_Click(object sender, RoutedEventArgs e)
        {
            Holydays.SaveHolyDays(new StreamWriter("holidays.txt"), holidayList.Items);
        }

        private void HolidayForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Holydays.SaveHolyDays(new StreamWriter("holidays.txt"), holidayList.Items);
        }

        private void holidayList_GotFocus(object sender, RoutedEventArgs e)
        {
            removeHolidaysButton.IsEnabled = true;
        }

        private void holidayList_LostFocus(object sender, RoutedEventArgs e)
        {
            removeHolidaysButton.IsEnabled = false;
        }

    }

    public class Holydays
    {

        public static bool LoadHolyDays(string filePath, out List<string> dates)
        {

            dates = new List<string>();
            string fileString;
            try
            {
                StreamReader srFile = new StreamReader(filePath);
                while (!srFile.EndOfStream)
                {
                    fileString = srFile.ReadLine();
                    dates.Add(fileString);
                }

                return true;
            }
            catch (FileNotFoundException e)
            {
                return false;
            }
        }

        public static void SaveHolyDays(StreamWriter swFile, ItemCollection dates)
        {
            try
            {
                foreach (var date in dates)
                {
                    swFile.WriteLine(date.ToString());
                }
                swFile.Close();
                MessageBox.Show("Dates Saved!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
