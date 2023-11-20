using System.Text.RegularExpressions;

namespace SoftUniBarIncome
{
    class Order
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>();
            
            string input;
            while ((input = Console.ReadLine()) != "end of shift")
            {
                string pattern = @"%(?<name>[A-Z][a-z]+)%[^|$%.]*?<(?<product>\w+)>[^|$%.]*?\|(?<count>\d+)\|[^|$%.]*?(?<price>\d+(\.\d+)?)\$";
                foreach (Match match in Regex.Matches(input, pattern))
                {
                    Order order = new Order();
                    order.CustomerName = match.Groups["name"].Value;
                    order.ProductName = match.Groups["product"].Value;
                    order.Quantity = int.Parse(match.Groups["count"].Value);
                    order.Price = decimal.Parse(match.Groups["price"].Value);

                    orders.Add(order);
                }

            }

            decimal finalPrice = 0;
            foreach (Order order in orders)
            {
                Console.WriteLine($"{order.CustomerName}: {order.ProductName} - {order.GetTotalPrice():f2}");
                finalPrice += order.GetTotalPrice();
            }

            Console.WriteLine($"Total income: {finalPrice:f2}");

        }
    }
}