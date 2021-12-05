using System.Drawing;
namespace Day05;

record Line
{
    public Point From;
    public Point To;
    public bool IsVertical => this.From.X == this.To.X;
    public bool IsHorizontal => this.From.Y == this.To.Y;

    public int K => (To.Y - From.Y) / (To.X - From.X);

    internal bool HasPoint(int x, int y)
    {
        if (IsVertical)
        {
            if (x == From.X)
            {
                return (y >= From.Y && y <= To.Y) || (y >= To.Y && y <= From.Y);
            }
            else
            {
                return false;
            }
        }
        else if (IsHorizontal)
        {
            if (y == From.Y)
            {
                return (x >= From.X && x <= To.X) || (x >= To.X && x <= From.X);
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (!(x >= From.X && x <= To.X) || (x >= To.X && x <= From.X))
                return false;
            if (!(y >= From.Y && y <= To.Y) || (y >= To.Y && y <= From.Y))
                return false;

            var expected = From.Y + K * (x - From.X);
            return (expected == y);
        }
    }


    public static Line Parse(string vectorString)
    {
        var points = vectorString.Split(' ', StringSplitOptions.TrimEntries).ToList();
        var from = MakePoint(points[0]);
        var to = MakePoint(points[2]);

        if (from.X < to.X)
            return new Line { From = from, To = to };
        else if (from.X > to.X)
            return new Line { From = to, To = from };
        else if (from.Y < to.Y)
            return new Line { From = from, To = to };
        else
            return new Line { From = to, To = from };
    }

    private static Point MakePoint(string pointString)
    {
        var point = pointString.Split(',');
        return new Point(int.Parse(point[0]), int.Parse(point[1]));
    }
}
