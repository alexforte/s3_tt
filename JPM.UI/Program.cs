using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPM.Core.Model;

namespace JPM.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nInitializing Market ans Stocks...");

            // Initialize Market
            var market = new Market("GBCE Global Beverage Corporation Exchange - Market");

            // Initialize Stocks with example data
            var stock1 = new Stock("TEA", "£", 0, 0, 100);
            var stock2 = new Stock("POP", "£", 8, 0, 100);
            var stock3 = new Stock("ALE", "£", 23, 0, 60);
            var stock4 = new StockPreferredMode("GIN", "£", 8, 2, 100);
            var stock5 = new Stock("JOE", "£", 13, 0, 250);

            // Simulating Stocks Trading
            stock1.SimulateTradesForStock();
            stock2.SimulateTradesForStock();
            stock3.SimulateTradesForStock();
            stock4.SimulateTradesForStock();
            stock5.SimulateTradesForStock();

            // Add Stocks to Market
            market.AddStock(stock1);
            market.AddStock(stock2);
            market.AddStock(stock3);
            market.AddStock(stock4);
            market.AddStock(stock5);
            Console.WriteLine("");

            // Print A-1 Requirement
            Console.WriteLine("\nPrint A-1 Requirement\n");
            Console.WriteLine(stock1.ToString() + "\n");
            Console.WriteLine(stock2.ToString() + "\n");
            Console.WriteLine(stock3.ToString() + "\n");
            Console.WriteLine(stock4.ToString() + "\n");
            Console.WriteLine(stock5.ToString() + "\n");
            Console.Read();

            // Print A-2 Requirement
            Console.WriteLine("\nPrint A-2 Requirement\n");
            Console.WriteLine(stock1.ToString() + "\n");
            Console.WriteLine(stock2.ToString() + "\n");
            Console.WriteLine(stock3.ToString() + "\n");
            Console.WriteLine(stock4.ToString() + "\n");
            Console.WriteLine(stock5.ToString() + "\n");
            Console.Read();

            // Print A3 Requirement
            Console.WriteLine("\nPrint A-3 Requirement\n");
            Console.WriteLine(market.ToString());
            stock1.PrintTrades();
            stock2.PrintTrades();
            stock3.PrintTrades();
            stock4.PrintTrades();
            stock5.PrintTrades();
            Console.WriteLine("");
            Console.Read();

            // Print A4 Requirement
            Console.WriteLine("\nPrint A-4 Requirement\n");

            int periodInMinutes = 15;
            Console.WriteLine(String.Format("\nStock {0} price based on trades recorded in past {1} minutes: {2}", stock1.Symbol, periodInMinutes, stock1.CalculateStockPrice()));
            Console.WriteLine(String.Format("\nStock {0} price based on trades recorded in past {1} minutes: {2}", stock2.Symbol, periodInMinutes, stock2.CalculateStockPrice()));
            Console.WriteLine(String.Format("\nStock {0} price based on trades recorded in past {1} minutes: {2}", stock3.Symbol, periodInMinutes, stock3.CalculateStockPrice()));
            Console.WriteLine(String.Format("\nStock {0} price based on trades recorded in past {1} minutes: {2}", stock4.Symbol, periodInMinutes, stock4.CalculateStockPrice()));
            Console.WriteLine(String.Format("\nStock {0} price based on trades recorded in past {1} minutes: {2}", stock5.Symbol, periodInMinutes, stock5.CalculateStockPrice()));
            Console.WriteLine("");
            Console.Read();

            // Print B1 Requirement
            Console.WriteLine("\nPrint B-1 Requirement\n");
            var value = market.CalculateIndexValue();
            Console.WriteLine(String.Format("\nGBCE All Share Index Value: {0:0.00}", value));

            Console.WriteLine("\nPress Key to exit\n");
            Console.Read();
        }
    }
}
