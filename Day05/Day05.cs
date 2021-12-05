using System.Drawing;
namespace Day05;

class Day05
{
    public void Run(string[] lines)
    {
        var vectors = lines.Select(Line.Parse).ToArray();
        var ortoLines = vectors.Where(v => v.From.X == v.To.X || v.From.Y == v.To.Y).ToArray();

        var (start, end) = FindBounds(vectors);
        var map = new Map(start, end);

        foreach (var line in vectors)
        {
            map.Draw(line);
        }

        Console.Write(map.Area.Count(intersections => intersections > 1));
    }

    private static (Point start, Point end) FindBounds(Line[] lines)
    {
        var points = lines
            .SelectMany(v => new[] { v.From, v.To })
            .ToArray();

        var start = new Point(points.Min(p => p.X), points.Min(p => p.Y));
        var end = new Point(points.Max(p => p.X), points.Max(p => p.Y));

        return (start, end);
    }
}
