using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroCompany
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var r = new Random(DateTime.Now.Millisecond);
            var stock = CreateStock();
            var missingOportunity = new MissingOportunity();
            while (true)
            {
                PrintLine($"Stock [{stock.Capacity} | {missingOportunity.IncomeLoss}]: {stock["Z"]} {stock["B"]} {stock["K"]}");
                var buyer = CreateBuyer(r, stock);
                Print($"@New buyer: {buyer} ", ConsoleColor.Yellow);
                if (stock.CanSatisfy(buyer))
                {
                    stock.Sell(buyer);
                    PrintLine($"!Sold: {buyer}", ConsoleColor.Green);
                }
                else
                {
                    missingOportunity.RegisterLoss(buyer.ItemTag, buyer.Amount);
                    PrintLine("!Out of stock!", ConsoleColor.Red);
                }

                if (missingOportunity.IncomeLoss > stock.Capacity)
                {
                    PrintLine("GAME OVER!");
                    break;
                }
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private static void PrintLine(string message, ConsoleColor? color = null)
        {
            PrintInColor(() => Console.WriteLine(message), color);
        }
        private static void Print(string message, ConsoleColor? color = null)
        {
            PrintInColor(() => Console.Write(message), color);
        }

        private static void PrintInColor(Action printMessage, ConsoleColor? color)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                printMessage();
                Console.ForegroundColor = oldColor;
            }
            else
            {
                printMessage();
            }
        }

        private static Buyer CreateBuyer(Random r, Stock s)
        {
            var amount = r.Next(1, s.Capacity / 10);
            var itemTagOffset = r.Next(0, s.Count());
            return new Buyer(amount, s.Skip(itemTagOffset).Take(1).Single().Tag);
        }

        private static Stock CreateStock()
        {
            return new Stock(1000,
               new Item(name: "Ziemniaki", amount: 400, tag: "Z"),
               new Item(name: "Buraki", amount: 200, tag: "B"),
                new Item(name: "Kukurydza", amount: 400, tag: "K"));
        }
    }
}
