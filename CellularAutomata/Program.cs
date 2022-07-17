using CellularAutomata;

Grid grid = new(40, 80);
grid.GenerateNoise(40);
string? answer;
do
{
    Console.WriteLine(grid);
    Console.WriteLine("<enter> = iterate, X = exit");
    answer = Console.ReadLine();
    if(answer != null && answer == "")
    {
        grid.ApplyCellularAutomata(3);
    }
}while (answer != null && answer.ToLower() != "x");

