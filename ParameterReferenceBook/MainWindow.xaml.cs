using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data.Entity;

namespace ParameterReferenceBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private long _tagId;        // Поле класса для хранения значения IdTypeParameter из Tag узлов treeView

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Logging.GetInstance().WriteInLog("Успешная инициализация программы.");
                TreeFiller.GetInstance().FillTree(ref treeView);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка!\r\n " + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                Logging.GetInstance().WriteInLog(ex.Message);
                Application.Current.Shutdown();
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logging.GetInstance().WriteInLog("Успешное завершение работы программы.");
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    if (dataGridView.SelectedItems.Count == 0)
                        return;

                    var id = (dataGridView.SelectedItem as Parameter).IdParameter;
                    Parameter parameter = db.Parameters.Find(id);

                    DialogWindow dialogWindow = new DialogWindow(this);

                    dialogWindow.ParameterName = parameter.Name;
                    dialogWindow.ParameterMinValue = parameter.MinValue;
                    dialogWindow.ParameterMaxValue = parameter.MaxValue;
                    dialogWindow.ParameterDescription = parameter.Description;

                    if (dialogWindow.ShowDialog() != true)
                        return;

                    parameter.Name = dialogWindow.ParameterName;
                    parameter.MinValue = dialogWindow.ParameterMinValue;
                    parameter.MaxValue = dialogWindow.ParameterMaxValue;
                    parameter.Description = dialogWindow.ParameterDescription;

                    db.SaveChanges();
                    dataGrid_DataUpdate(db);

                    Logging.GetInstance().WriteInLog("Успешное внесение изменений в базу данных.");
                    MessageBox.Show("Изменения успешно внесены.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка!\r\n " + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logging.GetInstance().WriteInLog(ex.Message);
                    Application.Current.Shutdown();
                    return;
                }
            }
        }

        private void treeView_Selected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem treeViewItem)
            {
                using (var db = new DatabaseContext())
                {
                    _tagId = Convert.ToInt64(treeViewItem.Tag);
                    dataGrid_DataUpdate(db);
                }
            }
        }

        private void treeView_Expanded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem treeViewItem)
            {
                using (var db = new DatabaseContext())
                {
                    _tagId = Convert.ToInt64(treeViewItem.Tag);
                    dataGrid_DataUpdate(db);
                }
            }
        }

        private void dataGrid_DataUpdate(DatabaseContext db)
        {
            dataGridView.ItemsSource = null;
            dataGridView.Items.Clear();
            db.Parameters.Where(p => p.IdTypeParameter == _tagId).Load();
            dataGridView.ItemsSource = db.Parameters.Local.ToBindingList();
            dataGridView.Items.Refresh();
        }
    }
}
