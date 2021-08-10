using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoAssetManagement.StockScreener.Modeling
{
    class iStockResearch
    {
        //assuming that fixed expenses do not change from year to year while variable expenses change at the rate of revenue growth
        public double ExpenseGrowthRatio;
        public double RevenueGrowthRatio;
        public double VariableExpenseToTotalExpenseRatio
        { get { return ExpenseGrowthRatio / RevenueGrowthRatio; } }

//MODEL'S INPUT PARAMETERS
//Company-specific:

//    Revenue
//    Initial revenue growth rate
//    Gross margin (excluding depreciation)
//    Fixed operating expenses
//    Interest rate on debt
//    Corporate tax rate
//    Cost of stock options / Revenue
//    Dividend payout ratio
//    Revenue / Production assets
//    Revenue / Working capital
//    Cash & short-term investments
//    Goodwill
//    Gross debt
//    Trade receivables
//    Equity
//    Funds from operations
//    Number of shares outstanding

//Macroeconomic/market data:

//    Risk-free rate
//    Inflation rate
//    Equity risk premium
//    Terminal revenue growth rate(or GDP)

//Industry-specific:

//    Maximum debt-to-equity ratio
//    Optimal debt-to-equity ratio
//    Service life of production assets
//    Revenue decline factor
//    Discount rate multiplier


        //adopt DCF model
    }
}
