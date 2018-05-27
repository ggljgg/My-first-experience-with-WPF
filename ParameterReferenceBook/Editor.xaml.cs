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
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            InitializeComponent();
        }

        public Editor(MainWindow parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
        }

        public string TypeParameterName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbName.Text))
                tbName.BorderBrush = Brushes.Red;

            if (tbName.BorderBrush == Brushes.Red)
                return;

            DialogResult = true;
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty((sender as TextBox).Text) || String.IsNullOrWhiteSpace((sender as TextBox).Text))
                (sender as TextBox).BorderBrush = Brushes.Red;
            else
                (sender as TextBox).BorderBrush = Brushes.Gray;
        }
    }
}
