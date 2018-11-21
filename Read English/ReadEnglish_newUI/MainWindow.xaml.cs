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
using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;

namespace ReadEnglish_newUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        List<DictionaryEntry> dictionaryEntryList;
        StatisticsMonitor test;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            dictionaryEntryList = new List<DictionaryEntry>();
            test = new StatisticsMonitor("ddddd");            

            dictionaryDataGrid.ItemsSource = dictionaryEntryList;

            //Тестирование доступа
            mainMonitor.Gauge.Value = 67;
            textMonitor.Gauge.Value = 35;

            mainMonitor.wordsTextBlock.SetBinding(TextBlock.TextProperty, "test.bla");
           

            test.bla = "12235";
            //dictionaryEntryList.Add(new DictionaryEntry("aaa", "aaa" , "sss"));
        }

    }
}
