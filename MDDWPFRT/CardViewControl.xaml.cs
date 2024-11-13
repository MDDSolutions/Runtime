using MDDDataAccess;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace MDDWPFRT
{
    /// <summary>
    /// Interaction logic for CardViewControl.xaml
    /// </summary>
    public partial class CardViewControl : UserControl
    {
        public CardViewControl()
        {
            InitializeComponent();
        }
        public CardViewControl(TableViewModel viewModel) : this() => Initialize(viewModel);

        public void Initialize(TableViewModel viewModel)
        {
            DataContext = viewModel;
            if (viewModel.CurrentRow == null) return;
            viewModel.SaveChanges = () =>
            {
                switch (MessageBox.Show("Save Changes?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        return true;
                    case MessageBoxResult.No:
                        return false;
                    case MessageBoxResult.Cancel:
                    default:
                        return null;
                }
            };

            if (viewModel.ColumnList.Count() > 10)
            {
                //this.MinWidth = 800;
                //this.MinHeight = 575;
            }

            //this.MinHeight = 800;
            //this.MinWidth = 900;

            //foreach (var column in viewModel.ColumnList)
            //{
            //    StackPanel fieldPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            //    Label label = new Label { Content = column.Name, Width = 100 };
            //    TextBox textBox = new TextBox { Width = 200 };
            //    textBox.TextChanged += async (s, e) => await viewModel.DirtyCheck();

            //    Binding binding = new Binding($"CurrentRow[{column.Name}]")
            //    {
            //        Source = viewModel,
            //        Mode = BindingMode.TwoWay,
            //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //    };
            //    textBox.SetBinding(TextBox.TextProperty, binding);
            //    textBox.IsReadOnly = column.IsIdentity || (column.IsPrimaryKey && column.HasDefault);

            //    fieldPanel.Children.Add(label);
            //    fieldPanel.Children.Add(textBox);
            //    CardPanel.Children.Add(fieldPanel);
            //}
            foreach (var column in viewModel.ColumnList)
            {
                StackPanel fieldPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                Label label = new Label { Content = column.Name, Width = 100 };

                Control control;

                if (column.DataType == "date") // || column.DataType == "datetime")
                {
                    control = new DatePicker();
                    Binding binding = new Binding($"CurrentRow[{column.Name}]")
                    {
                        Source = viewModel,
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    control.SetBinding(DatePicker.SelectedDateProperty, binding);
                }
                else if (column.DataType == "varbinary" && column.Name.ToLower().Contains("image"))
                {
                    var imgctl = new Image { Width = 200, Height = 200 };
                    Binding binding = new Binding($"CurrentRow[{column.Name}]")
                    {
                        Source = viewModel,
                        Mode = BindingMode.OneWay,
                        Converter = new ByteArrayToImageConverter() // You need to implement this converter
                    };
                    imgctl.SetBinding(Image.SourceProperty, binding);
                    control = new ContentControl { Content = imgctl };
                }
                else
                {
                    var txtbox = new TextBox { Width = 200 };
                    txtbox.TextChanged += async (s, e) => await viewModel.DirtyCheck();

                    Binding binding = new Binding($"CurrentRow[{column.Name}]")
                    {
                        Source = viewModel,
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    txtbox.SetBinding(TextBox.TextProperty, binding);
                    txtbox.IsReadOnly = column.IsIdentity || (column.IsPrimaryKey && column.HasDefault);
                    control = txtbox;
                }

                fieldPanel.Children.Add(label);
                fieldPanel.Children.Add(control);
                CardPanel.Children.Add(fieldPanel);
            }

        }
        protected override Size MeasureOverride(Size constraint)
        {
            var size = base.MeasureOverride(constraint);
            if (size.Height > 800)
            {
                return new Size(constraint.Width, 800);
            }
            return size;
        }
    }
}
