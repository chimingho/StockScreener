using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Collections.ObjectModel;

namespace HoAssetManagement.StockScreener
{
    public enum StockExchangeType { NYSE, NYSDAQ, NYSEAMEX, NYSEARCA, NYSEMKT, OTCBB, OTCMKTS }

    class FileFeed
    {

        public class YahooIndustryQuote
        {
            [XmlAttribute(AttributeName = "Industry")]
            public string Name;
            [XmlAttribute(AttributeName = "1-Day Price Chg % ")] 
            public double? PriceChange;
            [XmlAttribute(AttributeName = "Market Cap ")]
            public double? MarketCap;
            [XmlAttribute(AttributeName = "P/E ")]
            public double? PE;
            [XmlAttribute(AttributeName = "ROE % ")]
            public double? ROE;
            [XmlAttribute(AttributeName = "Div. Yield % ")]
            public double? DividendYield;
            [XmlAttribute(AttributeName = "Debt to Equity ")]
            public double? DebtToEquityRatio;
            [XmlAttribute(AttributeName = "Price to Book ")]
            public double? PBR;
            [XmlAttribute(AttributeName = "Net Profit Margin (mrq) ")]
            public double? NetProfitMargin; 
            [XmlAttribute(AttributeName = "Price To Free Cash Flow (mrq) ")]
            public double? PriceToFreeCashFlow; 
            
        }

        public static YahooIndustryQuote[] ParseYahooIndustryQuote()
        { 
            string INDUSTRY_SUM = @"http://biz.yahoo.com/p/csv/sum_conameu.csv";
            string FILE_NAME = "industry.csv";

            if (!File.Exists(FILE_NAME) || File.GetLastWriteTime(FILE_NAME).AddDays(7) < DateTime.Today)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(INDUSTRY_SUM, FILE_NAME);
            }

            double temp;
            string[] lines = File.ReadAllLines(FILE_NAME);
            string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern);
            var stuff = from line in lines
                        .Skip(1)
                        .Take(lines.Count()-2)
                        let data = r.Split(line)
                        select new YahooIndustryQuote { 
                            Name = data[0].Replace("\"", ""),
                            PriceChange = Double.TryParse(data[1], out temp)? temp:Double.NaN, // Convert.ToDouble(data[1]),
                            MarketCap = Double.TryParse(data[2].Remove(data[2].Length-1), out temp)? temp*1000000000:Double.NaN,
                            PE = Double.TryParse(data[3], out temp) ? temp : Double.NaN,
                            ROE = Double.TryParse(data[4], out temp) ? temp : Double.NaN,
                            DividendYield = Double.TryParse(data[5], out temp) ? temp : Double.NaN,
                            DebtToEquityRatio = Double.TryParse(data[6], out temp) ? temp : Double.NaN,
                            PBR = Double.TryParse(data[7], out temp) ? temp : Double.NaN,
                            NetProfitMargin = Double.TryParse(data[8], out temp) ? temp : Double.NaN,
                            PriceToFreeCashFlow = Double.TryParse(data[9], out temp) ? temp : Double.NaN,
                        };
             return stuff.ToArray();
        }


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
        public class ListingCompanyIndustry
        {
            public string Name;
            public ListingCompany[] Companies;
        }

        public static ListingCompany[] ParseListingCompany(StockExchangeType exchange)
        {
            string COMPANIES=string.Empty;
            string FILE_NAME=string.Empty;
            switch (exchange)
            { 
                case StockExchangeType.NYSEAMEX:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=amex&render=download";
                    FILE_NAME = "amexco.csv";
                    break;
                case StockExchangeType.NYSDAQ:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nasdaq&render=download";
                    FILE_NAME = "nasdaqco.csv";
                    break;
                case StockExchangeType.NYSE:
                    COMPANIES = @"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nyse&render=download";
                    FILE_NAME = "nyseco.csv";
                    break;
                default:
                    return new ListingCompany[0];
            }
            if (!File.Exists(FILE_NAME) || File.GetLastWriteTime(FILE_NAME).AddMonths(6) < DateTime.Today)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(COMPANIES, FILE_NAME);
            }

            double temp;
            string[] lines = File.ReadAllLines(FILE_NAME);
            string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern);
            var companies = from line in lines
                        .Skip(1)
                        .Take(lines.Count() - 2)
                        let data = r.Split(line)
                        select new ListingCompany
                        {
                            Symbol = data[0].Replace("\"", ""),
                            Name = data[1].Replace("\"", ""),
                            LastSale = Double.TryParse(data[2], out temp) ? temp : Double.NaN, 
                            MarketCap = Double.TryParse(data[3], out temp) ? temp : Double.NaN,
                            AdrTso = data[4].Replace("\"", ""),
                            IpoYear = data[5].Replace("\"", ""),
                            Sector = data[6].Replace("\"", ""),
                            Industry = data[7].Replace("\"", ""),
                            SummaryQuote = data[8].Replace("\"", "")
                        };

            return companies.ToArray();
        }
        public static ListingCompanyIndustry[] ParseListingCompanyIndustry(StockExchangeType exchange)
        {
            ListingCompany[] companies = ParseListingCompany(exchange);
            var orderGroups = from p in companies
                                      group p by p.Industry into g
                                      select new ListingCompanyIndustry { Name = g.Key, Companies = g.ToArray() };
            return orderGroups.ToArray<ListingCompanyIndustry>();
        }

    }
}
