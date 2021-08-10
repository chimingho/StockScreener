using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HoAssetManagement.StockScreener.Entity;

namespace HoAssetManagement.StockScreener.Modeling
{
    class FinalYearModel:BaseModel
    {

        public override List<double[]> GetIntrinsicValuePerShare()
        {   
            List<double[]> results = new List<double[]>();
            foreach (Quote q in this._qs)
            {
                double vCrtYr = GetDiscountedEarningSingleYear(GetBeginningEps((double)q.EpsEstimateCurrentYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate) * TargetPE + GetDiscountedEarningStream((double)q.EpsEstimateCurrentYear, Years, FirstStageGrowthRate, FirstStageDiscountRate) * GetDividendPayoutRatio((double)q.DividendShare, (double)q.EarningsShare);
                double vNxtYr = GetDiscountedEarningSingleYear(GetBeginningEps((double)q.EpsEstimateNextYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate) * TargetPE + GetDiscountedEarningStream((double)q.EpsEstimateCurrentYear, Years, FirstStageGrowthRate, FirstStageDiscountRate) * GetDividendPayoutRatio((double)q.DividendShare, (double)q.EarningsShare);
                results.Add(new double[2] { Math.Min(vCrtYr, vNxtYr), Math.Max(vCrtYr, vNxtYr) });
            }
            return results;
        }
    }
}
