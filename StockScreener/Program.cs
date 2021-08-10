using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using HoAssetManagement.StockScreener.Feed;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: log4net.Config.XmlConfigurator(ConfigFile ="Log4Net.config", Watch = true)]
//[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "mylogger", Watch = true)]


namespace HoAssetManagement.StockScreener
{
    class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log.Fatal("test");
            log.Error("test");
            log.Warn("test");
            log.Info("test");
            log.Debug("test");
            log.Debug("probess file feeds");

            YahooWebService.StockLookup("92");
            YahooWebService.StockLookup("978");
            YahooWebService.StockLookup("^DJSA");
            YahooWebService.StockLookup("^ME");

            //RunDataFeed();
            //RunScreening(@"ScreeningStrategyTemplate.xml");

                /*
                Entity.Sector.CleanDbTables();
                Entity.Sector.WriteDbTables(Entity.Sector.Fetch());
                using (var db = new StockScreener.Model.StockScreenerEntities())
                {
                    var industries = db.Sectors.SelectMany(s => s.Industries);
                    foreach (var i in industries)
                    {
                        Entity.Stock.WriteDbTables(Entity.Stock.Fetch(i.Id));
                    }
                }
                */

                /*
                using (StreamWriter fd = new StreamWriter(@"DataMemberAttributeExample.xml", true))
                {
                    fd.WriteLine("Company:" + c.Name);
                    foreach (Entity.Quote q in quotes)
                    {
                        DataContractSerializer ser = new DataContractSerializer(typeof(Entity.Quote));
                        ser.WriteObject(fd.BaseStream, q);
                    }

                }*/

        }

        public static void RunDataFeed()
        {
            FileFeed[] fileFeeds = { new YahooFileFeed(), new NasdaqFileFeed() };
            foreach (FileFeed f in fileFeeds)
            {
                f.GetFeeds();
                f.WriteToDb();
            }

            log.Debug("probess http feeds");
            DataFeed[] dfs = { new YahooWebService() };
            foreach (DataFeed f in dfs)
            {
                f.WriteToDb();
            }
        }

        public static void RunScreening(string strategyFile)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(strategyFile);
            IEnumerable<Model.Quote> rslt = Entity.Quote.FilterDbTables(xmlDoc.SelectSingleNode("ScreeningStrategy").SelectSingleNode("ScreeningCriteria[@logic='']").Attributes[0].Value);
            foreach (System.Xml.XmlNode node in xmlDoc.SelectSingleNode("ScreeningStrategy").ChildNodes)
            {
                if (node.NodeType == System.Xml.XmlNodeType.Comment) continue;
                string file = node.Attributes[0].Value;
                string logic = string.IsNullOrWhiteSpace(node.Attributes[1].Value) ? string.Empty : node.Attributes[1].Value;
                IEnumerable<Model.Quote> filter = Entity.Quote.FilterDbTables(file);
                switch (logic)
                {
                    case "|":   //or
                        rslt = rslt.Union(filter);
                        break;
                    case "^":   //xor
                        rslt = rslt.Except(filter).Union(filter.Except(rslt));
                        break;
                    case "!":   //not
                        rslt = rslt.Except(filter);
                        break;
                    case "&":   //and
                        rslt = rslt.Intersect(filter);
                        break;
                }
            }

            Entity.Quote.GetXhtml(rslt, "filter.html");
        }

        public static void test()
        {
            const string exp = "(Math.Min(A - B,C))";

            var p0 = Expression.Parameter(typeof(int), "A");
            var p1 = Expression.Parameter(typeof(int), "B");
            var p2 = Expression.Parameter(typeof(int), "C");

            var e = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { p0, p1, p2 }, typeof(int), exp);
            var result = e.Compile().DynamicInvoke(9, 4, 2);

            Console.WriteLine(result);
            Console.ReadKey();
        
        }


    }
}
