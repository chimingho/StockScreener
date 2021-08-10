using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HoAssetManagement.StockScreener.Entity
{

    //<?xml version="1.0" encoding="UTF-8"?>
    //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
    //    yahoo:count="2" yahoo:created="2012-06-29T16:04:17Z" yahoo:lang="en-US">
    //    <results>
    //        <rate id="EURUSD">
    //            <Name>EUR to USD</Name>
    //            <Rate>1.2669</Rate>
    //            <Date>6/29/2012</Date>
    //            <Time>12:03pm</Time>
    //            <Ask>1.2671</Ask>
    //            <Bid>1.2668</Bid>
    //        </rate>
    //        <rate id="USDEUR">
    //            <Name>USD to EUR</Name>
    //            <Rate>0.7893</Rate>
    //            <Date>6/29/2012</Date>
    //            <Time>12:03pm</Time>
    //            <Ask>0.7894</Ask>
    //            <Bid>0.7892</Bid>
    //        </rate>
    //    </results>
    //</query>
    public class FX
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id;
        public string Name;
        public decimal? Rate;
        public DateTime? Date;
        public DateTime? Time;
        public decimal? Ask;
        public decimal? Bid;
    }
}
