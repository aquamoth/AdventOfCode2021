class Day04
{
    public void Run(string[] lines)
    {
        var bingoNumbers = lines[0].Split('\u002C').Select(int.Parse).ToArray();

        var boards = new List<Board>();
        var index = 1;
        while (index < lines.Length)
        {
            index++;
            var board = new Board(lines, index);
            boards.Add(board);
            index += 5;
        }

        for (int i = 0; i < bingoNumbers.Length; i++)
        {
            var number = bingoNumbers[i];
            for (var boardIndex = boards.Count - 1; boardIndex >= 0; boardIndex--)
            {
                Board board = boards[boardIndex];
                board.Set(number);
                if (board.IsWinner)
                {
                    if (boards.Count() > 1)
                    {
                        Console.WriteLine($"{number}: Removing board {boardIndex}");
                        boards.Remove(board);
                    }
                    else
                    {
                        Console.WriteLine($"{number}: Last board {boardIndex}");
                        var sum = board.DoSum();
                        Console.WriteLine($"{sum} * {number} = {sum * number}");
                        return;

                    }

                }
            }
        }
    }


    private class Board
    {
        private int[][] rows;

        private bool[] sets = new bool[25];

        public Board(string[] allRows, int index)
        {
            var rows = allRows
                .Skip(index)
                .Take(5)
                .ToArray();

            var data = rows.Select(row => row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
            this.rows = data;
        }

        public bool IsWinner
        {
            get
            {
                for (var y = 0; y < 5; y++)
                {
                    if (sets[y * 5] && sets[y * 5 + 1] && sets[y * 5 + 2] && sets[y * 5 + 3] && sets[y * 5 + 4])
                        return true;
                }

                for (var y = 0; y < 5; y++)
                {
                    if (sets[y] && sets[y + 5] && sets[y + 10] && sets[y + 15] && sets[y + 20])
                        return true;
                }

                return false;
            }
        }

        internal void Set(int number)
        {
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (rows[x][y] == number)
                    {
                        sets[x * 5 + y] = true;
                    }
                }
            }
        }

        public long DoSum()
        {
            var sum = 0L;
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (!sets[x * 5 + y])
                    {
                        sum += rows[x][y];
                    }
                }
            }
            return sum;
        }
    }
}
