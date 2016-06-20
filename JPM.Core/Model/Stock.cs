using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JPM.Core.Model
{

    public class Stock
    {
         //Reference: http://finance.yahoo.com/q?s=AAPL

         #region Properties

        public string Symbol { get; set; }
        public string Currency { get; set; }
        //public string Company { get; set; }

        // public decimal Price { get; set; }
        public decimal LastDividend { get; set; }
        public decimal FixedDividend { get; set; }
        public decimal ParValue { get; set; }
        public List<Trade> Trades { get; set; }

        #endregion

         #region Constructor

        public Stock(string symbol, string currency, decimal lastDividend, decimal fixedDividend, decimal parValue)
        {
            Symbol = symbol;
            Currency = currency;
            LastDividend = lastDividend;
            FixedDividend = fixedDividend;
            ParValue = parValue;

            Trades = new List<Trade>();
        }

        #endregion

        #region Methods

        public virtual decimal CalculateDividendYield()
        {
            decimal retValue;
            decimal stockPrice = CalculateStockPrice();
            retValue = (LastDividend == 0 || stockPrice==0) ? 0 : LastDividend / stockPrice;
            return retValue;
        }

        public decimal CalulatePERatio()
        {

            decimal retValue;
            decimal stockPrice = CalculateStockPrice();
            retValue = (LastDividend == 0 || stockPrice == 0) ? 0 : stockPrice / LastDividend;
            return retValue;
        }

        public decimal CalculateStockPrice()
        {
            decimal retValue = 0;
            DateTime last15mins;
            last15mins = DateTime.UtcNow.AddMinutes(-15);

            decimal totalValue = 0;
            decimal totalQuantity = 0;

            var recentTrades = Trades.Where(trade => trade.TradeTimestamp >= last15mins);

            if (recentTrades.Count() == 0)
            {
                retValue = 0;
                return 0;
            }

            foreach (var trade in recentTrades)
            {
                totalValue += trade.getTradeValue();
                totalQuantity += trade.Quantity;
            }

            retValue =  totalValue / totalQuantity;
            return retValue;
        }

        public void RecordTrade(DateTime tradeDate, Trade.TypeOfTrade tradeType, int quantity, decimal price)
        {
            Trades.Add(new Trade(tradeDate, tradeType, quantity, price));
        }

        public void PrintTrades()
        {
            Console.WriteLine(String.Format("\nStock: {0}", this.Symbol));

            for (int i = 0; i < Trades.Count; i++)
            {
                Trade t = Trades[i];
                Console.WriteLine(String.Format("\n{0}-{1}. {2} {3} Trades at {4:0.00}{5} for Stock {6}", i, t.TradeTimestamp, Enum.GetName(typeof(Trade.TypeOfTrade), t.TradeType), t.Quantity, t.Price, this.Currency, this.Symbol));
            }
            Console.WriteLine("\n---------------------------------------------------------------");

        }

        /// <summary>
        /// Simulate a random number of stock trading for every stock required
        /// </summary>
        /// <param name="pStock">Stock reference</param>
        public void SimulateTradesForStock()
        {
            int randomPauseSecs = new Random(DateTime.Now.Millisecond).Next(1, 3) * 1000;
            int randomTrades = new Random(DateTime.Now.Millisecond).Next(20);
            Console.WriteLine(String.Format("\nGenerating {0} Trades For Stock {1} every {2} sec", randomTrades, this.Symbol, randomPauseSecs / 1000));
            for (int i = 0; i < randomTrades; i++)
            {
                // Generate Random Data
                int randomQty = new Random(DateTime.Now.Millisecond).Next(100);

                int randomAction = new Random(DateTime.Now.Millisecond).Next(0, 2);  // Set to 2 to increse Seed
                if (randomAction > 0) randomAction = 1;

                int randomMinutesFromNow = new Random(DateTime.Now.Millisecond).Next(1, 60);
                decimal randomPrice = this.CalculateStockPrice(); //new Random(DateTime.Now.Millisecond).NextDouble(1);
                DateTime randomTimestamp = DateTime.UtcNow;
                int randomSign = new Random(DateTime.Now.Millisecond).Next(0, 1);
                if (randomSign == 0) // Minus Sign
                {
                    var toSubt = new Random(DateTime.Now.Millisecond).Next(1, 100);
                    randomPrice = Math.Abs(this.CalculateStockPrice() - (Convert.ToDecimal(toSubt) / 100));
                    randomTimestamp = randomTimestamp.AddMinutes(-randomMinutesFromNow);
                }
                else // Plus Sign
                {
                    var toAdd = new Random(DateTime.Now.Millisecond).Next(1, 100);
                    randomPrice = Math.Abs(this.CalculateStockPrice() + (Convert.ToDecimal(toAdd) / 100));
                    randomTimestamp = randomTimestamp.AddMinutes(randomMinutesFromNow);
                }
                // Avoid 0 Price
                if (randomPrice == 0) randomPrice = 0.01m;

                var t = new Trade(randomTimestamp, (Trade.TypeOfTrade)randomAction, randomQty, randomPrice);
                Trades.Add(t);

                //Console.WriteLine(String.Format("\n{0}-{1}. {2} {3} Trades at {4:0.00}{5} for Stock {6}", i, t.TradeTimestamp, Enum.GetName(typeof(Trade.TypeOfTrade), t.TradeType), t.Quantity, t.Price, this.Currency, this.Symbol));
                Thread.Sleep(randomPauseSecs); // Simulate a pause in trading 

            }

            //Console.WriteLine("\nGeneration completed.");
        }

        public override string ToString()
        {
            var retValue = String.Format("Symbol: {0}\nPrice: {1:0.00} {2}\nDividend Yield: {3:0.00} ({5:0.00}%)\nP/E Ratio: {4:0.00}", Symbol, CalculateStockPrice(), Currency, CalculateDividendYield(), CalulatePERatio(), CalculateDividendYield() * 100);

            return retValue;
        }

        #endregion

    
    } 

}
