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
using AddHolidays4VTWPF;

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
            if (Holydays.LoadHolyDays("holidays.txt", out List<string> fileHolydaysList))
            {
                foreach (string s in fileHolydaysList)
                {
                    holydaysList.Add(DateTime.Parse(s));
                }               
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

 
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            int i0=0, iN=0, numHolydays=0;
            DateTime startDate = (DateTime)FirstDay.SelectedDate;
            DateTime finishDate=(DateTime)FinishDay.SelectedDate;
            VacationPeriodCalendar.DisplayDateStart = FirstDay.SelectedDate;
            VacationPeriodCalendar.DisplayDateEnd = FinishDay.SelectedDate;
            //производим операции с компонентами даты, 
            //добавляем 1 день, поскольку он включатеся в период отпуска
            
            if (holydaysList.Count>0)
            {
                if (!(finishDate.Date < holydaysList.First().Date || startDate.Date > holydaysList.Last().Date))
                {
                    i0 = holydaysList.FindIndex(x => x >= startDate.Date);
                        if (i0 == -1)
                            i0 = 0;
                    iN = holydaysList.FindLastIndex(x => x <= finishDate.Date);
                        if (iN == -1)
                            iN = holydaysList.Count-1;
                    numHolydays = iN - i0+1;
                    
                }

            }

            TimeSpan datePeriod = finishDate.Date
                        .AddDays(1-numHolydays)
                        .Subtract((startDate).Date);

            VacationPeriodTextBox.Text = datePeriod.Days.ToString()
                +"\n"+ RusDayDeclension(Int32.Parse(datePeriod.Days.ToString()));
            HolydaysTextBox.Text = numHolydays.ToString()+" "+RusDayDeclension(Int32.Parse(numHolydays.ToString()));
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

        
    }
}
