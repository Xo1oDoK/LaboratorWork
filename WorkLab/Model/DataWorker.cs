using WorkLab.Data;
using System.Linq;
using System.Collections.Generic;

namespace WorkLab.Model
{
    public static class DataWorker
    {
        public static List<Product> GetAllProducts()
        {
            using (AplicationContext db = new AplicationContext())
            {
                var result = db.Products.ToList();
                return result;
            }
        }
        public static string AddProduct(string name, double? price, string description)
        {
            string result = "Товар уже существует";
            using (AplicationContext db = new AplicationContext())
            {
                Product product = db.Products.Where(p => p.Name == name && p.Description == description && p.Price == price).FirstOrDefault();
                if (product == null)
                {
                    Product newProduct = new Product(name, price, description);

                    db.Products.Add(newProduct);
                    db.SaveChanges();
                    result = "Товар успешно занесен в список";
                }

                return result;
            }
        }
        public static string DeleteProduct(Product oldProduct)
        {
            string result = "Товар не существует";
            using (AplicationContext db = new AplicationContext())
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == oldProduct.Id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    result = $"{product.Name} успешно удален";
                }

            }
            return result;
        }
        public static string EditProduct(Product oldProduct, string newname, string newdescription, double? newprice)
        {
            string result = "Товар не существует";
            using (AplicationContext db = new AplicationContext())
            {
                Product product = db.Products.Where(p => p.Id == oldProduct.Id).FirstOrDefault();
                if (product != null)
                {
                    product.Name = newname;
                    product.Description = newdescription;
                    product.Price = newprice;
                    db.SaveChanges();
                    result = $"{product.Name} успешно изменен";
                }
                return result;
            }

        }
    }
}
