using System.Text.RegularExpressions;

namespace P01.Furrniture
{
    class Furniture
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Furniture> furnitures = new List<Furniture>();

            string pattern = @">{2}(?<name>[A-Z]+([a-z]+)?)<{2}(?<price>\d+(\.\d+)?)!(?<quantity>\d+)";

            string input;
            while ((input = Console.ReadLine()) != "Purchase")
            {
                foreach (Match match in Regex.Matches(input, pattern))
                {
                    Furniture furniture = new Furniture();
                    furniture.Name = match.Groups["name"].Value;
                    furniture.Price = decimal.Parse(match.Groups["price"].Value);
                    furniture.Quantity = int.Parse(match.Groups["quantity"].Value);

                    furnitures.Add(furniture);
                }

            }

            Console.WriteLine("Bought furniture:");

            decimal totalCost = 0;
            foreach (Furniture furniture in furnitures)
            {
                Console.WriteLine(furniture.Name);
                totalCost += furniture.GetTotalPrice();
            }

            Console.WriteLine($"Total money spend: {totalCost:f2}");

        }
    }
}