using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ImageManager
{
    public class ImageManager
    {
        /// <summary>
        /// Resim kırpma işlemini gerçekleştirir.
        /// </summary>
        /// <param name="imageData">Kırpma işlemi yapılacak olan resmin byte[] olarak beklenen verisi</param>
        /// <param name="x">Kırpma işlemine başlanacak olan x düzlem değeri</param>
        /// <param name="y">Kırpma işlemine başlanacak olan y boylam değeri</param>
        /// <param name="width">Kırpma işleminin x noktasından başlayarak ne kadar uzunlukta oluğu bilgisidir.</param>
        /// <param name="height">Kırpma işleminin y noktasından başlayarak ne kadar yükseklikte olduğu bilgisidir.</param>
        /// <param name="imageFormatType">Kırpma işleminden sonra geri dönülecek olan resmin hangi format da olması gerektiği bilgisidir.</param>
        public static byte[] ImageCropping(
            byte[] imageData,
            int x,
            int y,
            int width,
            int height,
            ImageFormatType imageFormatType)
        {
            ImageFormat imageFormat = GetImageFormatByImageFormatType(imageFormatType);

            return ImageCroppingWithSizing(
                imageData,
                x,
                y,
                width,
                height,
                null,
                null,
                imageFormat);
        }

        /// <summary>
        /// Resim kırpma işlemini gerçekleştirir.
        /// </summary>
        /// <param name="imageData">Kırpma işlemi yapılacak olan resmin byte[] olarak beklenen verisi</param>
        /// <param name="x">Kırpma işlemine başlanacak olan x düzlem değeri</param>
        /// <param name="y">Kırpma işlemine başlanacak olan y boylam değeri</param>
        /// <param name="width">Kırpma işleminin x noktasından başlayarak ne kadar uzunlukta oluğu bilgisidir.</param>
        /// <param name="height">Kırpma işleminin y noktasından başlayarak ne kadar yükseklikte olduğu bilgisidir.</param>
        /// <param name="maxWidth">Kırpma işleminden sonra elde edilen görselin sahip olması istenen son genişlik değeri</param>
        /// <param name="maxWidth">Kırpma işleminden sonra elde edilen görselin sahip olması istenen son yükseklik değeri</param>
        /// <param name="imageFormatType">Kırpma işleminden sonra geri dönülecek olan resmin hangi format da olması gerektiği bilgisidir.</param>
        public static byte[] ImageCropping(
            byte[] imageData,
            int x,
            int y,
            int width,
            int height,
            int maxWidth,
            int maxHeight,
            ImageFormatType imageFormatType)
        {
            ImageFormat imageFormat = GetImageFormatByImageFormatType(imageFormatType);

            return ImageCroppingWithSizing(
                imageData,
                x,
                y,
                width,
                height,
                maxWidth,
                maxHeight,
                imageFormat);
        }

        /// <summary>
        /// Resim maksimum değerini aşıyorsa ölçeklendirme işlemini yapar.
        /// </summary>
        /// <param name="imageData">Ölçeklendirilecek olan resmin byte[] olarak beklenen datası</param>
        /// <param name="maxWidth">Maksimum genişlik değeri</param>
        /// <param name="imageFormatType">Geri dönülecek olan byte[] nesnesinin hangi resim formatında olması isteniyorsa o format gönderilir.</param>
        public static byte[] ImageSizing(
            byte[] imageData,
           int maxWidth,
           ImageFormatType imageFormatType)
        {
            ImageFormat imageFormat = GetImageFormatByImageFormatType(imageFormatType);

            byte[] sizingImage = null;

            using (Stream stream = new MemoryStream(imageData))
            {
                using (Bitmap bitmap = new Bitmap(stream))
                {
                    Size newSize = new Size();
                    if (bitmap.Width > maxWidth)
                    {
                        double width = bitmap.Width;
                        double height = bitmap.Height;
                        double num3 = width / height;
                        double o = ((double)maxWidth) / num3;
                        newSize = new Size(maxWidth, o.xToIntDefault());
                    }
                    else
                    {
                        newSize = new Size(bitmap.Width, bitmap.Height);
                    }

                    using (Bitmap bitmap2 = new Bitmap(bitmap, newSize))
                    {
                        sizingImage = ImageEncodingWithBitmap(bitmap2, imageFormat);
                    }
                }
            }

            return sizingImage;
        }

        /// <summary>
        /// Kırpma işlemini yönetir.
        /// </summary>
        private static byte[] ImageCroppingWithSizing(
            byte[] imageData,
            int x,
            int y,
            int width,
            int height,
            int? maxWidth,
            int? maxHeight,
            ImageFormat imageFormat)
        {
            byte[] croppingImageData = null;

            using (Stream stream = new MemoryStream(imageData))
            {
                Image image = Image.FromStream(stream);
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.DrawImage(image, 0, 0, width, height);
                        graphics.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);

                        Size? newSize = null;

                        if (maxWidth.HasValue && maxHeight.HasValue)
                        {
                            newSize = new Size(maxWidth.Value, maxHeight.Value);
                        }
                        else
                        {
                            newSize = new Size(width, height);
                        }

                        using (Bitmap bitmap2 = new Bitmap(bitmap, newSize.Value))
                        {
                            croppingImageData = ImageEncodingWithBitmap(bitmap2, imageFormat);
                        }
                    }
                }
            }

            return croppingImageData;
        }

        /// <summary>
        /// Bitmap nesnesini image format nesnesine çevirip byte[] olarak geri döner.
        /// </summary>
        private static byte[] ImageEncodingWithBitmap(
            Bitmap bmp,
            ImageFormat imageFormat)
        {
            byte[] imageData = null;

            using (MemoryStream stream = new MemoryStream())
            {
                ImageCodecInfo info = (from codec in ImageCodecInfo.GetImageDecoders()
                                       where codec.FormatID == imageFormat.Guid
                                       select codec).FirstOrDefault<ImageCodecInfo>();

                System.Drawing.Imaging.Encoder quality = System.Drawing.Imaging.Encoder.Quality;
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    using (EncoderParameter parameter = new EncoderParameter(quality, 120L))
                    {
                        encoderParams.Param[0] = parameter;
                        bmp.Save(stream, info, encoderParams);
                    };
                };

                imageData = stream.ToArray();
            }

            return imageData;
        }

        /// <summary>
        /// Gönderilen imageFormatType bilgisinden ImageFormat nesnesi geri döner.
        /// </summary>
        private static ImageFormat GetImageFormatByImageFormatType(
            ImageFormatType imageFormatType)
        {
            ImageFormat imageFormat = null;

            switch (imageFormatType)
            {
                case ImageFormatType.Jpeg:
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case ImageFormatType.Bmp:
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ImageFormatType.Png:
                    imageFormat = ImageFormat.Png;
                    break;
            }

            return imageFormat;
        }
    }

    public enum ImageFormatType
    {
        Jpeg = 1,
        Bmp = 2,
        Png = 3
    }
}
