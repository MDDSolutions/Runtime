using System.Windows;
using System.Windows.Controls;

namespace MDDWPFRT
{
    public class VerticalWrapPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double width = 0;
            double height = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                width = Math.Max(width, child.DesiredSize.Width);
                height += child.DesiredSize.Height;
            }
            return new Size(Math.Min(availableSize.Width, width), Math.Min(availableSize.Height, height));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double columnWidth = 0;
            double totalHeight = 0;
            double maxHeight = 0;

            foreach (UIElement child in InternalChildren)
            {
                if (totalHeight + child.DesiredSize.Height > finalSize.Height)
                {
                    columnWidth += maxHeight;
                    totalHeight = 0;
                    maxHeight = 0;
                }

                child.Arrange(new Rect(columnWidth, totalHeight, child.DesiredSize.Width, child.DesiredSize.Height));
                totalHeight += child.DesiredSize.Height;
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Width);
            }

            return finalSize;
        }
    } 
}
