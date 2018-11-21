using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для textEditorUserControl.xaml
    /// </summary>
    public partial class textEditorUserControl : UserControl
    {
        public event UpdateValue UpdateMonitor;

        private int CountNonSpaceCharacter()
        {
            string text = new TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd).Text;
            return Regex.Matches(text, @"[\w]").Count;          
        }

        public textEditorUserControl()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);            
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbFontSize.Text = "14";
            cmbFontFamily.Text = "Arial";
        }

        private void mainRTB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (mainRTB.Selection.Text.Trim() != string.Empty)
            {
                object temp = mainRTB.Selection.GetPropertyValue(Inline.FontWeightProperty);
                btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
                temp = mainRTB.Selection.GetPropertyValue(Inline.FontStyleProperty);
                btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
                temp = mainRTB.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

                temp = mainRTB.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                cmbFontFamily.SelectedItem = temp;
                temp = mainRTB.Selection.GetPropertyValue(Inline.FontSizeProperty);
                cmbFontSize.Text = temp.ToString();
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
            {
                mainRTB.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }

            mainRTB.Focus();
        }

        private void cmbFontSize_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
            {
                mainRTB.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);
            }

            mainRTB.Focus();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintCommand();
        }

        private void PrintCommand()
        {
            PrintDialog printDialog = new PrintDialog();
            if ((printDialog.ShowDialog() == true))
            {
                var flowDoc = mainRTB.Document;

                flowDoc.PageHeight = printDialog.PrintableAreaHeight;
                flowDoc.PageWidth = printDialog.PrintableAreaWidth;
                flowDoc.PagePadding = new Thickness(25);

                flowDoc.ColumnGap = 0;

                flowDoc.ColumnWidth = (flowDoc.PageWidth -
                                       flowDoc.ColumnGap -
                                       flowDoc.PagePadding.Left -
                                       flowDoc.PagePadding.Right);

                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDoc).DocumentPaginator, "printing as paginator");
            }
        }

        private void mainRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMonitor.Invoke(CountNonSpaceCharacter());            
        }
    }
}
