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
    [DataContract()]
    public class Quote : INotifyPropertyChanged
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Quote(string ticker)
        {
            symbol = ticker;
        }

        #region data member 
        public event PropertyChangedEventHandler PropertyChanged;

        private string symbol;
        private decimal? averageDailyVolume;
        private decimal? bid;
        private decimal? ask;
        private decimal? bookValue;
        private decimal? changePercent;
        private decimal? change;
        private decimal? dividendShare;
        private DateTime? lastTradeDate;
        private decimal? earningsShare;
        private decimal? epsEstimateCurrentYear;
        private decimal? epsEstimateNextYear;
        private decimal? epsEstimateNextQuarter;
        private decimal? dailyLow;
        private decimal? dailyHigh;
        private decimal? yearlyLow;
        private decimal? yearlyHigh;
        private decimal? marketCapitalization;
        private decimal? ebitda;
        private decimal? changeFromYearLow;
        private decimal? percentChangeFromYearLow;
        private decimal? changeFromYearHigh;
        private decimal? percentChangeFromYearHigh;
        private decimal? lastTradePrice;
        private decimal? fiftyDayMovingAverage;
        private decimal? twoHunderedDayMovingAverage;
        private decimal? changeFromTwoHundredDayMovingAverage;
        private decimal? percentChangeFromFiftyDayMovingAverage;
        private string name;
        private decimal? open;
        private decimal? previousClose;
        private decimal? changeInPercent;
        private decimal? priceSales;
        private decimal? priceBook;
        private DateTime? exDividendDate;
        private decimal? pegRatio;
        private decimal? priceEpsEstimateCurrentYear;
        private decimal? priceEpsEstimateNextYear;
        private decimal? shortRatio;
        private decimal? oneYearPriceTarget;
        private decimal? dividendYield;
        private DateTime? dividendPayDate;
        private decimal? percentChangeFromTwoHundredDayMovingAverage;
        private decimal? peRatio;
        private decimal? volume;
        private string stockExchange;
        private DateTime lastUpdate;


        public T Get<T>(string propertyName)
        {
            return (T)new object();
        }
        public void Set<T>(string propertyName, T value)
        {
                //lastUpdate = (DateTime)value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [DataMember]
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set
            {
                lastUpdate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastUpdate"));
            }
        }

        [DataMember]
        public string StockExchange
        {
            get { return stockExchange; }
            set
            {
                stockExchange = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("StockExchange"));
            }
        }

        [DataMember]
        public decimal? Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Volume"));
            }
        }

        [DataMember]
        public decimal? PeRatio
        {
            get { return peRatio; }
            set
            {
                peRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PeRatio"));
            }
        }

        [DataMember]
        public decimal? PercentChangeFromTwoHundredDayMovingAverage
        {
            get { return percentChangeFromTwoHundredDayMovingAverage; }
            set
            {
                percentChangeFromTwoHundredDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromTwoHundredDayMovingAverage"));
            }
        }

        [DataMember]
        public DateTime? DividendPayDate
        {
            get { return dividendPayDate; }
            set
            {
                dividendPayDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendPayDate"));
            }
        }

        [DataMember]
        public decimal? DividendYield
        {
            get { return dividendYield; }
            set
            {
                dividendYield = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendYield"));
            }
        }


        [DataMember]
        public decimal? OneYearPriceTarget
        {
            get { return oneYearPriceTarget; }
            set
            {
                oneYearPriceTarget = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OneYearPriceTarget"));
            }
        }

        [DataMember]
        public decimal? ShortRatio
        {
            get { return shortRatio; }
            set
            {
                shortRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ShortRatio"));
            }
        }

        [DataMember]
        public decimal? PriceEpsEstimateNextYear
        {
            get { return priceEpsEstimateNextYear; }
            set
            {
                priceEpsEstimateNextYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceEpsEstimateNextYear"));
            }
        }

        [DataMember]
        public decimal? PriceEpsEstimateCurrentYear
        {
            get { return priceEpsEstimateCurrentYear; }
            set
            {
                priceEpsEstimateCurrentYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceEpsEstimateCurrentYear"));
            }
        }

        [DataMember]
        public decimal? PegRatio
        {
            get { return pegRatio; }
            set
            {
                pegRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PegRatio"));
            }
        }

        [DataMember]
        public DateTime? ExDividendDate
        {
            get { return exDividendDate; }
            set
            {
                exDividendDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ExDividendDate"));
            }
        }

        [DataMember]
        public decimal? PriceBook
        {
            get { return priceBook; }
            set
            {
                priceBook = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceBook"));
            }
        }

        [DataMember]
        public decimal? PriceSales
        {
            get { return priceSales; }
            set
            {
                priceSales = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceSales"));
            }
        }

        [DataMember]
        public decimal? ChangeInPercent
        {
            get { return changeInPercent; }
            set
            {
                changeInPercent = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeInPercent"));
            }
        }

        [DataMember]
        public decimal? PreviousClose
        {
            get { return previousClose; }
            set
            {
                previousClose = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PreviousClose"));
            }
        }

        [DataMember]
        public decimal? Open
        {
            get { return open; }
            set
            {
                open = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Open"));
            }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        [DataMember]
        public decimal? PercentChangeFromFiftyDayMovingAverage
        {
            get { return percentChangeFromFiftyDayMovingAverage; }
            set
            {
                percentChangeFromFiftyDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromFiftyDayMovingAverage"));
            }
        }

        [DataMember]
        public decimal? ChangeFromTwoHundredDayMovingAverage
        {
            get { return changeFromTwoHundredDayMovingAverage; }
            set
            {
                changeFromTwoHundredDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromTwoHundredDayMovingAverage"));
            }
        }

        [DataMember]
        public decimal? TwoHunderedDayMovingAverage
        {
            get { return twoHunderedDayMovingAverage; }
            set
            {
                twoHunderedDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TwoHunderedDayMovingAverage"));
            }
        }

        [DataMember]
        public decimal? FiftyDayMovingAverage
        {
            get { return fiftyDayMovingAverage; }
            set
            {
                fiftyDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FiftyDayMovingAverage"));
            }
        }

        [DataMember]
        public decimal? LastTradePrice
        {
            get { return lastTradePrice; }
            set
            {
                lastTradePrice = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastTradePrice"));
            }
        }

        [DataMember]
        public decimal? PercentChangeFromYearHigh
        {
            get { return percentChangeFromYearHigh; }
            set
            {
                percentChangeFromYearHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromYearHigh"));
            }
        }

        [DataMember]
        public decimal? ChangeFromYearHigh
        {
            get { return changeFromYearHigh; }
            set
            {
                changeFromYearHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromYearHigh"));
            }
        }

        [DataMember]
        public decimal? PercentChangeFromYearLow
        {
            get { return percentChangeFromYearLow; }
            set
            {
                percentChangeFromYearLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromYearLow"));
            }
        }

        [DataMember]
        public decimal? ChangeFromYearLow
        {
            get { return changeFromYearLow; }
            set
            {
                changeFromYearLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromYearLow"));
            }
        }

        [DataMember]
        public decimal? EBITDA
        {
            get { return ebitda; }
            set
            {
                ebitda = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Ebitda"));
            }
        }

        [DataMember]
        public decimal? MarketCapitalization
        {
            get { return marketCapitalization; }
            set
            {
                marketCapitalization = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MarketCapitalization"));
            }
        }

        [DataMember]
        public decimal? YearlyHigh
        {
            get { return yearlyHigh; }
            set
            {
                yearlyHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("YearlyHigh"));
            }
        }

        [DataMember]
        public decimal? YearlyLow
        {
            get { return yearlyLow; }
            set
            {
                yearlyLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("YearlyLow"));
            }
        }

        [DataMember]
        public decimal? DailyHigh
        {
            get { return dailyHigh; }
            set
            {
                dailyHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DailyHigh"));
            }
        }

        [DataMember]
        public decimal? DailyLow
        {
            get { return dailyLow; }
            set
            {
                dailyLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DailyLow"));
            }
        }

        [DataMember]
        public decimal? EpsEstimateNextQuarter
        {
            get { return epsEstimateNextQuarter; }
            set
            {
                epsEstimateNextQuarter = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateNextQuarter"));
            }
        }

        [DataMember]
        public decimal? EpsEstimateNextYear
        {
            get { return epsEstimateNextYear; }
            set
            {
                epsEstimateNextYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateNextYear"));
            }
        }

        [DataMember]
        public decimal? EpsEstimateCurrentYear
        {
            get { return epsEstimateCurrentYear; }
            set
            {
                epsEstimateCurrentYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateCurrentYear"));
            }
        }

        [DataMember]
        public decimal? EarningsShare
        {
            get { return earningsShare; }
            set
            {
                earningsShare = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EarningsShare"));
            }
        }

        [DataMember]
        public DateTime? LastTradeDate
        {
            get { return lastTradeDate; }
            set
            {
                lastTradeDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastTradeDate"));
            }
        }

        [DataMember]
        public decimal? DividendShare
        {
            get { return dividendShare; }
            set
            {
                dividendShare = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendShare"));
            }
        }

        [DataMember]
        public decimal? Change
        {
            get { return change; }
            set
            {
                change = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Change"));
            }
        }

        [DataMember]
        public decimal? ChangePercent
        {
            get { return changePercent; }
            set
            {
                changePercent = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangePercent"));
            }
        }

        [DataMember]
        public decimal? BookValue
        {
            get { return bookValue; }
            set
            {
                bookValue = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BookValue"));
            }
        }

        [DataMember]
        public decimal? Ask
        {
            get { return ask; }
            set
            {
                ask = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Ask"));
            }
        }

        [DataMember]
        public decimal? Bid
        {
            get { return bid; }
            set
            {
                bid = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Bid"));
            }
        }

        [DataMember]
        public decimal? AverageDailyVolume
        {
            get { return averageDailyVolume; }
            set
            {
                averageDailyVolume = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AverageDailyVolume"));
            }
        }

        [DataMember]
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Symbol"));
            }
        }
        #endregion data member

        # region static method

        protected const string STOCK_REALTIME_QUOTE = @"http://query.yahooapis.com/v1/public/yql?q=
                select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})
                &env=http%3A%2F%2Fdatatables.org%2Falltables.env";

        public static ObservableCollection<Quote> Fetch(string[] symbols)
        {
            string symbolList = String.Join("%2C", symbols.Select(w => "%22" + w + "%22").ToArray());
            string url = string.Format(STOCK_REALTIME_QUOTE, symbolList);
            XDocument doc = XDocument.Load(url);
            return Parse(doc);
        }



        private static ObservableCollection<Quote> Parse(XDocument doc)
        {
            ObservableCollection<Quote> quotes = new ObservableCollection<Quote>();

            foreach (XElement q in doc.Descendants("quote"))
            {
                Quote quote = new Quote(q.Attribute("symbol").Value);
                quote.Ask = Utility.GetDecimal(q.Element("Ask").Value);
                quote.Bid = Utility.GetDecimal(q.Element("Bid").Value);
                quote.AverageDailyVolume = Utility.GetDecimal(q.Element("AverageDailyVolume").Value);
                quote.BookValue = Utility.GetDecimal(q.Element("BookValue").Value);
                quote.Change = Utility.GetDecimal(q.Element("Change").Value);
                quote.DividendShare = Utility.GetDecimal(q.Element("DividendShare").Value);
                quote.LastTradeDate = Utility.GetDateTime(q.Element("LastTradeDate").Value + " " + q.Element("LastTradeTime").Value);
                quote.EarningsShare = Utility.GetDecimal(q.Element("EarningsShare").Value);
                quote.EpsEstimateCurrentYear = Utility.GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
                quote.EpsEstimateNextYear = Utility.GetDecimal(q.Element("EPSEstimateNextYear").Value);
                quote.EpsEstimateNextQuarter = Utility.GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
                quote.DailyLow = Utility.GetDecimal(q.Element("DaysLow").Value);
                quote.DailyHigh = Utility.GetDecimal(q.Element("DaysHigh").Value);
                quote.YearlyLow = Utility.GetDecimal(q.Element("YearLow").Value);
                quote.YearlyHigh = Utility.GetDecimal(q.Element("YearHigh").Value);
                quote.MarketCapitalization = Utility.GetDecimal(q.Element("MarketCapitalization").Value);
                quote.EBITDA = Utility.GetDecimal(q.Element("EBITDA").Value);
                quote.ChangeFromYearLow = Utility.GetDecimal(q.Element("ChangeFromYearLow").Value);
                quote.PercentChangeFromYearLow = Utility.GetDecimal(q.Element("PercentChangeFromYearLow").Value);
                quote.ChangeFromYearHigh = Utility.GetDecimal(q.Element("ChangeFromYearHigh").Value);
                quote.LastTradePrice = Utility.GetDecimal(q.Element("LastTradePriceOnly").Value);
                quote.PercentChangeFromYearHigh = Utility.GetDecimal(q.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
                quote.FiftyDayMovingAverage = Utility.GetDecimal(q.Element("FiftydayMovingAverage").Value);
                quote.TwoHunderedDayMovingAverage = Utility.GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
                quote.ChangeFromTwoHundredDayMovingAverage = Utility.GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromTwoHundredDayMovingAverage = Utility.GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromFiftyDayMovingAverage = Utility.GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
                quote.Name = q.Element("Name").Value;
                quote.Open = Utility.GetDecimal(q.Element("Open").Value);
                quote.PreviousClose = Utility.GetDecimal(q.Element("PreviousClose").Value);
                quote.ChangeInPercent = Utility.GetDecimal(q.Element("ChangeinPercent").Value);
                quote.PriceSales = Utility.GetDecimal(q.Element("PriceSales").Value);
                quote.PriceBook = Utility.GetDecimal(q.Element("PriceBook").Value);
                quote.ExDividendDate = Utility.GetDateTime(q.Element("ExDividendDate").Value);
                quote.PeRatio = Utility.GetDecimal(q.Element("PERatio").Value);
                quote.DividendPayDate = Utility.GetDateTime(q.Element("DividendPayDate").Value);
                quote.PegRatio = Utility.GetDecimal(q.Element("PEGRatio").Value);
                quote.PriceEpsEstimateCurrentYear = Utility.GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
                quote.PriceEpsEstimateNextYear = Utility.GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);
                quote.ShortRatio = Utility.GetDecimal(q.Element("ShortRatio").Value);
                quote.OneYearPriceTarget = Utility.GetDecimal(q.Element("OneyrTargetPrice").Value);
                quote.Volume = Utility.GetDecimal(q.Element("Volume").Value);
                quote.StockExchange = q.Element("StockExchange").Value;
                quote.LastUpdate = DateTime.Now;

                quotes.Add(quote);
            }
            return quotes;
        }


        public static void CleanDbTables()
        {
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                db.Database.ExecuteSqlCommand("truncate table quotes");
            }
        }


        public static System.Collections.Generic.IList<Model.Quote> FilterDbTables(string templatePath)
        {
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.Load(templatePath);
#if DEBUG
                    string sql = string.Empty;
#else
                    string sql = "select symbol from quote where RecordDate >= '" + DateTime.Today.ToShortDateString() + "' and ";
#endif
                    if (Convert.ToBoolean(xmlDoc.SelectSingleNode("ScreeningCriteria/Basic").Attributes[0].Value))
                    {
                        foreach (System.Xml.XmlNode node in xmlDoc.SelectSingleNode("ScreeningCriteria/Basic").ChildNodes)
                        {
                            if (node.NodeType == System.Xml.XmlNodeType.Comment) continue;
                            decimal low = string.IsNullOrWhiteSpace(node.Attributes[0].Value) ? 0 : Convert.ToDecimal(node.Attributes[0].Value);
                            decimal high = string.IsNullOrWhiteSpace(node.Attributes[1].Value) ? 0 : Convert.ToDecimal(node.Attributes[1].Value);
                            if (low > 0) sql += string.Format(" {0}>={1} and ", node.Name, low);
                            if (high > 0) sql += string.Format(" {0}<={1} and ", node.Name, high);
                        }
                    }

                    if (Convert.ToBoolean(xmlDoc.SelectSingleNode("ScreeningCriteria/Derived").Attributes[0].Value))
                    {
                        //var fields = typeof(Model.Quote).GetProperties();
                        foreach (System.Xml.XmlNode node in xmlDoc.SelectSingleNode("ScreeningCriteria/Derived").ChildNodes)
                        {
                            if (node.NodeType == System.Xml.XmlNodeType.Comment) continue;

                            string exp = string.IsNullOrWhiteSpace(node.Attributes[2].Value) ? null : node.Attributes[2].Value; ;
                            if (exp == null) continue;

                            /*
                            ParameterExpression[] ps = (from f in fields
                                     where exp.Contains(f.Name)
                                     select Expression.Parameter(f.PropertyType, f.Name)
                                ).ToArray();
                            var e = System.Linq.Dynamic.DynamicExpression.ParseLambda(ps, typeof(bool), exp);
                            var result = e.Compile().DynamicInvoke(9, 4, 2);
                             */

                            sql += string.Format(" ({0}) and ", exp);
                        }
                    }

                    if (sql != string.Empty)
                    {
                        sql = "select * from quotes where " + sql.Substring(0, sql.Length - 4);
                        System.Data.Entity.Infrastructure.DbSqlQuery<Model.Quote> vs = db.Quotes.SqlQuery(sql);
                        return vs.ToList<Model.Quote>();
                    }

                }
                catch (Exception ex)
                {
                    log.Error("FilterTable", ex);
                }
                return new System.Collections.Generic.List<Model.Quote>();
            }
       }
 
        public static void WriteDbTables(IEnumerable<Model.Quote> quotes)
        {
            if (quotes == null) return;
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {
                    foreach (Model.Quote q in quotes)
                    {

                        var v = db.Quotes.Find(q.Symobl, q.RecordDate);
                        if (v == null)
                        {
                            db.Quotes.Add(q);
                        }
                        else
                        {
#if EXISTING_ENTITY_UPDATE
                            db.Entry(v).CurrentValues.SetValues(q);
                            db.Entry(v).State = System.Data.EntityState.Modified;
#endif
                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Error("WriteTable", ex);
                }
            }
        
        }


        class ViewQuote
        {
            public string Name { get; set; }
            public string Symobl { get; set; }
            public Nullable<decimal> PercentChangeFromTwoHundreddayMovingAverage { get; set; }
            public Nullable<decimal> PercentChangeFromFiftydayMovingAverage { get; set; }
            public Nullable<decimal> PriceSales { get; set; }
            public Nullable<decimal> PriceBook { get; set; }
            public Nullable<decimal> PERatio { get; set; }
            public Nullable<decimal> PEGRatio { get; set; }
            public Nullable<decimal> PriceEPSEstimateCurrentYear { get; set; }
            public Nullable<decimal> PriceEPSEstimateNextYear { get; set; }
            public Nullable<decimal> OneyrTargetPrice { get; set; }
        }


        public static void GetXhtml(IEnumerable<Model.Quote> qs, string fileName)
        { 
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                var quotes = from q in qs
                         join s in db.Stocks on q.Symobl equals s.Symbol
                         select new ViewQuote()
                         {
                             Symobl = s.Symbol,
                             Name = s.Name,
                             OneyrTargetPrice = q.OneyrTargetPrice,
                             PEGRatio = q.PEGRatio,
                             PERatio = q.PERatio,
                             PercentChangeFromFiftydayMovingAverage =  q.PercentChangeFromFiftydayMovingAverage,
                             PercentChangeFromTwoHundreddayMovingAverage = q.PercentChangeFromTwoHundreddayMovingAverage,
                             PriceBook = q.PriceBook,
                             PriceSales = q.PriceSales,
                             PriceEPSEstimateCurrentYear = q.PriceEPSEstimateCurrentYear,
                             PriceEPSEstimateNextYear = q.PriceEPSEstimateNextYear
                         };

                var styleInstruction = new XProcessingInstruction(
                    "xml-stylesheet", "href='styles.css' type='text/css'"
                );

                var docType = new XDocumentType("html",
                  "-//W3C//DTD XHTML 1.0 Strict//EN",
                  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", null);


                //TODO:parsing properties on ananamous object
                var fields = typeof(ViewQuote).GetProperties();
                XNamespace ns = "http://www.w3.org/1999/xhtml";
                var root =
                    new XElement(ns + "html",
                        new XElement(ns + "head",
                            new XElement(ns + "title", "An XHTML page")
                        ),
                        new XElement(ns + "body",
                            new XElement(ns + "table",
                    //header
                                new XElement(ns + "tr",
                                    from h in fields
                                    select new XElement(ns + "th", h.Name)
                                ),
                    //value
                                from q in quotes
                                select new XElement(ns + "tr",
                                    from h in fields
                                    select new XElement(ns + "td", h.GetValue(q))
                                )
                            )
                        )
                    );
                var doc =
                    new XDocument(
                        new XDeclaration("1.0", "utf-8", "no"),
                        new XComment("Reference a stylesheet"),
                        styleInstruction,
                        docType,
                        root
                    );

                doc.Save(fileName);
            }
        }

        public string GetJson(IEnumerable<Model.Quote> quotes)
        {
            return string.Empty;
        
        }
        #endregion static region
    }
}


