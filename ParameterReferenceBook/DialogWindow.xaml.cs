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

        public float? ParameterMinValue
        {
            get
            {
                if (String.IsNullOrEmpty(tbMin.Text))
                    return null;
                else
                    return Convert.ToSingle(tbMin.Text);
            }
            set { tbMin.Text = value.ToString(); }
        }

        public float ParameterMaxValue
        {
            get { return Convert.ToSingle(tbMax.Text); }
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

        public DialogWindow(MainWindow parentWindow, String parentButton)
        {
            InitializeComponent();
            Owner = parentWindow;
            if (parentButton == "addButton")
                tbName.IsEnabled = true;
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbName.Text))
            {
                tbName.BorderBrush = Brushes.Red;
                return;
            }
                
            if (String.IsNullOrEmpty(tbMax.Text))
            {
                tbMax.BorderBrush = Brushes.Red;
                return;
            }

            if (!Single.TryParse(tbMax.Text, out float maxResult))
            {
                tbMax.BorderBrush = Brushes.Red;
                return;
            }

            if (!String.IsNullOrEmpty(tbMin.Text))
            {
                if (!Single.TryParse(tbMin.Text, out float minResult) || Single.Parse(tbMin.Text) > Single.Parse(tbMax.Text))
                {
                    tbMin.BorderBrush = Brushes.Red;
                    return;
                }
                else
                    tbMin.BorderBrush = Brushes.Gray;
            }

            DialogResult = true;
        }

        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (
                String.IsNullOrEmpty((sender as TextBox).Text) ||
                String.IsNullOrWhiteSpace((sender as TextBox).Text)
                )
                (sender as TextBox).BorderBrush = Brushes.Red;
            else
                (sender as TextBox).BorderBrush = Brushes.Gray;

            // try parse to float
            if ((sender as TextBox).Name != "tbName")
            {
                if (!Single.TryParse((sender as TextBox).Text, out float result))
                    (sender as TextBox).BorderBrush = Brushes.Red;
                else
                    (sender as TextBox).BorderBrush = Brushes.Gray;
            }
        }
    }
}
