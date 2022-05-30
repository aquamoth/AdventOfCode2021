class Day11
{
    int width;
    int height;
    int[] energyLevels = Array.Empty<int>();

    long flashCounter = 0;

    public void Run(string[] lines)
    {
        Initialize(lines);

        for (var i = 0; i < 1000; i++)
        {
            Step();


            if (energyLevels.All(x => x <= 0))
            {
                Console.WriteLine($"All flashes at step {i + 1}");
                return;

            }
        }

        for (int y = 0; y < height; y++)
        {
            var row = energyLevels.Skip((y + 1) * (width + 2) + 1).Take(width);
            var rowString = string.Join("", row);
            Console.WriteLine(rowString);
        }

        Console.WriteLine($"Flashes = {flashCounter}");
    }

    private void Step()
    {
        for (var i = 0; i < energyLevels.Length; i++)
        {
            if (energyLevels[i] >= 0)
            {
                energyLevels[i]++;
            }
        }

        while (TryFlash())
        {
        }

        ResetFlashedEnergies();
    }

    private bool TryFlash()
    {
        var hasFlashed = false;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var index = MapIndex(x, y);
                if (energyLevels[index] > 9)
                {
                    Flash(x, y);
                    hasFlashed = true;
                }
            }
        }

        return hasFlashed;
    }

    private void Flash(int x, int y)
    {
        flashCounter++;
        var index = MapIndex(x, y);
        energyLevels[index] = -1;

        Boost(x - 1, y - 1);
        Boost(x, y - 1);
        Boost(x + 1, y - 1);
        Boost(x - 1, y);
        Boost(x + 1, y);
        Boost(x - 1, y + 1);
        Boost(x, y + 1);
        Boost(x + 1, y + 1);
    }

    private void Boost(int x, int y)
    {
        var index = MapIndex(x, y);
        if (energyLevels[index] >= 0)
        {
            energyLevels[index]++;
        }
    }

    private void ResetFlashedEnergies()
    {
        for (int i = 0; i < energyLevels.Length; i++)
        {
            if (energyLevels[i] == -1)
            {
                energyLevels[i] = 0;
            }
        }
    }

    private void Initialize(string[] lines)
    {
        width = lines[0].Length;
        height = lines.Length;
        energyLevels = Enumerable.Repeat(int.MinValue, (width + 2) * (height + 2)).ToArray();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                int mapIndex = MapIndex(x, y);
                energyLevels[mapIndex] = int.Parse(lines[y][x].ToString());
            }
        }
    }

    private int MapIndex(int x, int y) => (y + 1) * (width + 2) + (x + 1);
}
