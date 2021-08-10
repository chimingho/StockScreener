

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
    public class Industry
    {

        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteDbTables(IEnumerable<Model.Industry> industries)
        {
            if (industries == null || industries.Count() <=0) return;
            log.Debug("count > 0");

            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {
                    foreach (Model.Industry s in industries)
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

        static public void ChangeEntityValue(StockScreener.Model.StockScreenerEntities db, Model.Industry s)
        {
            log.Debug("(feed, industry):" + s.SectorFeed + s.Name );

            var v = db.Industries.Find(s.Id, s.SectorFeed);
            if (v == null)
            {
                v = db.Industries.SingleOrDefault(n => n.Name == s.Name && n.SectorFeed == s.SectorFeed && n.SectorId == s.SectorId);
            }

            if (v == null)
            {
                if (s.Id <= 0)
                {
                   s.Id = db.Industries.Where(i => i.SectorFeed == s.SectorFeed).Max(i => i.Id) + 1;
                }  
                db.Industries.Add(s);
            }
            else
            {
#if EXISTING_ENTITY_UPDATE
                s.Id = v.Id;
                s.SectorId = v.SectorId;
                if (v.SectorFeed != null) s.SectorFeed = v.SectorFeed;
                db.Entry(v).CurrentValues.SetValues(s);
#endif

            }
        }


    }
}