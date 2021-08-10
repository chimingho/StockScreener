using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoAssetManagement.StockScreener.Entity
{
    class StagingStock
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void CleanDbTables()
        {
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                db.Database.ExecuteSqlCommand("truncate table stagingstocks");
            }
        }

        public static void WriteDbTables(IEnumerable<Model.StagingStock> stocks)
        {
            if (stocks == null || stocks.Count() <= 0) return;
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
                try
                {
                    foreach (Model.StagingStock s in stocks)
                    {
                        db.StagingStocks.Add(s);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Error("WriteDbTable", ex);
                }
            }

        }


    }
}
