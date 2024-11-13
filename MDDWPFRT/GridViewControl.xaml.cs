using MDDDataAccess;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace MDDWPFRT
{
    public partial class GridViewControl : UserControl
    {
        public GridViewControl()
        {
            InitializeComponent();
        }

        public GridViewControl(TableViewModel viewModel) : this() => Initialize(viewModel);

        public void Initialize(TableViewModel viewModel)
        {
            DataContext = viewModel;
            dataGrid.ItemsSource = viewModel.CurrentRow?.DataView;

            //viewModel.SaveChanges = () =>
            //{
            //    switch (MessageBox.Show("Save Changes?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            //    {
            //        case MessageBoxResult.Yes:
            //            return true;
            //        case MessageBoxResult.No:
            //            return false;
            //        case MessageBoxResult.Cancel:
            //        default:
            //            return null;
            //    }
            //};

            foreach (var column in viewModel.ColumnList)
            {
                var binding = new Binding($"[{column.Name}]")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                var dataGridColumn = new DataGridTextColumn
                {
                    Header = column.Name,
                    Binding = binding,
                    IsReadOnly = column.IsIdentity || (column.IsPrimaryKey && column.HasDefault)
                };

                dataGrid.Columns.Add(dataGridColumn);
            }
        }
        private bool isCancellingEdit = false;
        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (isCancellingEdit) return;
            if (DataContext is TableViewModel viewModel)
            {
                //Debug.WriteLine($"RowEditEnding: {viewModel}, {viewModel.CurrentRow[1]}, Row Index: {dataGrid.SelectedIndex}");
                switch (MessageBox.Show("Save Changes?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        e.Row.Header = (dataGrid.SelectedIndex + 1).ToString();
                        return;
                    case MessageBoxResult.No:
                        viewModel.CurrentRow?.CancelEdit();
                        isCancellingEdit = true;
                        dataGrid.CancelEdit();
                        isCancellingEdit = false;
                        e.Row.Header = (dataGrid.SelectedIndex + 1).ToString();
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        e.Cancel = true;
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            dataGrid.SelectedItem = e.Row.Item;
                            dataGrid.ScrollIntoView(e.Row.Item);
                            var cell = dataGrid.Columns[0].GetCellContent(e.Row)?.Parent as DataGridCell;
                            cell?.Focus();
                        }), DispatcherPriority.Background);
                        break;
                }
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TableViewModel viewModel)
            {
                if (dataGrid.SelectedItem is DataRowView selectedRow)
                {
                    viewModel.CurrentRowIndex = dataGrid.SelectedIndex + 1;
                    //Debug.WriteLine($"SelectionChanged: {viewModel}, {viewModel.CurrentRow[1]}, Row Index: {dataGrid.SelectedIndex}");
                }
                else
                {
                    throw new Exception("Invalid selection");
                }
            }
            else
            {
                throw new Exception("Invalid DataContext");
            }
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Row.Header = ">>";
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
