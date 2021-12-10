class Day09
{
    int width;
    int height;
    int[] heightMap = Array.Empty<int>();

    public void Run(string[] lines)
    {
        Initialize(lines);

        var risk = CalculateRisk();
        Console.WriteLine($"Risk = {risk}");


        var value = FindRooms()
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate(1, (a, b) => a * b);

        Console.WriteLine(value);
    }

    private List<int> FindRooms()
    {
        List<int> roomSizes = new List<int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                int mapIndex = MapIndex(x, y);

                if (heightMap[mapIndex] < 9)
                {
                    var size = FloodFill(x, y);
                    roomSizes.Add(size);
                }
            }
        }

        return roomSizes;
    }

    private int FloodFill(int x, int y)
    {
        var size = 0;

        var stack = new Stack<(int x, int y)>();
        stack.Push((x,y));
        while (stack.Any())
        {
            var point = stack.Pop();
            if (heightMap[MapIndex(point.x, point.y)] < 9) 
            {
                size++;
                heightMap[MapIndex(point.x, point.y)] = 9;
                stack.Push((point.x, point.y - 1));
                stack.Push((point.x, point.y + 1));
                stack.Push((point.x - 1, point.y));
                stack.Push((point.x + 1, point.y));
            }
        }

        return size;
    }

    private void Initialize(string[] lines)
    {
        width = lines[0].Length;
        height = lines.Length;
        heightMap = Enumerable.Repeat(9, (width + 2) * (height + 2)).ToArray();



        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                int mapIndex = MapIndex(x, y);
                heightMap[mapIndex] = int.Parse(lines[y][x].ToString());
            }
        }
    }

    private int CalculateRisk()
    {
        var risk = 0;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var adjacent = Math.Min(
                    Math.Min(heightMap[MapIndex(x, y - 1)], heightMap[MapIndex(x, y + 1)]),
                    Math.Min(heightMap[MapIndex(x - 1, y)], heightMap[MapIndex(x + 1, y)])
                );

                int cellHeight = heightMap[MapIndex(x, y)];
                if (adjacent > cellHeight)
                {
                    risk += 1 + cellHeight;
                }
            }
        }

        return risk;
    }

    private int MapIndex(int x, int y) => (y + 1) * (width + 2) + (x + 1);
}
