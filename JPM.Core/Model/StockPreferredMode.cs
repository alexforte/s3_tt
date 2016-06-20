using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPM.Core.Model
{

    public class StockPreferredMode: Stock
    {
        //Reference: http://finance.yahoo.com/q?s=AAPL

        #region Properties

      

        #endregion

        #region Constructor

        public StockPreferredMode(string symbol, string currency, decimal lastDividend, decimal fixedDividend, decimal parValue)
            : base(symbol, currency, lastDividend, fixedDividend, parValue)
        {
        }

        #endregion

        #region Methods

        public override decimal CalculateDividendYield()
        {
            decimal retValue;
            decimal stockPrice = CalculateStockPrice();
            retValue = (FixedDividend == 0 || stockPrice == 0) ? 0 : ((FixedDividend / 100) * ParValue) / stockPrice;
            return retValue;
        }

        public override string ToString()
        {
            var retValue = String.Format("Symbol: {0}\nPrice: {1:0.00} {2}\nDividend Yield (Preferred): {3:0.00}\nP/E Ratio: {4:0.00}", Symbol, CalculateStockPrice(), Currency, CalculateDividendYield(), CalulatePERatio());

            return retValue;
        }
    
        #endregion
    
    } 
}
