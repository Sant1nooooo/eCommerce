using Server.Application.Models;

namespace Server.Core
{
    public class DataTransferObjects
    {
        public class ProductWithSubImages
        {
            public Product? Product { get; set; }
            public IEnumerable<ProductWithSubImages>? SubImages { get; set; }
        }
    }
}
