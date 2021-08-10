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
    
    public partial class Industry
    {
        public Industry()
        {
            this.Stocks = new HashSet<Stock>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> PriceChange { get; set; }
        public Nullable<decimal> MarketCapitalization { get; set; }
        public Nullable<decimal> PE { get; set; }
        public Nullable<decimal> ROE { get; set; }
        public Nullable<decimal> DividendYield { get; set; }
        public Nullable<decimal> DebtToEquityRatio { get; set; }
        public Nullable<decimal> PriceBook { get; set; }
        public Nullable<decimal> NetProfitMargin { get; set; }
        public Nullable<decimal> PriceToFreeCashFlow { get; set; }
        public Nullable<int> SectorId { get; set; }
        public string SectorFeed { get; set; }
    
        public virtual Sector Sector { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
