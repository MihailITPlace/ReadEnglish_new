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

namespace ReadEnglish_newUI
{
    /// <summary>
    /// Логика взаимодействия для UIMonitor.xaml
    /// </summary>
    public partial class UIMonitor : UserControl
    {
        //public int RequiredNumberOfWords
        //{
        //    get { return (int)GetValue(RequiredNumberOfWordsProperty); }
        //    set
        //    {
        //        SetValue(RequiredNumberOfWordsProperty, value);
        //        wordsTextBlock.Text = "0/" + value;
        //    }
        //}

        //public static readonly DependencyProperty RequiredNumberOfWordsProperty;

        //static UIMonitor()
        //{

        //    var metaData = new FrameworkPropertyMetadata(25, FrameworkPropertyMetadataOptions.AffectsRender |
        //                             FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        //                             new PropertyChangedCallback(OnRequiredNumberOfWordsChanged),
        //                             new CoerceValueCallback(CoerceRequiredNumberOfWords));

        //    RequiredNumberOfWordsProperty = DependencyProperty.Register("RequiredNumberOfWords", typeof(int), typeof(UIMonitor), metaData);
        //}


        //private static object CoerceRequiredNumberOfWords(DependencyObject d, object value)
        //{
        //    if (value is int)
        //    {
        //        return value;
        //    }

        //    return 0;
        //}

        //private static void OnRequiredNumberOfWordsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    //wordsTextBlock.Text = "0/" + RequiredNumberOfWords;

        //}

        public UIMonitor()
        {
            InitializeComponent();

        }
    }
}
