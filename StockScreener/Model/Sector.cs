//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HoAssetManagement.StockScreener.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sector
    {
        public Sector()
        {
            this.Industries = new HashSet<Industry>();
        }
    
        public int Id { get; set; }
        public string Feed { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Industry> Industries { get; set; }
    }
}