using System.Windows.Controls;

namespace SanityArchiver.Application.Models
{
    class DummyTreeViewItem : TreeViewItem
    {
        public DummyTreeViewItem()
            : base()
        {
            Header = "Dummy";
            Tag = "Dummy";
        }
    }
}
