using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorkLab.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public Product(string Name, double? Price, string Description)
        {
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
            this.QRCode = QRGenerator.QRCoder(Name, Description, Price);
        }
        [NotMapped]
        public BitmapImage QRCode { get; set; }
    }
}
