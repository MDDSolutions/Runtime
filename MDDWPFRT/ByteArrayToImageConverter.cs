using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MDDWPFRT
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] bytes)
            {
                using (var stream = new MemoryStream(bytes))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapSource bitmapSource)
            {
                using (var stream = new MemoryStream())
                {
                    BitmapEncoder encoder = GetEncoder(bitmapSource);
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }
        private BitmapEncoder GetEncoder(BitmapSource bitmapSource)
        {
            // Default to PNG if the format is not recognized
            BitmapEncoder encoder = new PngBitmapEncoder();

            if (bitmapSource is BitmapFrame bitmapFrame)
            {
                var format = bitmapFrame.Decoder?.CodecInfo?.MimeTypes;

                switch (format)
                {
                    case "image/jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case "image/bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    case "image/gif":
                        encoder = new GifBitmapEncoder();
                        break;
                    case "image/tiff":
                        encoder = new TiffBitmapEncoder();
                        break;
                        // Add more cases as needed
                }
            }

            return encoder;
        }
    }
}
