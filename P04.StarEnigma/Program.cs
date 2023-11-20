using System.Text;
using System.Text.RegularExpressions;

namespace P04.StarEnigma
{
    class Message
    {
        public Message(string planetName, uint population, string attackType, uint soldiersCount)
        {
            PlanetName = planetName;
            Population = population;
            AttackType = attackType;
            SoldiersCount = soldiersCount;
        }

        public string PlanetName { get; set; }
        public uint Population { get; set; }
        public string AttackType { get; set; }
        public uint SoldiersCount { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfMessages = int.Parse(Console.ReadLine());
            List<Message> messages = new List<Message>();

            string starPattern = @"[SsTtAaRr]";
            string messagePattern = @"@(?<planet>[A-Za-z]+)[^@\-!:>]*:(?<population>\d+)[^@\-!:>]*!(?<attack>[A|D])![^@\-!:>]*->(?<soldiers>\d+)";

            for (int i = 0; i < numberOfMessages; i++)
            {
                string encryptedMessage = Console.ReadLine();
                int decryptionKey = Regex.Matches(encryptedMessage, starPattern).Count();

                StringBuilder messageSb = new StringBuilder();
                for (int j = 0; j < encryptedMessage.Length; j++)
                {
                    messageSb.Append((char)(encryptedMessage[j] - decryptionKey));
                }

                string decyptedMessage = messageSb.ToString();

                Match match = Regex.Match(decyptedMessage, messagePattern);
                if (!match.Success)
                {
                    continue;
                }

                string planetName = match.Groups["planet"].Value;
                uint population = uint.Parse(match.Groups["population"].Value);
                string attack = match.Groups["attack"].Value;
                uint soldiers = uint.Parse(match.Groups["soldiers"].Value);

                Message message = new Message(planetName, population, attack, soldiers);
                messages.Add(message);

            }

            List<Message> attackedPlanets = messages
                .Where(m => m.AttackType == "A")
                .OrderBy(m => m.PlanetName)
                .ToList();

            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
            attackedPlanets.ForEach(m => Console.WriteLine($"-> {m.PlanetName}"));

            List<Message> destroyedPlanets = messages
                .Where(m => m.AttackType == "D")
                .OrderBy(m => m.PlanetName)
                .ToList();

            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
            destroyedPlanets.ForEach(m => Console.WriteLine($"-> {m.PlanetName}"));
        }
    }
}