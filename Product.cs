//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webbanhang
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderItems = new HashSet<OrderItem>();
            this.Ratings = new HashSet<Rating>();
            this.SponsoredItems = new HashSet<SponsoredItem>();
            this.Reports = new HashSet<Report>();
        }
    
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public int ProductTypeID { get; set; }
        public int BrandID { get; set; }
        public string ProductName { get; set; }
        public string Detail { get; set; }
        public int Stock { get; set; }
        public string ProductImage { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> OldPrice { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ProductType ProductType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SponsoredItem> SponsoredItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
