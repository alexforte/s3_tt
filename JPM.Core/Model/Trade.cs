using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPM.Core.Model
{

    public class Trade
    {
        #region Properties

        public DateTime TradeTimestamp { get; set; }
        public TypeOfTrade TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        #endregion

        #region Enums
        public enum TypeOfTrade : int
        {
            Buy = 0,
            Sell = 1,
        }
        #endregion

        #region Constructor
        public Trade(DateTime tradeDate, TypeOfTrade tradeType, int quantity, decimal price)
        {
            TradeTimestamp = tradeDate;
            TradeType = tradeType;
            Quantity = quantity;
            Price = price;
        }

        #endregion

        #region Methods
        public Decimal getTradeValue()
        {
            return Quantity * Price;
        }

        public override string ToString()
        {
            var retValue = String.Format("Quantity: {0} - {1} at {2:0.00}\nDate: {3}", Quantity, Enum.GetName(typeof(TypeOfTrade), TradeType), Price, TradeTimestamp.ToString());

            return retValue;
        }

        #endregion

    }
    
}
