//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OsCoreApplication.DataLayer.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class NewsCategory
    {
        public long id { get; set; }
        public string NewsCategoryName { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<long> UserCreated { get; set; }
        public Nullable<long> UserUpdated { get; set; }
    }
}
