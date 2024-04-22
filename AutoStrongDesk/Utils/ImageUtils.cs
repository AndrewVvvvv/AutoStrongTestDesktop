using System.IO;
using System.Windows.Media.Imaging;

namespace AutoStrongDesk.Utils
{
    public static class ImageUtils
    {
        public static async Task<byte[]> ImageToBytes(BitmapImage imageSource)
        {
            try
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSource));

                await using var memoryStream = new MemoryStream();
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
