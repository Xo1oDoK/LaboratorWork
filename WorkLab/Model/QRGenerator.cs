using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorkLab.Model
{
    public class QRGenerator
    {
        public static BitmapImage QRCoder(string name, string desc, double? price)
        {
            string source = $"Товар: {name}\n Цена: {price}\n Описание: {desc}";
            Bitmap qr;
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(source, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            qr = qRCode.GetGraphic(30);
            return Converter(qr);
        }
        private static BitmapImage Converter(Bitmap saveQr)
        {
            MemoryStream memoryStream = new MemoryStream();
            ((Bitmap)saveQr).Save(memoryStream, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);
            image.StreamSource = memoryStream;
            image.EndInit();
            return image;
        }
    }
}

