namespace WebShopPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCTS")]
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            ORDER_DETAILS = new HashSet<ORDER_DETAILS>();
        }

        public int ID { get; set; }

        public int CATEGORY_ID { get; set; }

        public int BRAND_ID { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        public int? IMPORT_PRICE { get; set; }

        public int? PRICE { get; set; }

        public double? DISCOUNT { get; set; }

        [StringLength(50)]
        public string COLOR { get; set; }

        public double? SIZE { get; set; }

        [Column(TypeName = "ntext")]
        public string DESCRIPTION { get; set; }

        public int? AVAILABLE_QUANTITY { get; set; }

        public int? QUANTITY_SOLD { get; set; }

        [StringLength(50)]
        public string PRIMARY_IMAGE { get; set; }

        public virtual BRAND BRAND { get; set; }

        public virtual CATEGORy CATEGORy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAILS> ORDER_DETAILS { get; set; }
    }
}
