using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoAssetManagement.StockScreener.Entity
{

    //    <?xml version="1.0" encoding="UTF-8"?>
    //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
    //    yahoo:count="1" yahoo:created="2012-06-28T18:39:59Z" yahoo:lang="en-US">
    //    <results>
    //        <industry id="112" name="Agricultural Chemicals">
    //            <company name="Agrium Inc." symbol="AGU"/>
    //            <company name="American Vanguard Corporation" symbol="AVD"/>
    //        </industry>
    //    </results>
    //</query>
 


    public class Company
    {
        public string name;
        public string symbol;
    }
}
