using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HoAssetManagement.StockScreener.Entity;

namespace HoAssetManagement.StockScreener.Modeling
{
    class IndefiniteLifeModel:BaseModel
    {

        int LongTermDebtAdjustmentPerShare;

        public override List<double[]> GetIntrinsicValuePerShare()
        {
            List<double[]> results = new List<double[]>();
            foreach (Quote q in this._qs)
            {
                double vCrtYr = GetDiscountedEarningStream(GetBeginningEps((double)q.EpsEstimateCurrentYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate)
                                + GetTerminalValue(GetBeginningEps((double)q.EpsEstimateCurrentYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate, SecondStageGrowthRate, SecondStageDscountRate)
                                - LongTermDebtAdjustmentPerShare;
                double vNxtYr = GetDiscountedEarningStream(GetBeginningEps((double)q.EpsEstimateNextYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate)
                                + GetTerminalValue(GetBeginningEps((double)q.EpsEstimateNextYear, (double)q.DividendShare), Years, FirstStageGrowthRate, FirstStageDiscountRate, SecondStageGrowthRate, SecondStageDscountRate)
                                - LongTermDebtAdjustmentPerShare;
                results.Add(new double[2] { Math.Min(vCrtYr, vNxtYr), Math.Max(vCrtYr, vNxtYr) });
            }
            return results;
        }



    }
}
