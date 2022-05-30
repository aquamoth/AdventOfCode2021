using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Day12
{
    class Node
    {
        public string Name;
        public bool Visited;
        public bool IsSmall;
        public List<string> Destinations;
    }

    Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    public void Run(string[] lines)
    {
        var paths = BuildPaths(lines);

        nodes = paths
            .GroupBy(x => x.from)
            .Select(grp => new Node { Name = grp.Key, IsSmall = (grp.Key.ToLowerInvariant() == grp.Key), Destinations = grp.Select(x => x.to).ToList() })
            .ToDictionary(x => x.Name);

        var counts = Search("start", 0, false);
        Console.WriteLine(counts.Length);
    }

    private long[] Search(string current, int count, bool hasVisitedSmallTwice)
    {
        var currentNode = nodes[current];
        if (currentNode.Name == "start" && count > 0)
            return Array.Empty<long>();

        var nowVisitedTwice = false;

        if (currentNode.IsSmall && currentNode.Visited)
        {
            if (hasVisitedSmallTwice)
                return Array.Empty<long>();

            hasVisitedSmallTwice = true;
            nowVisitedTwice = true;
        }

        if (currentNode.Name == "end")
            return new long[] { count + 1 };

        if (currentNode.IsSmall)
            currentNode.Visited = true;

        List<long> result = new List<long>();
        foreach (var hop in currentNode.Destinations)
        {
            var length = Search(hop, count + 1, hasVisitedSmallTwice);
            result.AddRange(length);
        }

        if (!nowVisitedTwice)
            currentNode.Visited = false;

        return result.ToArray();
    }

    private static List<(string from, string to)> BuildPaths(string[] lines)
    {
        var paths = new List<(string from, string to)>();

        foreach (var line in lines)
        {
            var parts = line.Split('-');
            paths.Add((parts[0], parts[1]));
            paths.Add((parts[1], parts[0]));
        }

        return paths;
    }
}
