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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StockScreenerEntities : DbContext
    {
        public StockScreenerEntities()
            : base("name=StockScreenerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<StagingStock> StagingStocks { get; set; }
    }
}
