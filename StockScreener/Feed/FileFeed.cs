using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace HoAssetManagement.StockScreener.Feed
{
    public enum StockExchangeType { NYSE, NASDAQ, NYSEAMEX, NYSEARCA, NYSEMKT, OTCBB, OTCMKTS }
    public enum DataFeedType { NYSE, Yahoo}

    public interface IEntityConvertor
    {
        IEnumerable<T> ToModel<T>();
        IEnumerable<TModel> ToModel<TModel, TSource>(IEnumerable<TSource> source, params object[] args);
        void WriteToDb();
    }
    public interface IFileFeed
    { 
        void GetFeeds();
    }
    public interface IService { }

    abstract public class DataFeed : IEntityConvertor
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        abstract public IEnumerable<TModel> ToModel<TModel, TSource>(IEnumerable<TSource> source, params object[] args);
        virtual public IEnumerable<T> ToModel<T>()
        {
            return ToModel<T, T>(null, null);
        }

        virtual public void WriteToDb()
        {
            Entity.Industry.WriteDbTables(this.ToModel<Model.Industry>());
            Entity.Stock.WriteDbTables(this.ToModel<Model.Stock>());
            Entity.Sector.WriteDbTables(this.ToModel<Model.Sector>());
            Entity.Quote.WriteDbTables(this.ToModel<Model.Quote>());
        }


    }

    abstract public class FileFeed: DataFeed, IFileFeed
    {
        abstract public void GetFeeds();

    }

    public class YahooFileFeed:FileFeed
    {
        const string FILE_NAME_INDUSTRY = "industry.csv";

        #region public 
        override public void GetFeeds()
        {
            log.Debug("probess yahoo industry quote file");
            GetIndustryQuoteFile();
        }
        override public IEnumerable<TModel> ToModel<TModel, TSource>(IEnumerable<TSource> source, params object[] args)
        {
            Type t = typeof(TModel);
            if(typeof(TModel).Equals(typeof(Model.Industry)))
            {
                return (IEnumerable<TModel>)ParseIndustryQuote();
            }else
            {
                return null;
            }
        }

        #endregion public 

        #region private 

        private static bool isFileOld(string fileName)
        {
            return File.GetCreationTime(fileName).AddDays(7) < DateTime.Today;
        }
        private static string GetIndustryQuoteFile()
        {
            string INDUSTRY_SUM = @"http://biz.yahoo.com/p/csv/sum_conameu.csv";

            if (!File.Exists(FILE_NAME_INDUSTRY) || isFileOld(FILE_NAME_INDUSTRY))
            {
                log.Debug("download yahoo industry quote file");
                WebClient webClient = new WebClient();
                webClient.DownloadFile(INDUSTRY_SUM, FILE_NAME_INDUSTRY);
            }
            return FILE_NAME_INDUSTRY;
        }


        private static IEnumerable<Model.Industry> ParseIndustryQuote()
        {
            decimal temp;
            string[] lines = File.ReadAllLines(FILE_NAME_INDUSTRY);
            string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern);
            int i=1;
            var stuff = from line in lines
                        .Skip(1)
                        .Take(lines.Count()-2)
                        let data = r.Split(line)
                        select new Model.Industry{
                            DividendYield = Decimal.TryParse(data[5], out temp) ? temp/100 : (decimal?)null,
                            DebtToEquityRatio = Decimal.TryParse(data[6], out temp) ? temp : (decimal?)null,
                            //Id = (i++)*1000,
                            //MarketCapitalization = Decimal.TryParse(data[2].Remove(data[2].Length-1), out temp)? temp*1000000000:(decimal?)null, 
                            Name = data[0].Replace("\"", "").Trim(),
                            NetProfitMargin= Decimal.TryParse(data[8], out temp) ? temp/100 : (decimal?)null,
                            PE = Decimal.TryParse(data[3], out temp) ? temp : (decimal?)null,
                            PriceBook = Decimal.TryParse(data[7], out temp) ? temp : (decimal?)null,
                            PriceChange = Decimal.TryParse(data[1], out temp) ? temp / 100 : (decimal?)null,
                            PriceToFreeCashFlow = Decimal.TryParse(data[9], out temp) ? temp : (decimal?)null,
                            ROE = Decimal.TryParse(data[4], out temp) ? temp / 100 : (decimal?)null,
                            SectorFeed = DataFeedType.Yahoo.ToString()
                             
                        };
             return stuff;
        }
        #endregion private 

    }

    public class NasdaqFileFeed:FileFeed
    {
        /// <summary>
        /// object model is base on nasdaq feed
        /// </summary>
        public class ListingCompany
        {
            [XmlAttribute(AttributeName = "Symbol")]
            public string Symbol;
            [XmlAttribute(AttributeName = "Industry")]
            public string Name;
            [XmlAttribute(AttributeName = "LastSale")]
            public double? LastSale;
            [XmlAttribute(AttributeName = "Market Cap ")]
            public double? MarketCap;
            [XmlAttribute(AttributeName = "ADR TSO")]
            public string AdrTso;
            [XmlAttribute(AttributeName = "IPOyear")]
            public string IpoYear;
            [XmlAttribute(AttributeName = "Sector")]
            public string Sector;
            [XmlAttribute(AttributeName = "industry")]
            public string Industry;
            [XmlAttribute(AttributeName = "Summary Quote")]
            public string SummaryQuote;
        }
        public class ListingIndustry
        {
            public string Name;
            public ListingCompany[] Companies;
        }
        public class ListingSector
        {
            public string Name;
            public ListingIndustry[] Industries;
        }

        const string FILE_NAME_AMEX = "amexco.csv";
        const string FILE_NAME_NASDAQ = "nasdaqco.csv";
        const string FILE_NAME_NYSE = "nyseco.csv";


        override public void GetFeeds()
        {
            StockExchangeType[] exchanges = { StockExchangeType.NASDAQ, StockExchangeType.NYSE, StockExchangeType.NYSEAMEX }; //Utility.GetEnumValues<StockExchangeType>();
            foreach (StockExchangeType ex in exchanges)
            {
                GetListingFile(ex);
            }
       
        }

        override public void WriteToDb()
        {
            log.Debug("write to db: AMEX file feeds");
            Entity.Sector.WriteDbTables(ToModel(ParseListingCompany(ParseListingCompany(FILE_NAME_AMEX)), StockExchangeType.NYSEAMEX));
            log.Debug("write to db: NYSE file feeds");
            Entity.Sector.WriteDbTables(ToModel(ParseListingCompany(ParseListingCompany(FILE_NAME_NYSE)), StockExchangeType.NYSE));
            log.Debug("write to db: NASDAQ file feeds");
            Entity.Sector.WriteDbTables(ToModel(ParseListingCompany(ParseListingCompany(FILE_NAME_NASDAQ)), StockExchangeType.NASDAQ));
            
        }

        private IEnumerable<Model.Sector> ToModel(IEnumerable<ListingSector> listing, StockExchangeType ex)
        {
            if (listing.Count() <= 0) return (IEnumerable<Model.Sector>) null;
            log.Debug("count > 0");
            var nl = from s in listing
                     select new Model.Sector
                     {
                        Name=s.Name, Feed=StockExchangeType.NYSE.ToString(), 
                        Industries = (from i in s.Industries
                                     select new Model.Industry{
                                        Name=i.Name, SectorFeed = StockExchangeType.NASDAQ.ToString(),
                                        Stocks = (from c in i.Companies
                                                 select new Model.Stock{
                                                    ExchangeSymbol = ex.ToString(),
                                                    Name = c.Name,
                                                    Symbol = c.Symbol
                                                 }).ToArray()
                                     }).ToArray()
                     };
            return nl;
        }

        override public IEnumerable<TModel> ToModel<TModel, TSource>(IEnumerable<TSource> source, params object[] args)
        {
            Type t = typeof(TModel);
            if (typeof(TModel).Equals(typeof(Model.Sector))) //including sector, Industry and Stock
            {   
                return (IEnumerable<TModel>)ToModel((IEnumerable<ListingSector>)source, (StockExchangeType)args[0]);
            }
            else
            {
                return null;
            }
        }


        private static bool isFileOld(string fileName)
        {
            return File.GetCreationTime(fileName).AddMonths(6)  < DateTime.Today;
        }

        private static string GetListingFile(StockExchangeType exchange)
        {
            string COMPANIES = string.Empty;
            string FILE_NAME = string.Empty;
            switch (exchange)
            {
                case StockExchangeType.NYSEAMEX:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=amex&render=download";
                    FILE_NAME = FILE_NAME_AMEX;
                    break;
                case StockExchangeType.NASDAQ:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nasdaq&render=download";
                    FILE_NAME = FILE_NAME_NASDAQ;
                    break;
                case StockExchangeType.NYSE:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nyse&render=download";
                    FILE_NAME = FILE_NAME_NYSE;
                    break;
            }
            if (!File.Exists(FILE_NAME) || isFileOld(FILE_NAME))
            {
                log.Debug("download " + FILE_NAME);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(COMPANIES, FILE_NAME);
            }
            return FILE_NAME;        
        }


        private static IEnumerable<ListingCompany> ParseListingCompany(string fileName)
        {
            log.Debug("parsing");
            double temp;
            string[] lines = File.ReadAllLines(fileName);
            string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern);
            var companies = from line in lines
                        .Skip(1)
                        .Take(lines.Count() - 2)
                        let data = r.Split(line)
                        select new ListingCompany
                        {
                            Symbol = data[0].Replace("\"", "").Trim(),
                            Name = data[1].Replace("\"", "").Trim(),
                            LastSale = Double.TryParse(data[2], out temp) ? temp : Double.NaN, 
                            MarketCap = Double.TryParse(data[3], out temp) ? temp : Double.NaN,
                            AdrTso = data[4].Replace("\"", "").Trim(),
                            IpoYear = data[5].Replace("\"", "").Trim(),
                            Sector = data[6].Replace("\"", "").Trim(),
                            Industry = data[7].Replace("\"", "").Trim(),
                            SummaryQuote = data[8].Replace("\"", "").Trim()
                        };

            return companies;
        }
        private static IEnumerable<ListingSector> ParseListingCompany(IEnumerable<ListingCompany> companies)
        {
            var groups = from s in companies
                         group s by s.Sector into g
                         select new ListingSector
                         {
                             Name = g.Key,
                             Industries = (from i in g
                                           group i by i.Industry into g2
                                           select new ListingIndustry
                                           {
                                               Name = g2.Key,
                                               Companies = g2.ToArray()
                                           }).ToArray()
                         };
            
            
            return groups;
        }

    }


}
