using System.Drawing;
namespace Day06;

class Day06
{
    public void Run(string[] lines)
    {
        const int DAYS = 256;

        var fish = lines.Single().Split(',').Select(int.Parse).ToArray();
        //var fish = new[] { 0 };


        //var fishCount = Trivial(fish, DAYS); //80=>359344
        //Console.WriteLine(fishCount);



        var current = Enumerable.Range(0, 9)
            .Select(day => (long)fish.Count(age => age == day))
            .ToArray();

        for (int day = 1; day <= DAYS; day++)
        {
            var next = new[] {
                current[1] ,
                current[2] ,
                current[3] ,
                current[4] ,
                current[5] ,
                current[6] ,
                current[7] + current[0],
                current[8],
                current[0]
            };

            current = next;
        }

        Console.WriteLine(current.Sum());
    }

    private static int Trivial(int[] ages, int days)
    {
        var fish = ages.Select(age => new LanternFish(age)).ToList();

        for (int day = 1; day <= days; day++)
        {
            foreach (var f in fish)
                f.StepAge();

            var births = fish.Count(f => f.IsSpawning);
            fish.AddRange(Enumerable.Repeat(0, births).Select(x => new LanternFish(8)));
        }

        return fish.Count;
    }
}
