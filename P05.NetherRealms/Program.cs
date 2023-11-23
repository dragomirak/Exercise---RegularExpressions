using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace P05.NetherRealms
{
    internal class Program
    {
        class Demon
        {
            public Demon(string name, int health, double damage)
            {
                Name = name;
                Health = health;
                Damage = damage;
            }

            public string Name { get; set; }
            public int Health { get; set; }
            public double Damage { get; set; }
        }
        static void Main(string[] args)
        {
            List<string> demonsNames = Console.ReadLine()
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();

            List<Demon> demons = new List<Demon>();

            Regex healthRegex = new Regex(@"[^\d.+\-*\/]");
            Regex damageRegex = new Regex(@"[+\-]?\d+\.?\d*");
            Regex modifierRegex = new Regex(@"[*\/]");

            foreach (string name in demonsNames)
            {
                int health = healthRegex.Matches(name)
                    .Select(n => char.Parse(n.Value))
                    .Sum(x => x);

                double damage = damageRegex.Matches(name)
                    .Select(n => double.Parse(n.Value))
                    .Sum(x => x);

                MatchCollection modifiersMatches = modifierRegex.Matches(name);
                foreach (Match match in modifiersMatches)
                {
                    if (match.Value == "*")
                    {
                        damage *= 2;
                    }
                    else
                    {
                        damage /= 2;
                    }
                }

                Demon demon = new Demon(name, health, damage);
                demons.Add(demon);
            }

            List<Demon> sortedDemons = demons
                .OrderBy(x => x.Name)
                .ToList();

            foreach (var demon in sortedDemons)
            {
                Console.WriteLine($"{demon.Name} - {demon.Health} health, {demon.Damage:f2} damage");
            }
        }
    }
}