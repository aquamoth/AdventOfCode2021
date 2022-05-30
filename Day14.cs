using System.Text;
using System;

class Day14
{
    public void Run(string[] lines)
    {
        Dictionary<string, string> formulas = new Dictionary<string, string>();

        var polymer = lines[0];

        foreach (var line in lines.Skip(2))
        {
            var parts = line.Split(' ');
            formulas.Add(parts[0], parts[2]);
        }


        Dictionary<string, long> polymerParts = new Dictionary<string, long>();
        for (var i = 0; i < polymer.Length - 2; i++)
        {
            polymerParts.Add(polymer.Substring(i, 2), 1);
        }



        for (int i = 0; i < 10; i++)
        {
            var newParts = new Dictionary<string, long>();
            foreach (var part in polymerParts)
            {
                if (formulas.TryGetValue(part.Key, out var result))
                {
                    foreach (var p1 in new[] { part.Key[0] + result, result + part.Key[1] })
                    {
                        if (polymerParts.ContainsKey(p1))
                            polymerParts[p1]++;
                        else if (newParts.ContainsKey(p1))
                            newParts[p1]++;
                        else
                            newParts.Add(p1, 1);
                    }
                }
            }
            foreach(var part in newParts)
            {
                polymerParts.Add(part.Key, part.Value);
            }

            //for (var x = polymer.Length - 1; x >= 1; x--)
            //{
            //    var pair = polymer.Substring(x - 1, 2);
            //    if (formulas.ContainsKey(pair))
            //    {
            //        polymer = string.Concat(polymer.AsSpan(0, x), formulas[pair], polymer.AsSpan(x));
            //    }
            //}
            //Console.WriteLine($"After step {i + 1}: {polymer.Length}");
        }

        var partCounts = polymerParts
            .SelectMany(x => new[] { (name: x.Key[0], count: x.Value), (name: x.Key[1], count: x.Value) })
            .GroupBy(x => x.name)
            .ToDictionary(x => x.Key, x => x.Sum(xx => xx.count));

        var min = partCounts.Min(x => x.Value);
        var max = partCounts.Max(x => x.Value);
        //var counts = polymer.Select(x => x).GroupBy(x => x).ToDictionary(grp => grp.Key, grp => grp.LongCount());
        //var min = counts.Min(x => x.Value);
        //var max = counts.Max(x => x.Value);
        Console.WriteLine($"{max} - {min} = {max - min}");
    }
}
