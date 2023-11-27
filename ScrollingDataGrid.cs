using System.Collections;
using System.Windows.Controls;

namespace ControlExtention
{
    //https://stackoverflow.com/questions/18019425/scrollintoview-for-wpf-datagrid-mvvm
    public class ScrollingDataGrid : DataGrid
    {
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            DataGrid? grid = e.Source as DataGrid;

            if (grid?.SelectedItem != null)
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem);
            }

            base.OnSelectionChanged(e);
        }

        // https://stackoverflow.com/questions/4663771/wpf-4-datagrid-getting-the-row-number-into-the-rowheader/4663799#4663799
        //protected override void OnLoadingRow(DataGridRowEventArgs e)
        //{
        //    e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        //    Trace.WriteLine(e.Row.GetIndex().ToString() + Environment.NewLine);
        //}

        protected override void OnItemsSourceChanged(
                                IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
        }
    }

}
