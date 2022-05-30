class Day13
{
    public void Run(string[] lines)
    {
        HashSet<(int x, int y)> points = new HashSet<(int x, int y)>();


        bool parseFolds = false;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                parseFolds = true;
            }
            else if (parseFolds)
            {
                var fold = line.Substring("fold along ".Length).Split('=');
                var foldLine = int.Parse(fold[1]);

                if (fold[0] == "x")
                {
                    var pointsToMove = points.Where(p => p.y > foldLine).ToList();
                    points.RemoveWhere(p => p.y > foldLine);

                    foreach (var point in pointsToMove)
                    {
                        points.Add((point.x, foldLine + foldLine - point.y));
                    }
                }
                else if (fold[0] == "y")
                {
                    var pointsToMove = points.Where(p => p.x > foldLine).ToList();
                    points.RemoveWhere(p => p.x > foldLine);

                    foreach (var point in pointsToMove)
                    {
                        points.Add((foldLine + foldLine - point.x, point.y));
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }

                //break;
            }
            else
            {
                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                points.Add((x, y));
            }
        }

        Console.WriteLine($"{points.Count} points left");
    }
}
