using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using HoAssetManagement.StockScreener.Modeling;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace HoAssetManagement.StockScreener.Entity
{

    
    //<?xml version="1.0" encoding="UTF-8"?>
    //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
    //    yahoo:count="1" yahoo:created="2012-06-29T15:51:46Z" yahoo:lang="en-US">
    //    <results>
    //        <stock symbol="yhoo">
    //            <CompanyName/>
    //            <start>1996-04-12</start>
    //            <end>2012-06-29</end>
    //            <Sector>Technology</Sector>
    //            <Industry>Internet Information Providers</Industry>
    //            <FullTimeEmployees>14100</FullTimeEmployees>
    //        </stock>
    //    </results>
    //</query    
    class Stock
    {
        /*
        [XmlAttribute(AttributeName = "symbol")]
        public string Symbol;
        public string CompanyName;
        public DateTime? start;
        public DateTime? end;
        public string Sector;
        public string Industry;
        public string FullTimeEmployees;
        */

        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const string COMPANY_OF_A_INDUSTRY = @"http://query.yahooapis.com/v1/public/yql?q=
                select%20*%20from%20yahoo.finance.industry%20where%20id%3D%22{0}%22
                &env=http%3A%2F%2Fdatatables.org%2Falltables.env";
        
        # region static method
        public static ObservableCollection<Model.Stock> Fetch(int industryId)
        {
            XDocument doc = XDocument.Load(string.Format(COMPANY_OF_A_INDUSTRY, industryId));
            ObservableCollection<Model.Stock> stocks = new ObservableCollection<Model.Stock>();
            foreach (XElement s in doc.Descendants("company"))
            {
                Model.Stock stock= new Model.Stock();
                stock.Symbol = s.Attribute("symbol").Value;
                stock.Name = s.Attribute("name").Value;
                stock.IndustryId = industryId;
                stocks.Add(stock);
            }
            return stocks;
        }

        public static void CleanDbTables()
        {
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                db.Database.ExecuteSqlCommand("truncate table stocks");
            }
        }

        public static void WriteDbTables(IEnumerable<Model.Stock> stocks)
        {
            if (stocks == null || stocks.Count() <=0) return;
            log.Debug("count > 0");
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {
                    foreach (Model.Stock s in stocks)
                    {
                        ChangeEntityValue(db, s);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Error("WriteDbTable", ex);
                }
            }

        }

        static public void ChangeEntityValue(StockScreener.Model.StockScreenerEntities db, Model.Stock s)
        {
            log.Debug("stock:"+s.Symbol);
            var v = db.Stocks.Find(s.Symbol);
            if (v == null)
            {
                db.Stocks.Add(s);
            }
            else
            {
#if EXISTING_ENTITY_UPDATE
                db.Entry(v).CurrentValues.SetValues(s);
#endif

            }
        }

        public static void WriteXhtml(string fileName)
        {
            XDocument doc = XDocument.Load(COMPANY_OF_A_INDUSTRY);
            doc.Save(fileName);
        }
        #endregion static region
    


    }


    //        <?xml version="1.0" encoding="UTF-8"?>
    //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
    //    yahoo:count="1" yahoo:created="2012-06-28T19:10:56Z" yahoo:lang="en-US">
    //    <results>
    //        <stock symbol="YHOO">
    //            <ReturnOnEquity>8.75%</ReturnOnEquity>
    //            <Stockholders>12,541,067</Stockholders>
    //            <TotalAssets>14,782,786</TotalAssets>
    //            <TrailingPE>17.38</TrailingPE>
    //            <Analysts/>
    //            <EarningsGrowth>28.40%</EarningsGrowth>
    //            <EbitMarge>29.06%</EbitMarge>
    //            <LastYear>14.87</LastYear>
    //            <SixMonths>&lt;p&gt;15.41&lt;/p&gt;</SixMonths>
    //            <Today/>
    //            <LastMonth>15.24</LastMonth>
    //            <TwoMonthsAgo>15.54</TwoMonthsAgo>
    //            <ThreeMonthsAgo>15.22</ThreeMonthsAgo>
    //            <FourMonthsAgo>14.90</FourMonthsAgo>
    //            <CompanyName/>
    //        </stock>
    //    </results>
    //</query>
    class Quant
    {
        public decimal? ReturnOnEquity;
        public int Stockholders;
        public int TotalAssets;
        public decimal? TrailingPE;
        public string Analysts;
        public decimal? EarningsGrowth;
        public decimal? EbitMarge;
        public decimal? LastYear;
        public string SixMonths;
        public decimal? Today;
        public decimal? LastMonth;
        public decimal? TwoMonthsAgo;
        public decimal? ThreeMonthsAgo;
        public decimal? FourMonthsAgo;
    }

}
