// See https://aka.ms/new-console-template for more information
using System.Drawing;
class Program
{
    public static async Task Main()
    {
        var lines = await File.ReadAllLinesAsync("test.txt");

        var sut = new Day14();
        sut.Run(lines);
    }
}
