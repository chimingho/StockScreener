using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoAssetManagement.StockScreener.Modeling
{
    public abstract class BaseModel
    {
        public BaseModel() { }
        protected Entity.Quote[] _qs;
        protected Dictionary<string, decimal> _rslt;


        //Growth and Discount assumptions
        public double RiskFreeRate = 0.02;
        public int Years = 3;
        //assumptions
        public double FirstStageGrowthRate {get{return 0.08;}}
        /// <summary>
        /// based on long-term growth rate of S&P 
        /// </summary>
        public double SecondStageGrowthRate = 0.095;
        /// <summary>
        /// Discount rate = Stock Beta x Risk premium + RiskFreeRate
        /// 10~15% hurdle rate
        /// </summary>
        public double FirstStageDiscountRate { get { return Math.Max(0.1, 0.1 /*ToDo*/ ); } }
        /// <summary>
        /// Discount rate = Stock Beta x Risk premium + RiskFreeRate
        /// Second-stage discount rate should be higher thant first-stage discount rate
        /// </summary>
        public double SecondStageDscountRate { get { return Math.Max(FirstStageDiscountRate, 1 * 3 + RiskFreeRate); } }

        /// <summary>
        /// historic PE average = 17
        /// </summary>
        public double TargetPE = 17; 


        public BaseModel(Entity.Quote[] quotes)
        {
            _qs = quotes;
        }

        abstract public List<double[]> GetIntrinsicValuePerShare();


#region method

        public double GetDiscountedEarningSingleYear(double beginningEarning, int years, double firstStageGrowth, double firstStageDiscountRate)
        {
            return beginningEarning * Math.Pow((1 + firstStageGrowth) / (1 + firstStageDiscountRate), years);
        }
        public double GetDiscountedEarningStream(double beginningEarning, int years, double firstStageGrowth, double firstStageDiscountRate)
        {
            double totalEarning = 0;
            for (int i = 1; i <= years; i++)
            {
                totalEarning += beginningEarning * Math.Pow((1 + firstStageGrowth) / (1 + firstStageDiscountRate), i);
            }
            return totalEarning;
        }
        public double GetTerminalValue(double beginningEarning, int years, double firstStageGrowth, double firstStageDiscountRate, double secondStageGrowth, double secondStageDiscountRate)
        {
            return beginningEarning * Math.Pow((1 + firstStageGrowth), years + 1) / (secondStageDiscountRate - secondStageGrowth) / Math.Pow(1 + firstStageDiscountRate, years);
        }
        /// <summary>
        ///  Dividend Payout Ratio = Yearly Dividend per Share / Earnings per Share
        ///                        = Dividends / Net Income
        /// </summary>
        /// <param name="dividendPerShare"></param>
        /// <param name="EarningsPerShare"></param>
        /// <returns></returns>
        public double GetDividendPayoutRatio(double dividendPerShare, double EarningsPerShare) { return dividendPerShare / EarningsPerShare;}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetBeginningEps(double estimateEps, double dividendPerShare) { return estimateEps - dividendPerShare; }
        

#endregion method

        /***
         * Strategic Profit Formula
         * Return on equity = [profits/sales] × [sales/assets] × [assets/equity]
         *                  = profitability x productivity x capital structure 
         *                  = {profit margin, expense} x {asset productivity, unit productivity} x {cash flow, debt and liquidity} (strategic financial)
         * strategic intangible: 
         *  1) market place: market share, customer base, customer loyalty, brand strength, channel strength, supply chain 
         *  2) management: commerence, candor, independence
         */
#region asset productivity ratio
    //Receivables turnover = sales $ ÷ accounts receivable $
        public double GetAccountReceivableTurnoverRate(double sales, double accountReceivable){return sales / accountReceivable;        }
    //Average collection period = 360 ÷ receivables turnover
        public double GetAccountReceivableCollectionPeriod(double sales, double accountReceivable){return 360 / GetAccountReceivableTurnoverRate(sales, accountReceivable);        }
    //Inventory turnover = sales ÷ inventory $
        public double GetInventoryTurnoverRate(double sales, double inventory){return sales / inventory;        }
    //average inventory shelf life = 360 ÷ Inventory turnover
        public double GetInventoryShelfLife(double sales, double inventory){return 360/ GetInventoryTurnoverRate(sales, inventory);        }
    //Fixed asset turnover = sales $ ÷ fixed asset $
        public double GetFixedAssetTurnoverRate(double sales, double PPandE){return sales / PPandE;        }
    //Total asset turnover = – sales $ ÷ total asset $ (plus intangible)
        public double GetTotalAssetTurnoverRate(double sales, double PPandE, double intangible){return sales / (PPandE+intangible);        }
        //Nonfinancial productivity ratios
        //    Sales per employee
        //    Sales per square foot
        //    Average selling price (ASP)
        //    Industry specials
#endregion 
#region financial strength ratio
    //Current ratio = current assets ÷ current liabilities 	(>2 is safe)
        public double GetCurrentRatio(double currentAsset, double currentLiability){ return currentAsset / currentLiability; }
        public double GetWorkingCapital(double currentAsset, double currentLiability, double excessCash){ return currentAsset - currentLiability - excessCash ; }
    //Quick ratio = (current assets – inventory) ÷ current liabilities (>1 is safe)
        public double GetQuickRatio(double currentAsset, double inventory, double currentLiability) { return (currentAsset - inventory) / currentLiability; }

    //Cash to Debt
    //Debt to equity = total debt ÷ owner’s equity
        public double GetDebtToEquityRatio(double totalDebt, double equity) { return totalDebt / equity; }

    //Debt to total assets = total debt ÷ total assets
        public double GetDebtToAssetRatio(double totalDebt, double totalAsset) { return totalDebt / totalAsset; }
        //Overall cash flow ratio = cash inflow from operations ÷ (investing cash outflows + financing cash outflows)
        public double GetCashFlowRatio(double inflow, double outflow) { return inflow / outflow; }
    //Cash flow to earnings = cash flow from operations ÷ net earnings
        public double GetCashFlowToEarning(double netCashFlow, double netEarning) { return netCashFlow / netEarning; }
        //FCF = cash flow from operations - capital expenditures(PP&E)
        //    = net earnings +  depreciation and amortization - average annual capital expenditures, less (or plus) working capital changes.
        //    = operating earnings - interest and taxes - capital investments - net working capital increases (or plus working capital decreases, if working capital is being reduced)
        public double GetFreeCashFlow(double netCashFlow, double PPandE){ return netCashFlow - PPandE;}
        /// <summary>
        /// Total Common Equity / Total Common Shares Outstanding
        /// </summary>
        /// <param name="totalCommonEquity"></param>
        /// <param name="totalCommonSharesOutstanding"></param>
        /// <returns></returns>
        public double GetBookValuePerShare(double totalCommonEquity, double totalCommonSharesOutstanding) { return totalCommonEquity / totalCommonSharesOutstanding; }

#endregion 
#region profitibility ratio
    //Gross margin = (sales – cost of goods sold) ÷ sales
    //Operating margin = (sales – cost of goods sold – operating expenses) ÷ sales
    //Return on assets = net earnings ÷ total assets
        public double GetROA(double netEarning, double totalAsset) { return netEarning / totalAsset; }
        //Return on equity (ROE) = net earnings ÷ owner’s equity
        public double GetROE(double netEarning, double equity) { return netEarning / equity; }
        //Return on invested capital (ROIC) = net earnings ÷ (owner’s equity + long-term debt) ??
        //                                  = NOPAT divided by ‘operating net working capital plus operating fixed assets
        public double GetROIC(double netEarning, double workingCapital, double PPandE) { return netEarning / (workingCapital+PPandE); }
        //Return on sales (Net Profit Margine) = net earnings ÷ sales
        public double GetROS(double netEarning, double sales) { return netEarning / sales; }
#endregion 
#region valuation ratio
        //Price to earnings (P/E) = stock price ÷ net earnings per share
        //                        = (1/profit margin) × Price/sales (P/S)
        //                        = 1/ROE × Price to book (P/B)
        public double GetPERatio(double profitMargin, double PS) { return (1 / profitMargin) * PS; }
        public double GetPERatioContinuous(double returnRate, double Time) { return Math.Exp(-returnRate * Time); }
        public double GetPV(double amount, double discountedRate, double years) { return amount * Math.Pow(1 + discountedRate, years); }
        public double GetPVContinuous(double returnRate, double Time) { return Math.Exp(returnRate * Time); }
        public double GetReturnContinuous(double currentNav, double prevNav) { return Math.Log(currentNav / prevNav); }
        //Earnings yield (EY) = Earnings/equity (ROE) × Equity/price (1/(P/B))
        //                    = Earnings/sales × Sales/price (1/(P/S))
        //Price/earnings to growth (PEG) = (P/E) ÷ earnings growth rate
        public double GetPEGRatio(double PE, double growthRate) { return PE / growthRate; }
        //Price to sales (P/S) = stock price (total market cap) ÷ total sales (revenues)
        public double GetPSRatio(double marketCap, double sales) { return marketCap / sales; }
        //Price to book (P/B) = stock price (total market cap) ÷ book value
        public double GetPBRatio(double marketCap, double bookValue) { return marketCap / bookValue; }
        /// <summary>
        /// EV is a measure of theoretical takeover price, and is useful in comparisons against income statement line items above the interest expense/income lines such as revenue and EBITDA.
        /// </summary>
        /// <param name="marketCap"></param>
        /// <param name="totalDebt"></param>
        /// <param name="totalCash"></param>
        /// <param name="shortTermInvestments"></param>
        /// <returns></returns>
        public double GetEnterpriseValue(double marketCap, double totalDebt, double totalCash, double shortTermInvestments) { return marketCap + totalDebt - totalCash - shortTermInvestments; }
        /// <summary>
        /// Firm value compared against revenue. Provides a more rigorous comparison than the Price/Sales ratio by removing the effects of capitalization from both sides of the ratio. Since revenue is unaffected by the interest income/expense line item, the appropriate value comparison should also remove the effects of capitalization, as EV does.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="revenue"></param>
        /// <returns></returns>
        public double EnterpriseValuePerRevenue(double ev, double revenue) { return ev / revenue; }
        public double EnterpriseValuePerEbitda(double ev, double ebitda) { return ev / ebitda; }
        public double MarketCap(double shareNumber, double sharePrice) { return shareNumber * sharePrice; }

#endregion 
#region risk manamement 
        public double ArithmaticAverage(params double[] n) { return n.Average(); }
        public double GeometricAverage(params double[] n) { return Math.Pow(n.Aggregate((seed, next) => seed*next), n.Count()); }
        public double StandardVariance(params double[] n) 
        {
            double mean = ArithmaticAverage(n);
            return n.Sum(p => Math.Pow(p - mean, 2)) / n.Count(); 
        }
        //downside investment risk
        public double SemiVarianceDownside(double threshold, params double[] n)
        {
            return n.Sum(p => Math.Pow(Math.Min(p - threshold, 0), 2)) / n.Count();
        }

        //logrithm average
        
        /// <summary>
        /// The capital asset pricing model provides a formula that calculates the expected return on a security based on its level of risk. The formula for the capital asset pricing model is the risk free rate plus beta times the difference of the return on the market and the risk free rate
        /// </summary>
        /// <param name="returnRiskFree"></param>
        /// <param name="returnMarket"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        public double CAPM(double returnRiskFree, double returnMarket, double beta) { return returnRiskFree + beta * (returnMarket - returnRiskFree); }
        /// <summary>
        /// a measure of the trade-off between return and the risk the investor was exposed to in order to earn that return 
        /// </summary>
        /// <param name="returnActual"></param>
        /// <param name="returnRiskFree"></param>
        /// <param name="stdDevReturn"></param>
        /// <returns></returns>
        public double SharpeRatio(double returnActual, double returnRiskFree, double stdDevReturn) { return (returnActual-returnRiskFree) / stdDevReturn;}
        /// <summary>
        /// a measure of bad risk from Sharpe ratio total risk
        /// </summary>
        /// <param name="returnActual"></param>
        /// <param name="returnRiskFree"></param>
        /// <param name="downsideDevReturn"></param>
        /// <returns></returns>
        public double SortinoRatio(double returnActual, double returnRiskFree, double downsideDevReturn) { return (returnActual-returnRiskFree) / downsideDevReturn;}
        public double TreynorRatio(double returnActual, double returnRiskFree, double beta) { return (returnActual - returnRiskFree) / beta; }
#endregion risk management

    }
}
