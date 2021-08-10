using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HoAssetManagement.StockScreener.Entity;

namespace HoAssetManagement.StockScreener.Modeling
{
    class BenGrahamModel:BaseModel
    {

        //Growth and Discount assumptions
        double BondYieldRate;

        //Earnings, shares outstanding, EPS statements
        double BeginningEps; //low: net-of-dividends earnings, high: earnings

        /// <summary>
        ///     return BeginningEps * ((2 * GrowthRate) + 8.5) * (4.4 / BondYieldRate);
        ///     8.5 is the base P/E ratio for a stock with no growth
        /// </summary>
        /// <returns></returns>
        public override List<double[]> GetIntrinsicValuePerShare()
        {
            List<double[]> results = new List<double[]>();
            foreach (Quote q in this._qs)
            {
                double vCrtYr = GetBeginningEps((double)q.EpsEstimateCurrentYear, (double)q.DividendShare) * ((2 * FirstStageGrowthRate) + 8.5) * (4.4 / BondYieldRate);
                double vNxtYr = GetBeginningEps((double)q.EpsEstimateNextYear, (double)q.DividendShare) * ((2 * FirstStageGrowthRate) + 8.5) * (4.4 / BondYieldRate);
                results.Add(new double[2] { Math.Min(vCrtYr, vNxtYr), Math.Max(vCrtYr, vNxtYr) });
            }
            return results;
        }

    }
}
