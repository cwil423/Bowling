using System.Linq;

namespace Bowling
{
    class BowlingSimulator
    {
        static void Main(string[] args)
        {
            int shot = 1;
            bool firstShot = true;
            int pinsRemaining = 10;
            List<Shot> shots = new();

            Console.WriteLine("Welcome to bowling simulator.");
            while (shot < 18)
            {
                var totalScore = shots.Select(x => x.Score).Sum();
                Console.WriteLine($"Frame: {shot}, Total score: {totalScore}.");
                Console.WriteLine($"");
                var pinsHit = BowlPrompt(firstShot);

                if (pinsHit > pinsRemaining || pinsHit == -1)
                {
                    Console.WriteLine("Please enter a valid input");
                    continue;
                }

                try
                {
                    var twoShotsAgo = shots[shot - 3];
                    if (twoShotsAgo.IsStrike)
                    {
                        twoShotsAgo.Score += pinsHit;
                    }
                }
                catch
                {
                    // ignored
                }

                try
                {
                    var oneShotAgo = shots[shot - 2];
                    if (oneShotAgo.IsSpare || oneShotAgo.IsStrike)
                    {
                        oneShotAgo.Score += pinsHit;
                    }
                }
                catch
                {
                    // ignored
                }

                if (firstShot)
                {
                    var shotObject = new Shot();
                    pinsRemaining -= pinsHit;
                    if (pinsHit == 10)
                    {
                        Console.WriteLine("STRIKE!");
                        shotObject.Score = 10;
                        shotObject.IsStrike = true;
                        shot++;
                        pinsRemaining = 10;
                        shots.Add(shotObject);
                        continue;
                    }

                    shotObject.Score = pinsHit;
                    shots.Add(shotObject);
                    firstShot = false;
                    continue;
                }

                var currentFrame = shots[shot - 1];
                currentFrame.Score += pinsHit;
                if (pinsHit == pinsRemaining)
                {
                    Console.WriteLine("SPARE!");
                    currentFrame.IsSpare = true;
                }
                firstShot = true;
                shot++;
                pinsRemaining = 10;
            }
        }

        private static int BowlPrompt(bool isFirstShot)
        {
            Console.WriteLine(isFirstShot ? "First bowl." : "Second bowl.");
            Console.WriteLine("How many pins did you hit with your bowl?");
            string? input = Console.ReadLine();
            bool success = int.TryParse(input, out var pinsHit);
            if (success)
            {
                return pinsHit;
            }
            else return -1;
        }

        private class Shot
        {
            public int Score { get; set; }
            public bool IsStrike { get; set; }
            public bool IsSpare { get; set; }
        }

        private class TenthFrame
        {
            public int Score { get; set; }
            public int FirstRoll { get; set; }
            public int SecondRoll { get; set; }
        }
    }
}


    

