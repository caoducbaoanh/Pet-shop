namespace WebShopPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDERS")]
    public partial class ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDER()
        {
            ORDER_DETAILS = new HashSet<ORDER_DETAILS>();
        }

        public int ID { get; set; }

        public int USER_ID { get; set; }

        public int TOTAL_AMOUNT { get; set; }

        public int STATUS { get; set; }

        [StringLength(50)]
        public string ADDRESS { get; set; }

        [StringLength(10)]
        public string PHONE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAILS> ORDER_DETAILS { get; set; }

        public virtual USER USER { get; set; }
    }
}
