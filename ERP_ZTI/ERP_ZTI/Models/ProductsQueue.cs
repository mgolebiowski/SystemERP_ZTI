//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_ZTI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductsQueue
    {
        public int QueueID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Amount { get; set; }
    
        public virtual Products Products { get; set; }
    }
}