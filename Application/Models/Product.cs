using System.ComponentModel.DataAnnotations;

namespace Server.Application.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDetails { get; set; }
        public string? ImageURL { get; set; }
        public string? Type { get; set; }
        public int Price { get; set; }
        public int UnitSold { get; set; }
        public bool ShowProduct { get; set; }

        public Product(string ProductName, string ProductDetails, string Type, string ImageURL, int Price)
        {
            this.ProductName = ProductName;
            this.ProductDetails = ProductDetails;
            this.Type = Type;
            this.ImageURL = ImageURL;
            this.Price = Price;
            UnitSold = 0;
            ShowProduct = true;
        }
    }
}
