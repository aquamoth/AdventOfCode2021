// See https://aka.ms/new-console-template for more information
using System.Drawing;
class Program
{
    public static async Task Main()
    {
        var lines = await File.ReadAllLinesAsync("input.txt");

        var sut = new Day05.Day05();
        sut.Run(lines);
    }
}
