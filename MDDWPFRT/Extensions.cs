
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace MDDWPFRT
{
    public static class Extensions
    {
        public static Window ToWindow(this UserControl userControl, string title = "Window")
        {
            userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            userControl.VerticalAlignment = VerticalAlignment.Stretch;

            userControl.Measure(new Size(1500, 500));
            Debug.WriteLine($"Desired Size: {userControl.DesiredSize}");


            Window window = new Window
            {
                Title = title,
                Content = userControl,
                SizeToContent = SizeToContent.Manual,
                ResizeMode = ResizeMode.CanResize,
                Width = userControl.DesiredSize.Width + 50,
                Height = userControl.DesiredSize.Height + 50
                //Width = Double.IsNaN(userControl.Width) ? userControl.MinWidth : userControl.Width,
                //Height = Double.IsNaN(userControl.Height) ? userControl.MinHeight : userControl.Width,
                //WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
#if DEBUG
            window.SizeChanged += (s, e) =>
            {
                Debug.WriteLine($"Width: {window.Width}, Height: {window.Height}");
            };
#endif

            window.UpdateLayout();
            return window;
        }
    }

}
