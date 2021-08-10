#define NO_UPDATE_EXISTING_RECORD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using HoAssetManagement.StockScreener.Modeling;
using HoAssetManagement.StockScreener.Feed;

namespace HoAssetManagement.StockScreener.Entity
{

    public class Sector
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected const string SECTOR_INDUSTRY = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.sectors&env=http%3A%2F%2Fdatatables.org%2Falltables.env";

        # region static method

        public static ObservableCollection<Model.Sector> Fetch()
        {
            XDocument doc = XDocument.Load(SECTOR_INDUSTRY);
            ObservableCollection<Model.Sector> sectors = new ObservableCollection<Model.Sector>();
            foreach (XElement s in doc.Descendants("sector"))
            {
                Model.Sector sector = new Model.Sector();
                sector.Id = sectors.Count();
                sector.Name = s.Attribute("name").Value;
                sectors.Add(sector);
                foreach (XElement i in s.Descendants())
                {
                    Model.Industry industry = new Model.Industry();
                    industry.Id = Convert.ToInt32(i.Attribute("id").Value);
                    industry.Name = i.Attribute("name").Value;
                    sector.Industries.Add(industry);
                }
            }
            return sectors;
        }

        public static void CleanDbTables()
        {
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                db.Database.ExecuteSqlCommand("truncate table stocks");
                db.Database.ExecuteSqlCommand("delete from industries");
                db.Database.ExecuteSqlCommand("delete from sectors");
            }
        }

        public static void WriteDbTables(IEnumerable<Model.Sector> sectors)
        {
            if (sectors == null || sectors.Count() <=0) return;
            log.Debug("count > 0");
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {

                    //generate id                    
                    int si=1;
                    foreach(StockExchangeType t in Utility.GetEnumValues<StockExchangeType>())
                    {
                        if(sectors.First().Feed == t.ToString())
                        {
                            si+= (int)t*100;
                            break;
                        }
                    }

                    foreach (Model.Sector s in sectors)
                    {
                        if (s.Id == 0){s.Id = si*100;}

                        int ii=1;
                        foreach(Model.Industry i in s.Industries)
                        {
                            if (i.Id == 0){i.Id = s.Id + ii;}
                            ii++;
                            Entity.Industry.ChangeEntityValue(db, i);

                            foreach (Model.Stock o in i.Stocks)
                            {
                                Entity.Stock.ChangeEntityValue(db, o);
                            }
                        }
                        si++;

                        ChangeEntityValue(db, s);
                    }

                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    log.Error("WriteDbTable", ex);
                }
                catch (Exception ex)
                {
                    log.Error("WriteDbTable", ex);
                }
            }

        }

        static public void ChangeEntityValue(StockScreener.Model.StockScreenerEntities db, Model.Sector s)
        {
            log.Debug("(Feed,Sector):" + s.Feed + s.Name);

            var v = db.Sectors.Find(s.Id, s.Feed);
            if (v == null)
            {
                v = db.Sectors.SingleOrDefault(n => n.Name == s.Name && n.Feed == s.Feed);
            }
            if (v == null)
            {
                db.Sectors.Add(s);
            }
            else
            {
#if EXISTING_ENTITY_UPDATE
                s.Id = v.Id;
                if (v.Feed != null) s.Feed = v.Feed;
                db.Entry(v).CurrentValues.SetValues(s);
#endif
            }
        }

        public static void WriteXhtml(string fileName)
        {
            XDocument doc = XDocument.Load(SECTOR_INDUSTRY);
            doc.Save(fileName);
        }
        #endregion static region

    
    }

}



//<?xml version="1.0" encoding="UTF-8"?>
//<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
//    yahoo:count="9" yahoo:created="2012-06-28T19:31:48Z" yahoo:lang="en-US">
//    <results>
//        <sector name="Conglomerates">
//            <industry id="210" name="Conglomerates"/>
//        </sector>
//        <sector name="Utilities">
//            <industry id="913" name="Diversified Utilities"/>
//            <industry id="911" name="Electric Utilities"/>
//            <industry id="910" name="Foreign Utilities"/>
//            <industry id="912" name="Gas Utilities"/>
//            <industry id="914" name="Water Utilities"/>
//        </sector>
//    </results>
//</query>
 