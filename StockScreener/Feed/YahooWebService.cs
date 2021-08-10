using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace HoAssetManagement.StockScreener.Feed
{
    public class YahooWebService : DataFeed
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly System.Collections.Generic.HashSet<string> hs = new System.Collections.Generic.HashSet<string>();

        public override IEnumerable<TModel> ToModel<TModel, TSource>(IEnumerable<TSource> source, params object[] args)
        {
            Type t = typeof(TModel);
            if (typeof(TModel).Equals(typeof(Model.Quote)))
            {
                using (var db = new StockScreener.Model.StockScreenerEntities())
                {
                    DateTime date = DateTime.Today;
                    switch (DateTime.Today.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            date = date.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            date = date.AddDays(-2);
                            break;
                    }

                    if (db.Quotes.Count() > 0 && db.Quotes.Max(d => d.RecordDate) >= date.AddDays(-5))
                        return (IEnumerable<TModel>)null;
                    log.Debug("get yahoo stock quotes");
                    return (IEnumerable<TModel>)GetExcangeStockQuote(db.Stocks.AsEnumerable());
                }
            }
            else
            {
                return null;
            }
        }

        private const string STOCK_REALTIME_QUOTE = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=http%3A%2F%2Fdatatables.org%2Falltables.env";
        public static IEnumerable<Model.Quote> GetExcangeStockQuote(IEnumerable<Model.Stock> stocks)
        {
            int range = 40;
            int count = stocks.Count();
            int iter = count / range;

            IEnumerable<Model.Quote> rslt = null;

            for (int i = 0; i < iter; i++)
            {
                string symbolList = String.Join("%2C", stocks.Skip(i * range).Take(Math.Min(range, count - 4 * range)).Select(w => "%22" + w.Symbol + "%22").ToArray());
                string url = string.Format(STOCK_REALTIME_QUOTE, symbolList);
                XDocument doc = XDocument.Load(url);
                if (rslt == null) rslt = Parse(doc);
                else rslt = rslt.Union(Parse(doc));
            }
            return rslt;
        }

        private static IEnumerable<Model.Quote> Parse(XDocument doc)
        {
            ObservableCollection<Model.Quote> quotes = new ObservableCollection<Model.Quote>();

            foreach (XElement q in doc.Descendants("quote"))
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(q.Element("ErrorIndicationreturnedforsymbolchangedinvalid").Value))
                        continue;

                    Model.Quote quote = new Model.Quote();
                    quote.Symobl = q.Attribute("symbol").Value.Trim();
                    //quote.Ask = Utility.GetDecimal(q.Element("Ask").Value);
                    //quote.Bid = Utility.GetDecimal(q.Element("Bid").Value);
                    //quote.AverageDailyVolume = Utility.GetDecimal(q.Element("AverageDailyVolume").Value);
                    quote.BookValue = Utility.GetDecimal(q.Element("BookValue").Value);
                    //quote.Change = Utility.GetDecimal(q.Element("Change").Value);
                    quote.DividendShare = Utility.GetDecimal(q.Element("DividendShare").Value);
                    quote.RecordDate = Utility.GetDateTime(q.Element("LastTradeDate").Value + " " + q.Element("LastTradeTime").Value).Value;
                    quote.EarningsShare = Utility.GetDecimal(q.Element("EarningsShare").Value);
                    quote.EPSEstimateCurrentYear = Utility.GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
                    quote.EPSEstimateNextYear = Utility.GetDecimal(q.Element("EPSEstimateNextYear").Value);
                    quote.EPSEstimateNextQuarter = Utility.GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
                    //quote.DailyLow = Utility.GetDecimal(q.Element("DaysLow").Value);
                    //quote.DailyHigh = Utility.GetDecimal(q.Element("DaysHigh").Value);
                    quote.YearLow = Utility.GetDecimal(q.Element("YearLow").Value);
                    quote.YearHigh = Utility.GetDecimal(q.Element("YearHigh").Value);
                    quote.MarketCapitalization = Utility.GetDecimal(q.Element("MarketCapitalization").Value);
                    quote.EBITDA = Utility.GetDecimal(q.Element("EBITDA").Value);
                    //quote.ChangeFromYearLow = Utility.GetDecimal(q.Element("ChangeFromYearLow").Value);
                    //quote.PercentChangeFromYearLow = Utility.GetDecimal(q.Element("PercentChangeFromYearLow").Value);
                    //quote.ChangeFromYearHigh = Utility.GetDecimal(q.Element("ChangeFromYearHigh").Value);
                    //quote.LastTradePrice = Utility.GetDecimal(q.Element("LastTradePriceOnly").Value);
                    //quote.PercentChangeFromYearHigh = Utility.GetDecimal(q.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
                    //quote.FiftyDayMovingAverage = Utility.GetDecimal(q.Element("FiftydayMovingAverage").Value);
                    //quote.TwoHunderedDayMovingAverage = Utility.GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
                    //quote.ChangeFromTwoHundredDayMovingAverage = Utility.GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
                    quote.PercentChangeFromTwoHundreddayMovingAverage = Utility.GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
                    quote.PercentChangeFromFiftydayMovingAverage = Utility.GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
                    //quote.Name = q.Element("Name").Value;
                    //quote.Open = Utility.GetDecimal(q.Element("Open").Value);
                    //quote.PreviousClose = Utility.GetDecimal(q.Element("PreviousClose").Value);
                    //quote.ChangeInPercent = Utility.GetDecimal(q.Element("ChangeinPercent").Value);
                    quote.PriceSales = Utility.GetDecimal(q.Element("PriceSales").Value);
                    quote.PriceBook = Utility.GetDecimal(q.Element("PriceBook").Value);
                    //quote.ExDividendDate = Utility.GetDateTime(q.Element("ExDividendDate").Value);
                    quote.PERatio = Utility.GetDecimal(q.Element("PERatio").Value);
                    //quote.DividendPayDate = Utility.GetDateTime(q.Element("DividendPayDate").Value);
                    quote.PEGRatio = Utility.GetDecimal(q.Element("PEGRatio").Value);
                    //quote.PriceEpsEstimateCurrentYear = Utility.GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
                    //quote.PriceEpsEstimateNextYear = Utility.GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);
                    quote.ShortRatio = Utility.GetDecimal(q.Element("ShortRatio").Value);
                    //quote.OneYearPriceTarget = Utility.GetDecimal(q.Element("OneyrTargetPrice").Value);
                    //quote.Volume = Utility.GetDecimal(q.Element("Volume").Value);
                    //quote.StockExchange = q.Element("StockExchange").Value;
                    //quote.LastUpdate = DateTime.Now;

                    quotes.Add(quote);
                }
                catch (Exception ex)
                { }
            }
            return quotes;
        }

        private const string STOCK_LOOKUP = @"http://d.yimg.com/autoc.finance.yahoo.com/autoc?query={0}&callback=YAHOO.Finance.SymbolSuggest.ssCallback";
        public static void StockLookup(string keyword)
        {
            string pat = @"{""ResultSet"":[\s\n]*(.*)[\s\n]*}";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890^.".ToCharArray();
            bool isWarrantNextTierSearch = false;

            try
            {
                log.Info("query string:" + keyword);
                string url = string.Format(STOCK_LOOKUP, keyword);
                ObservableCollection<Model.StagingStock> stagingStocks = new ObservableCollection<Model.StagingStock>();

                using (WebClient webClient = new WebClient())
                {
                    string data = Encoding.UTF8.GetString(webClient.DownloadData(url));
                    Match m = r.Match(data);
                    while (m.Success)
                    {
                        Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(Utility.CleanInput(m.Value));
                        var stocks = o["ResultSet"]["Result"].Children();
                        if (stocks.Count() > 8)
                            isWarrantNextTierSearch |= true;

                        foreach (var s in stocks)
                        {
                            if (s["symbol"] == null || hs.Contains(s["symbol"].ToString())) continue;

                            Model.StagingStock ss = new Model.StagingStock();
                            ss.symbol = s["symbol"] == null ? null : s["symbol"].ToString();
                            ss.name = s["name"] == null ? null : s["name"].ToString();
                            ss.exch = s["exch"] == null ? null : s["exch"].ToString();
                            ss.exchDisp = s["exchDisp"] == null ? null : s["exchDisp"].ToString();
                            ss.type = s["type"] == null ? null : s["type"].ToString();
                            ss.typeDisp = s["typeDisp"] == null ? null : s["typeDisp"].ToString();
                            ss.recordDate = DateTime.Now;
                            hs.Add(ss.symbol);
                            stagingStocks.Add(ss);
                        }
                        m = m.NextMatch();
                    }
                }
                //insert into db
                Entity.StagingStock.WriteDbTables(stagingStocks);

                /*                 
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                js.Deserialize<StagingStock[]>(data);
                 */

            }
            catch (Exception ex)
            {
                log.Error("stocklookup failed", ex);
            }

            if (isWarrantNextTierSearch)
            {
                foreach (char c in alpha)
                {
                    StockLookup(keyword + c);
                }
            }
      }
    }
}
