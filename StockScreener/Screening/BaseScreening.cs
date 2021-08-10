using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

using System.Linq;
using System.Text;
using HoAssetManagement.StockScreener.Modeling;

namespace HoAssetManagement.StockScreener.Screening
{
    class BaseScreening
    {

        #region risk 
        double Alpha;
        double Beta;
        double SharpeRatio;
        double SortinoRatio;
        double TreynorRatio;
        #endregion 

        #region balance sheet
        double PriceBookRatio;
        double PriceSaleRatio;
        double PriceEarningRatio;
        double PriceEarningGrowthRatio;
        double EntityValueReturnRatio;
        double EntityValueOperatingCashFlow;
        double EntityValueFreeCashFlow;
        double CashPerShare;
        double CurrentRatio;
        double QuickRatio;
        double TotalDebtPerEquity;
        double LongTermDebtPerEquity;
        #endregion 

        #region income
        double EpsTtm;
        double EpsMrq;
        double GrossMargin;
        double EbitdaMargin;
        double OperatingMargin;
        double ProfitMargin;
        #endregion 

        #region cash flow
        double FreeCashFlow;
        double OperatingCashFlow;
        #endregion

        #region profitibility
        double ReturnOnEquity;
        double ReturnOnAsset;
        #endregion 

        #region dividend
        double Yield;
        #endregion 

        #region share 
        double ShortRatio;
        double MovingAverageVs50Days;
        double MovingAverageVs200Days;
        double CurrentPriceVs52Weeks;
        #endregion 

        #region estimation
        #endregion 

    }
}
