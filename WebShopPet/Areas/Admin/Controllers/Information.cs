using WebShopPet.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebShopPet.Areas.Admin.Controllers
{
    public class Information
    {
        public IEnumerable<ORDER_DETAILS> ORDER_DETAILS { get; set; }
        public int ID { get; internal set; }
        public int? Amount { get; internal set; }
        public int? Sell { get; internal set; }
        public IEnumerable<string> Name { get; internal set; }
        public int? Profit { get; internal set; }
        public IEnumerable<int?> Import_price { get; internal set; }
        public IEnumerable<int?> Price { get; internal set; }
    }
}