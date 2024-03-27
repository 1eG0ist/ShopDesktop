using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia.Media.Imaging;
using System.IO;

public class ByteArrayToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value is byte[] byteArray)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    Bitmap bitmap = new Bitmap(stream);
                    return bitmap;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to convert byte[] to image: {ex.Message}");
        }
        
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}