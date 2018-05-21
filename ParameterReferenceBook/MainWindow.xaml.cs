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
                MessageBox.Show("Ошибка!\r\n" + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                Logging.GetInstance().WriteInLog(ex.Message);
                Application.Current.Shutdown();
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logging.GetInstance().WriteInLog("Успешное завершение работы программы.");
        }

        #region Insert, Update, Delete for parameters
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    if (treeView.SelectedItem == null)
                    {
                        MessageBox.Show("Предупреждение!\r\nНе выбран ни один узел в древовидном списке. Для добавления параметра необходимо выбрать узел.", "Добавление параметра", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    DialogWindow dialogWindow = new DialogWindow(this, (sender as Button).Name);

                    if (dialogWindow.ShowDialog() != true)
                        return;

                    Parameter parameter = new Parameter
                    {
                        Name = dialogWindow.ParameterName,
                        IdTypeParameter = Convert.ToInt64((treeView.SelectedItem as TreeViewItem).Tag),
                        MinValue = dialogWindow.ParameterMinValue,
                        MaxValue = dialogWindow.ParameterMaxValue,
                        Description = dialogWindow.ParameterDescription
                    };

                    db.Parameters.Add(parameter);
                    db.SaveChanges();
                    dataGrid_DataUpdate(db);

                    Logging.GetInstance().WriteInLog("Успешное внесение параметра " + parameter.Name + "в базу данных.");
                    MessageBox.Show("Данные успешно внесены.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка!\r\n" + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logging.GetInstance().WriteInLog(ex.Message);
                    Application.Current.Shutdown();
                    return;
                }
            }
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
                    Parameter parameter = db.Parameters.Find(id);   // возможно, в дальнейшем потребуется проверка parameter на null
                    
                    DialogWindow dialogWindow = new DialogWindow(this, (sender as Button).Name);

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

                    Logging.GetInstance().WriteInLog("Успешное внесение изменений параметра " + parameter.Name + " в базу данных.");
                    MessageBox.Show("Изменения успешно внесены.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка!\r\n" + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logging.GetInstance().WriteInLog(ex.Message);
                    Application.Current.Shutdown();
                    return;
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    if (dataGridView.SelectedItems.Count == 0)
                        return;

                    var id = (dataGridView.SelectedItem as Parameter).IdParameter;
                    Parameter parameter = db.Parameters.Find(id);   // возможно, в дальнейшем потребуется проверка parameter на null

                    db.Parameters.Remove(parameter);
                    db.SaveChanges();
                    dataGrid_DataUpdate(db);

                    Logging.GetInstance().WriteInLog("Успешное удаление параметра " + parameter.Name + " из базы данных.");
                    MessageBox.Show("Данные успешно удалены.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка!\r\n" + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logging.GetInstance().WriteInLog(ex.Message);
                    Application.Current.Shutdown();
                    return;
                }
            }
        }
        #endregion

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
