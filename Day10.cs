class Day10
{
    public void Run(string[] lines)
    {
        var corruptionScore = 0L;
        var incompleteScores = new List<long>();

        foreach (var line in lines)
        {
            var validation = Validate(line);
            if (validation.status == Validation.Corrupted)
            {
                corruptionScore += CorruptionValue(validation.tokens[0]);
            }
            else if (validation.status == Validation.Incomplete)
            {
                incompleteScores.Add(
                    IncompletionValue(validation.tokens)
                );
            }
        }

        var incompletionScore = incompleteScores
            .OrderBy(x => x)
            .Skip((incompleteScores.Count - 1) / 2)
            .First();

        Console.WriteLine($"Corrupt = {corruptionScore}, Incomplete = {incompletionScore}");
    }

    private (Validation status, string tokens) Validate(string line)
    {
        var OPENS = new List<char> { '<', '[', '{', '(' };
        var CLOSE = new List<char> { '>', ']', '}', ')' };


        var stack = new Stack<char>();
        foreach (var token in line)
        {
            if (OPENS.Contains(token))
            {
                var expectedClose = CLOSE[OPENS.IndexOf(token)];
                stack.Push(expectedClose);
            }
            else if (CLOSE.Contains(token))
            {
                if (!stack.Any())
                    return (Validation.Corrupted, token.ToString());

                var expectedClose = stack.Pop();
                if (token != expectedClose)
                    return (Validation.Corrupted, token.ToString());
            }
        }

        if (stack.Any())
        {
            return (Validation.Incomplete, string.Join("", stack.ToArray()));
        }
        else
        {
            return (Validation.Valid, "");
        }
    }

    private int CorruptionValue(char token)
    {
        return token switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0
        };
    }

    private long IncompletionValue(string tokens)
    {
        var score = 0L;
        foreach (var token in tokens)
        {
            score *= 5;
            score += token switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => 0
            };
        }

        return score;
    }

    enum Validation
    {
        Valid,
        Incomplete,
        Corrupted
    }
}
