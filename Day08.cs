class Day08
{
    readonly List<char> WIRE_LIST = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

    public void Run(string[] lines)
    {
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            var signals = parts[0].Split(' ');
            var display = parts[1].Split(' ');


            HashSet<char> allocatedSegments = new HashSet<char>();

            for(var segment0 ='a';segment0 <= 'g'; segment0++)
            {
                allocatedSegments.Add(segment0);

                for (var segment1 = 'a'; segment1 <= 'g'; segment1++)
                {
                    if (segment1 == segment0) continue;


                }
            }

















            var segmentWires = Enumerable.Range(0, 7).Select(x => new List<char>(WIRE_LIST)).ToArray();
            var numbers = new string[10];


            foreach (var signal in signals)
            {
                var possibleDigits = DigitsFor(signal);
                int[] keepSegments = SegmentsFor(signal);
                var removeSegments = Enumerable.Range(0, 7).Except(keepSegments).ToList();
                var enabledWires = signal.Select(x => x).ToArray();
                var disabledWires = WIRE_LIST.Except(enabledWires).ToArray();

                foreach (var digit in possibleDigits)
                {
                    segmentWires[digit] = segmentWires[digit].Except(disabledWires).ToList();

                    foreach (var index in removeSegments)
                    {
                        segmentWires[index] = segmentWires[index].Except(enabledWires).ToList();
                    }
                }
                if (keepSegments.Any())
                {



                }
            }
        }


        //Console.WriteLine($"{crabs.X} = {crabs.Count} crabs == {crabs.UsedFuel}");
    }

    private static int[] DigitsFor(string signal)
    {
        return signal.Length switch
        {
            2 => new[] { 1 },
            3 => new[] { 7 },
            4 => new[] { 4 },
            5 => new[] { 2, 3, 5 },
            6 => new[] { 0, 6, 9 },
            7 => new[] { 8 },
            _ => throw new NotSupportedException(),
        };
    }
    private static int[] SegmentsFor(string signal)
    {
        return signal.Length switch
        {
            2 => new[] { 2, 5 },                    // digit 1
            3 => new[] { 0, 2, 5 },                 // digit 7
            4 => new[] { 1, 2, 3, 5 },              // digit 4
            5 => new[] { 0, 1, 2, 3, 4, 5, 6 },     // digit 2, 3, 5
            6 => new[] { 0, 1, 2, 3, 4, 5, 6 },     // digit 0, 6, 9
            7 => new[] { 0, 1, 2, 3, 4, 5, 6, 7 },  // digit 8
            _ => throw new NotSupportedException(),
        };
    }
}
