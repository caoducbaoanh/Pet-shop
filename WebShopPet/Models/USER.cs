namespace WebShopPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            ORDERS = new HashSet<ORDER>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

       
        public bool SEX { get; set; }

        public DateTime BIRTHDAY { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        public int? ROLE { get; set; }

        public int? STATUS { get; set; }

        [StringLength(50)]
        public string AVATAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER> ORDERS { get; set; }
    }
}