//<?xml version="1.0" encoding="UTF-8"?>
//<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
//    yahoo:count="1" yahoo:created="2012-06-28T19:24:35Z" yahoo:lang="en-US">
//    <results>
//        <quote symbol="YHOO">
//            <Ask>15.34</Ask>
//            <AverageDailyVolume>16248900</AverageDailyVolume>
//            <Bid>15.33</Bid>
//            <AskRealtime>15.34</AskRealtime>
//            <BidRealtime>15.33</BidRealtime>
//            <BookValue>10.812</BookValue>
//            <Change_PercentChange>-0.18 - -1.16%</Change_PercentChange>
//            <Change>-0.18</Change>
//            <Commission/>
//            <ChangeRealtime>-0.18</ChangeRealtime>
//            <AfterHoursChangeRealtime>N/A - N/A</AfterHoursChangeRealtime>
//            <DividendShare>0.00</DividendShare>
//            <LastTradeDate>6/28/2012</LastTradeDate>
//            <TradeDate/>
//            <EarningsShare>0.881</EarningsShare>
//            <ErrorIndicationreturnedforsymbolchangedinvalid/>
//            <EPSEstimateCurrentYear>0.96</EPSEstimateCurrentYear>
//            <EPSEstimateNextYear>1.12</EPSEstimateNextYear>
//            <EPSEstimateNextQuarter>0.24</EPSEstimateNextQuarter>
//            <DaysLow>15.29</DaysLow>
//            <DaysHigh>15.48</DaysHigh>
//            <YearLow>11.09</YearLow>
//            <YearHigh>16.79</YearHigh>
//            <HoldingsGainPercent>- - -</HoldingsGainPercent>
//            <AnnualizedGain/>
//            <HoldingsGain/>
//            <HoldingsGainPercentRealtime>N/A - N/A</HoldingsGainPercentRealtime>
//            <HoldingsGainRealtime/>
//            <MoreInfo>cnsprmiIed</MoreInfo>
//            <OrderBookRealtime/>
//            <MarketCapitalization>18.695B</MarketCapitalization>
//            <MarketCapRealtime/>
//            <EBITDA>1.319B</EBITDA>
//            <ChangeFromYearLow>+4.25</ChangeFromYearLow>
//            <PercentChangeFromYearLow>+38.32%</PercentChangeFromYearLow>
//            <LastTradeRealtimeWithTime>N/A - &lt;b&gt;15.34&lt;/b&gt;</LastTradeRealtimeWithTime>
//            <ChangePercentRealtime>N/A - -1.16%</ChangePercentRealtime>
//            <ChangeFromYearHigh>-1.45</ChangeFromYearHigh>
//            <PercebtChangeFromYearHigh>-8.64%</PercebtChangeFromYearHigh>
//            <LastTradeWithTime>3:09pm - &lt;b&gt;15.34&lt;/b&gt;</LastTradeWithTime>
//            <LastTradePriceOnly>15.34</LastTradePriceOnly>
//            <HighLimit/>
//            <LowLimit/>
//            <DaysRange>15.29 - 15.48</DaysRange>
//            <DaysRangeRealtime>N/A - N/A</DaysRangeRealtime>
//            <FiftydayMovingAverage>15.3675</FiftydayMovingAverage>
//            <TwoHundreddayMovingAverage>15.3619</TwoHundreddayMovingAverage>
//            <ChangeFromTwoHundreddayMovingAverage>-0.0219</ChangeFromTwoHundreddayMovingAverage>
//            <PercentChangeFromTwoHundreddayMovingAverage>-0.14%</PercentChangeFromTwoHundreddayMovingAverage>
//            <ChangeFromFiftydayMovingAverage>-0.0275</ChangeFromFiftydayMovingAverage>
//            <PercentChangeFromFiftydayMovingAverage>-0.18%</PercentChangeFromFiftydayMovingAverage>
//            <Name>Yahoo! Inc.</Name>
//            <Notes/>
//            <Open>15.39</Open>
//            <PreviousClose>15.52</PreviousClose>
//            <PricePaid/>
//            <ChangeinPercent>-1.16%</ChangeinPercent>
//            <PriceSales>3.79</PriceSales>
//            <PriceBook>1.44</PriceBook>
//            <ExDividendDate/>
//            <PERatio>17.62</PERatio>
//            <DividendPayDate/>
//            <PERatioRealtime/>
//            <PEGRatio>1.24</PEGRatio>
//            <PriceEPSEstimateCurrentYear>16.17</PriceEPSEstimateCurrentYear>
//            <PriceEPSEstimateNextYear>13.86</PriceEPSEstimateNextYear>
//            <Symbol>YHOO</Symbol>
//            <SharesOwned/>
//            <ShortRatio>2.00</ShortRatio>
//            <LastTradeTime>3:09pm</LastTradeTime>
//            <TickerTrend>&amp;nbsp;+++===&amp;nbsp;</TickerTrend>
//            <OneyrTargetPrice>18.03</OneyrTargetPrice>
//            <Volume>8551523</Volume>
//            <HoldingsValue/>
//            <HoldingsValueRealtime/>
//            <YearRange>11.09 - 16.79</YearRange>
//            <DaysValueChange>- - -1.16%</DaysValueChange>
//            <DaysValueChangeRealtime>N/A - N/A</DaysValueChangeRealtime>
//            <StockExchange>NasdaqNM</StockExchange>
//            <DividendYield/>
//            <PercentChange>-1.16%</PercentChange>
//        </quote>
//    </results>
//</query>
 