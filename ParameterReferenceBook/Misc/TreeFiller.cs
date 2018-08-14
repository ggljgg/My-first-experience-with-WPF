using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using System.Windows.Media;

namespace ParameterReferenceBook
{
    class TreeFiller : ITreeFiller
    {
        private static TreeFiller _instance = null;
        private ItemsControl _parentControl;

        private TreeFiller()
        { }

        public static TreeFiller GetInstance()
        {
            if (_instance == null)
                _instance = new TreeFiller();
            return _instance;
        }

        public void FillTree(ref TreeView treeView)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    foreach (TypeParameter typeParameter in db.TypeParameters.Where(ft => ft.IdTypeParameterParent == 0))
                    {
                        TreeViewItem rootNode = new TreeViewItem
                        {
                            Header = typeParameter.TypeParameterName,
                            Tag = typeParameter.IdTypeParameter.ToString()
                        };

                        treeView.Items.Add(rootNode);
                        AddNodes(ref rootNode, typeParameter);
                    }
                    Logging.GetInstance().WriteInLog("Успешное подключение к базе данных.");
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

        #region Add, Rename, Delete for nodes
        public void AddNode(ref TreeView treeView, String header, String tag, String parentMI)
        {
            TreeViewItem node = new TreeViewItem
            {
                Header = header,
                Tag = tag
            };

            if (parentMI.Equals("addMI"))
                treeView.Items.Add(node);
            else
                (treeView.SelectedItem as TreeViewItem).Items.Add(node);

            treeView.Items.Refresh();
        }

        public void RenameNode(ref TreeView treeView, String header)
        {
            (treeView.SelectedItem as TreeViewItem).Header = header;
            treeView.Items.Refresh();
        }

        public void DeleteNode(ref TreeView treeView)
        {
            _parentControl = GetSelectedNodeParent(treeView.SelectedItem as TreeViewItem);
            _parentControl.Items.Remove(treeView.SelectedItem as TreeViewItem);
            treeView.Items.Refresh();
        }
        #endregion

        private void AddNodes(ref TreeViewItem rootNode, TypeParameter typeParameter)
        {
            using (var db = new DatabaseContext())
            {
                foreach (TypeParameter childType in db.TypeParameters.Where(ch => ch.IdTypeParameterParent == typeParameter.IdTypeParameter))
                {
                    TreeViewItem childNode = new TreeViewItem
                    {
                        Header = childType.TypeParameterName,
                        Tag = childType.IdTypeParameter.ToString()
                    };

                    rootNode.Items.Add(childNode);
                    AddNodes(ref childNode, childType);
                }
            }
        }

        private ItemsControl GetSelectedNodeParent(TreeViewItem treeViewItem)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(treeViewItem);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }
    }
}
