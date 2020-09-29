using System;
using System.IO;
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
        public List<DateTime> holydaysList=new List<DateTime>();
        public MainWindow()
        {            
            InitializeComponent();
            FirstDay.Text = DateTime.Now.ToString();
            FinishDay.Text = DateTime.Now.ToString();
            
            //загружаем из файла даты праздников 
            if (LoadHolyDays("holidays.txt", out holydaysList))
            {
                
            }
            
            
            


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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var date in HolidayCalendar.SelectedDates)
            {
                holydaysList.Add(date);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VacationPeriodCalendar.DisplayDateStart = FirstDay.SelectedDate;
            VacationPeriodCalendar.DisplayDateEnd = FinishDay.SelectedDate;
            //производим операции с компонентами даты, 
            //добавляем 1 день, поскольку он включатеся в период отпуска
            TimeSpan datePeriod = ((DateTime)FinishDay.SelectedDate).Date
                        .AddDays(1)
                        .Subtract(((DateTime)FirstDay.SelectedDate).Date);

            VacationPeriodTextBox.Text = datePeriod.Days.ToString()
                +"\n"+ RusDayDeclension(Int32.Parse(datePeriod.Days.ToString()));
        }

        private string RusDayDeclension(int dayNum)
        {
            string returnVal = "";
            int mod10 = dayNum % 10;
            if ((mod10 == 0) || (mod10 >= 5 && mod10 <= 9) || (dayNum >= 11 && dayNum <= 14))
                returnVal = "дней";
            else if (mod10 == 1)
                returnVal = "день";
            else
                returnVal = "дня";
            return returnVal;
        }

        private bool LoadHolyDays(string filePath, out List<DateTime> dates)
        {
            
            dates = new List<DateTime>();            
            string fileString;
            try 
            {
                StreamReader srFile = new StreamReader(filePath);
                while (!srFile.EndOfStream)
                {
                    fileString = srFile.ReadLine();
                    dates.Add(DateTime.Parse(fileString));
                }
                return true;
            }
            catch (FileNotFoundException e)
            {
                return false;
            }
        }

        private void SaveHolyDays(StreamWriter swFile, List<DateTime> dates)
        {
            try
            {
                foreach (DateTime date in dates)
                {
                    swFile.WriteLine(dates.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
