using System;
using System.Windows;
using System.Collections.ObjectModel;

namespace HoAssetManagement.StockScreener.Modeling
{
    /*
    public class CardDeckViewModel : DependencyObject
    {
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

        public ObservableCollection<Quote> Quotes { get; set; }

        public CardDeckViewModel()
        {
            //Quotes = new ObservableCollection<Quote>();
            //Quotes.Add(new Quote("YHOO"));
            //Quotes.Add(new Quote("MSFT"));
            //Quote.Fetch(Quotes);

            //poll every 25 seconds
            //timer.Interval = new TimeSpan(0, 0, 125); 
            //timer.Tick += (o, e) => YahooStockEngine.Fetch(Quotes);
                              
            //timer.Start();

            //FileFeed.ParseYahooIndustryQuote();
            //FileFeed.ParseListingCompany(FileFeed.StockExchangeType.NYSE);
            string[] indices = UsEdgar.GetEdgarIndex(UsEdgar.GetEdgarIndexFile("2011", "QTR4"), "10-K", null);
            indices = new string[] { indices[0], indices[1], indices[2] };
            indices = UsEdgar.GetEdgarXbrlFile(indices);
            string[] filePath = MvvmPattern.CorporateFinance.UsEdgar.RendererReport(indices);

            foreach (string s in filePath)
            {
                Console.WriteLine("-----------" + s);
                string[] files = MvvmPattern.CorporateFinance.UsEdgar.GetReportFiles(s, GaapTax.StatementOfBalanceSheet.GetKeywords());

                UsEdgar.UsTaxonomyCell[] cells;
                string[] tags = GaapTax.GetTaxonomy(GaapTax.StatementOfBalanceSheet.Tag, 3);
                foreach (string tag in tags)
                {
                    cells = null;
                    foreach (string file in files)
                    {
                        if (MvvmPattern.CorporateFinance.UsEdgar.GetReportValue(file, tag, out cells))
                        {
                            Console.WriteLine("{0}={1}", tag, cells.Length > 0 ? cells[0].NumericAmount : string.Empty);
                            break;
                        }
                    }


                }
            }
        }
    }
     */
}