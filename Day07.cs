class Day07
{
    struct Crab
    {
        public int X;
        public int Count;
        public int NextFuelCost;
        public int UsedFuel = 0;

        public override string ToString()
        {
            return $"{X}: {Count} crabs used {UsedFuel} fuel (next = {NextFuelCost})";
        }
    }

    public void Run(string[] lines)
    {
        var positions = lines.Single().Split(',').Select(int.Parse).ToArray();

        var crabPostions = positions
            .GroupBy(p => p)
            .Select(grp => new Crab { X = grp.Key, Count = grp.Count(), NextFuelCost = grp.Count() })
            .OrderBy(x => x.X)
            .ToList();

        while (crabPostions.Count > 1)
        {
            var diff = crabPostions[0].NextFuelCost - crabPostions[crabPostions.Count - 1].NextFuelCost;
            if (diff < 0)
            {
                var crab0 = crabPostions[0];
                var crab1 = crabPostions[1];

                crab0 = new Crab
                {
                    X = crab0.X + 1,
                    Count = crab0.Count,
                    UsedFuel = crab0.UsedFuel + crab0.NextFuelCost,
                    NextFuelCost = crab0.NextFuelCost + crab0.Count
                };

                if (crab0.X == crab1.X)
                {
                    crabPostions[1] = new Crab
                    {
                        X = crab1.X,
                        Count = crab1.Count + crab0.Count,
                        UsedFuel = crab1.UsedFuel + crab0.UsedFuel,
                        NextFuelCost = crab1.NextFuelCost + crab0.NextFuelCost
                    };

                    crabPostions.RemoveAt(0);
                }
                else
                {
                    crabPostions[0] = crab0;
                }
            }
            else if (diff >= 0)
            {
                var crab0 = crabPostions[crabPostions.Count - 1];
                var crab1 = crabPostions[crabPostions.Count - 2];

                crab0 = new Crab
                {
                    X = crab0.X - 1,
                    Count = crab0.Count,
                    UsedFuel = crab0.UsedFuel + crab0.NextFuelCost,
                    NextFuelCost = crab0.NextFuelCost + crab0.Count
                };

                if (crab0.X == crab1.X)
                {
                    crabPostions[crabPostions.Count - 2] = new Crab
                    {
                        X = crab1.X,
                        Count = crab1.Count + crab0.Count,
                        UsedFuel = crab1.UsedFuel + crab0.UsedFuel,
                        NextFuelCost = crab1.NextFuelCost + crab0.NextFuelCost
                    };

                    crabPostions.RemoveAt(crabPostions.Count - 1);
                }
                else
                {
                    crabPostions[crabPostions.Count - 1] = crab0;
                }
            }
        }


        var crabs = crabPostions[0];
        Console.WriteLine($"{crabs.X} = {crabs.Count} crabs == {crabs.UsedFuel}");





        //var medianPos = positions.OrderBy(x=>x).Skip(positions.Length / 2).Take(2).ToArray();
        //var median = positions.Length % 2 == 0 ? medianPos[0] : medianPos.Average();
        //var fuel = positions.Select(p=>Math.Abs(median - p)).Sum();
        //Console.WriteLine($"{median} => {fuel}");
    }
}
