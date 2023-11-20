using System.Text;
using System.Text.RegularExpressions;

namespace P02.Race
{
    class Participant
    {
        public string Name { get; set; }
        public uint Distance { get; set; }

        public Participant(string name)
        {
            Name = name;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> participantsList = Console.ReadLine().Split(", ").ToList();
            List<Participant> participants = new List<Participant>();

            foreach (string name in participantsList)
            {
                Participant participant = new Participant(name);
                participants.Add(participant);
            }

            string input;
            while ((input = Console.ReadLine()) != "end of race")
            {
                string patternName = @"[A-Za-z]";
                StringBuilder sbName = new StringBuilder();
                foreach (Match match in Regex.Matches(input, patternName))
                {
                    sbName.Append(match.Value);
                }
                string participantName = sbName.ToString();

                string patternDistance = @"\d";
                uint distance = 0;
                foreach (Match match in Regex.Matches(input, patternDistance))
                {
                    distance += uint.Parse(match.Value);
                }

                Participant foundParticipant = participants.FirstOrDefault(p => p.Name == participantName);
                if (foundParticipant != null)
                {
                    foundParticipant.Distance += distance;
                }
            }

            List<Participant> firstThreeParticipants = participants
                .OrderByDescending(p => p.Distance)
                .Take(3)
                .ToList();

            Console.WriteLine($"1st place: {firstThreeParticipants[0].Name}");
            Console.WriteLine($"2nd place: {firstThreeParticipants[1].Name}");
            Console.WriteLine($"3rd place: {firstThreeParticipants[2].Name}");

        }
    }
}