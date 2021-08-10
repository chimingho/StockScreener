using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace HoAssetManagement.StockScreener
{
    public struct YQL
    {
        public const string STOCK_HISTORICAL_DATA = @"http://query.yahooapis.com/v1/public/yql?q=
                select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22{0}%22%20and%20startDate%20%3D%20%22{1}%22%20and%20endDate%20%3D%20%22{2}%22
                &env=http%3A%2F%2Fdatatables.org%2Falltables.env";



        public const string OPTION_CONTRACT = @"http://query.yahooapis.com/v1/public/yql?q=SELECT%20*%20FROM%20yahoo.finance.option_contracts%20WHERE%20symbol%3D'YHOO'&env=http%3A%2F%2Fdatatables.org%2Falltables.env";
        public const string OPTION = @"http://query.yahooapis.com/v1/public/yql?q=SELECT%20*%20FROM%20yahoo.finance.options%20WHERE%20symbol%3D'YHOO'%20AND%20expiration%3D'2012-05'&env=http%3A%2F%2Fdatatables.org%2Falltables.env";
        public const string SECTOR = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.sectors&env=http%3A%2F%2Fdatatables.org%2Falltables.env";
        public const string FX = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20({0})
                &env=http%3A%2F%2Fdatatables.org%2Falltables.env";
    }
    
}
