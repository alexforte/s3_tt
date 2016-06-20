using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JPM.Core.Model
{
    public class Market
    {
        #region Properties

        public List<Stock> Stocks;

        public string Name { get; set; }

        #endregion

        #region Constructor

        public Market(string marketName)
        {
            Name = marketName;

            Stocks = new List<Stock>();

        }

        #endregion

        #region Methods

        public void AddStock(Stock stock)
        {
            if (stock != null)
            {
                Stocks.Add(stock);
            }
            //else
            //{
            //    throw new ApplicationException("Stock cannot be null.");
            //}
        }

        public void RecordTrade(string stockSymbol, DateTime tradeDate, Trade.TypeOfTrade tradeType, int quantity, decimal price)
        {
            Stocks.Where(stock => stock.Symbol == stockSymbol).First().RecordTrade(tradeDate, tradeType, quantity, price);
        }


        public double CalculateIndexValue()
        {
            double retValue = 0;

            Console.WriteLine(String.Format("\nCalculating {0} All Share Index Value based on stock trades.", Name));
            Console.WriteLine("\nFormula Used: (StockPrice1 * StockPrice2 * StockPriceN)^1/N ");
            Console.WriteLine("\nStocks Prices: ");

            double product = 1;
            int n = 0;

            foreach (Stock stock in Stocks)
            {
                try
                {
                    Console.Write(" " + Convert.ToDouble(stock.CalculateStockPrice()));
                    product *= (double)stock.CalculateStockPrice();
                    n += 1;
                }
                catch (ApplicationException e)
                {
                    // If no trades in determined period on a stock then ignore this stock from calculating the geometric mean formula
                    if (e.Message != "No trades in last 15 minutes period.")
                    {
                        throw e;
                    }

                }
            }

            retValue = Math.Pow(product, 1.0 / n);

            return retValue;
        }

        public override string ToString()
        {
            var retValue = String.Format("Market Name: {0}\nStocks: {1}", Name, Stocks.Count);

            return retValue;
        }


        #endregion
    }

}
