using System.Windows.Controls;

namespace ParameterReferenceBook
{
    interface ITreeFiller
    {
        void FillTree(ref TreeView treeView);
    }
}
