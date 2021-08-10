using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoAssetManagement.StockScreener.Entity
{
    abstract public class BaseEntity
    {
        virtual public void WriteDbTables<T>(IEnumerable<T> items) where T : class
        {
            /*
            using (var db = new StockScreener.Model.StockScreenerEntities())
            {
            }
             */
            throw new NotImplementedException();

        }
    }
}
