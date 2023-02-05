namespace WebShopPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ORDER_DETAILS
    {
        public int ID { get; set; }

        public int ORDER_ID { get; set; }

        public int? QUANTITY { get; set; }

        public int? PRODUCT_PRICE { get; set; }

        public int PRODUCT_ID { get; set; }

        public virtual ORDER ORDER { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
