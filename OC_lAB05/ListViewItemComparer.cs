using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OC_lAB05_PCB
{
    class ListViewItemComparer : IComparer
    {
        private int _columnTndex;
        public int ColumnIndex
        {
            get
            {
                return _columnTndex;
            }
            set
            {
                _columnTndex = value;
            }
        }
        private SortOrder _sortDirection;
        public SortOrder SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
            }
        }
        public ListViewItemComparer()
        {
            _sortDirection = SortOrder.None;
        }
        public int Compare(object x, object y)
        {
            ListViewItem listViewItemX = x as ListViewItem;
            ListViewItem listViewItemY = y as ListViewItem;
            int result;
            switch (_columnTndex)
            {
                case 0:
                    result = string.Compare(listViewItemX.SubItems[_columnTndex].Text,
                        listViewItemY.SubItems[_columnTndex].Text, false);
                    break;
                case 1:
                    double valueX = double.Parse(listViewItemX.SubItems[_columnTndex].Text);
                    double valueY = double.Parse(listViewItemY.SubItems[_columnTndex].Text);
                    result = valueX.CompareTo(valueY);
                    break;
                default:
                    result = string.Compare(listViewItemX.SubItems[_columnTndex].Text,
                        listViewItemY.SubItems[_columnTndex].Text, false);
                    break;
            }
            if (_sortDirection == SortOrder.Descending)
            {
                return -result;
            }
            else
            {
                return result;
            }
        }
    }
}
