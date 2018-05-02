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

namespace ParameterReferenceBook
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region Properties for interaction between the windows
        public string ParameterName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public short? ParameterMinValue
        {
            get
            {
                if (String.IsNullOrEmpty(tbMin.Text))
                    return null;
                else
                    return Convert.ToInt16(tbMin.Text);
            }
            set { tbMin.Text = value.ToString(); }
        }

        public short ParameterMaxValue
        {
            get { return Convert.ToInt16(tbMax.Text); }
            set { tbMax.Text = value.ToString(); }
        }

        public string ParameterDescription
        {
            get { return tbDescription.Text; }
            set { tbDescription.Text = value; }
        }
        #endregion

        public DialogWindow()
        {
            InitializeComponent();
        }

        public DialogWindow(MainWindow parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbMax.BorderBrush == Brushes.Red)
                return;
            DialogResult = true;
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void tbMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbMax.Text))
                (sender as TextBox).BorderBrush = Brushes.Red;
            else
                tbMax.BorderBrush = Brushes.Gray;
        }
    }
}
