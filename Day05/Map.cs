using System.Drawing;
namespace Day05;

class Map
{
    public Map(Point start, Point end)
    {
        Start = start;
        End = end;
        Area = EmptyMap(start, end);
    }

    public Point Start { get; }
    public Point End { get; }
    public int[] Area { get; }

    public void Draw(Line line)
    {
        if (line.IsVertical)
        {
            for (var y = line.From.Y; y <= line.To.Y; y++)
            {
                int index = Index(line.From.X, y);
                Area[index]++;
            }

        }
        else
        {
            for (var x = line.From.X; x <= line.To.X; x++)
            {
                var y = line.From.Y + line.K * (x - line.From.X);
                int index = Index(x, y);
                Area[index]++;
            }
        }
    }

    private int Index(int x, int y) => IndexFrom(x, y, Start, End);
    private static int IndexFrom(int x, int y, Point start, Point end)
    {
        return x - start.X + (y - start.Y) * (end.X - start.X + 1);
    }

    private static int[] EmptyMap(Point start, Point end)
    {
        int locations = (end.X - start.X + 1) * (end.Y - start.Y + 1);
        return new int[locations];
    }
}
