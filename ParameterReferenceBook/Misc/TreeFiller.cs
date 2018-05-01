using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace ParameterReferenceBook
{
    class TreeFiller : ITreeFiller
    {
        private static TreeFiller _instance = null;
        
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
                        TreeViewItem rootNode = new TreeViewItem() { Header = typeParameter.Name, Tag = typeParameter.IdTypeParameter.ToString() };
                        treeView.Items.Add(rootNode);
                        AddNodes(rootNode, typeParameter);
                    }
                    Logging.GetInstance().WriteInLog("Успешное подключение к базе данных.");
                }
                catch (EntityException ex)
                {
                    MessageBox.Show("Ошибка!\r\n " + ex.Message, "Подключение к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logging.GetInstance().WriteInLog(ex.Message);
                    return;
                }
            }
        }

        private void AddNodes(TreeViewItem rootNode, TypeParameter typeParameter)
        {
            using (var db = new DatabaseContext())
            {
                foreach (TypeParameter childType in db.TypeParameters.Where(ch => ch.IdTypeParameterParent == typeParameter.IdTypeParameter))
                {
                    TreeViewItem childNode = new TreeViewItem() { Header = childType.Name, Tag = childType.IdTypeParameter.ToString() };
                    rootNode.Items.Add(childNode);
                    AddNodes(childNode, childType);
                }
            }
        }
    }
}
