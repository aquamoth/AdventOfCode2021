class Day03
{
    public void Run(string[] lines)
    {
        var oxygen = Bin2Dec(FilterLines(lines, false));
        var co2 = Bin2Dec(FilterLines(lines, true));

        Console.WriteLine($"{oxygen} * {co2} = {oxygen*co2}");
    }



    static int[] bitCommonality(string[] lines)
    {
        var bits = new int[lines[0].Length];
        foreach (var line in lines)
        {
            for (var index = 0; index < line.Length; index++)
            {
                var bit = line[index];
                if (bit == '0')
                    bits[index]--;
                else
                    bits[index]++;
            }
        }
        return bits;
    }

    static string FilterLines(string[] lines, bool isCO2)
    {
        var oxygen = lines.ToList();
        for (var bitIndex = 0; bitIndex < oxygen[0].Length; bitIndex++)
        {
            var bits = bitCommonality(oxygen.ToArray());
            var expected = isCO2 ^ bits[bitIndex] < 0 ? '0' : '1';
            for (var lineIndex = oxygen.Count - 1; lineIndex >= 0; lineIndex--)
            {
                if (oxygen[lineIndex][bitIndex] != expected)
                    oxygen.RemoveAt(lineIndex);
            }

            if (oxygen.Count == 1)
                break;
        }

        return oxygen.Single();
    }

    private static int Bin2Dec(string co2)
    {
        var gammarate = 0;
        foreach (var bit in co2)
        {
            gammarate *= 2;
            if (bit == '1') gammarate++;
        }

        return gammarate;
    }
}
