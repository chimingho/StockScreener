using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HoAssetManagement.StockScreener.Entity;

namespace HoAssetManagement.StockScreener.Modeling
{
    public class AcquisitionModel : BaseModel   
    {
            //EnvironmentVariableTarget 
            //Earnings, shares outstanding, EPS statements
            //double BeginningEps; //fully diluted
            //double BookValuePerShare;
            //double PerShareDividendNetBuyback;
            public double AcquisitionToBookRatio = 2; //2

            //stock quote 
            /*
            double NetPerShareEarningToBookValue
            { get { return BeginningEps - PerShareDividendNetBuyback; } 
            }
            */


        public override List<double[]> GetIntrinsicValuePerShare()
        {
            List<double[]> results = new List<double[]>();
            foreach (Quote q in this._qs)
            {
                //assume BookValuePerShare remain intact during the period
                //(GetDiscountedEarningStream(NetPerShareEarningToBookValue, Years, GrowthRate, DiscountRate)+ PV(BookValuePerShare, DiscountRate, Years)) * AcquisitionToBookRatio;
                double vCrtYr = (GetDiscountedEarningStream(GetBeginningEps((double)q.EpsEstimateCurrentYear, (double)q.DividendShare), Years, (double)(q.EpsEstimateNextQuarter / q.EpsEstimateCurrentYear - 1), FirstStageDiscountRate) + GetPV((double)(q.LastTradePrice / q.PriceBook), FirstStageDiscountRate, Years)) * AcquisitionToBookRatio;
                double vNxtYr = (GetDiscountedEarningStream(GetBeginningEps((double)q.EpsEstimateNextYear, (double)q.DividendShare), Years, (double)(q.EpsEstimateNextQuarter / q.EpsEstimateCurrentYear - 1), FirstStageDiscountRate) + GetPV((double)(q.LastTradePrice / q.PriceBook), FirstStageDiscountRate, Years)) * AcquisitionToBookRatio;
                results.Add(new double[2]{Math.Min(vCrtYr, vNxtYr), Math.Max(vCrtYr, vNxtYr)});
            }
            return results;
            

        }

    }
}
