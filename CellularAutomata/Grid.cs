using System.Text;

namespace CellularAutomata;

public class Grid
{
    private char[,] _grid;
    private readonly Random _generator = new();
    const char Wall = '#';
    const char Floor = ' ';

    public Grid(int numberOfRows, int numberOfColumns)
    {
        _grid = new char[numberOfRows, numberOfColumns];
        for (int row = 0; row < _grid.GetLength(0); row++)
        {
            for (int col = 0; col < _grid.GetLength(1); col++)
            {
               _grid[row, col] = Floor;
            }
        }
    }

    public void GenerateNoise(int wallDensity)
    {
        int randomNumber;
        for (int row = 0; row < _grid.GetLength(0); row++)
        {
            for (int col = 0; col < _grid.GetLength(1); col++)
            {
                randomNumber = _generator.Next(100);
                if(randomNumber < wallDensity)
                {
                    _grid[row, col] = Wall;
                }
            }
        }
    }

    public void ApplyCellularAutomata(int numberOfWalls=4)
    {
        int wallCount;
        char[,] temp = new char[_grid.GetLength(0), _grid.GetLength(1)];
        for (int row = 0; row < _grid.GetLength(0); row++)
        {
            for (int col = 0; col < _grid.GetLength(1); col++)
            {
                temp[row, col] = Floor;
                wallCount = GetWallCount(row, col);
                if(wallCount > numberOfWalls)
                {
                    temp[row, col] = Wall;
                }
            }
        }
        _grid = temp;
    }

    private int GetWallCount(int row, int col)
    {
        int wallCount = 0;
        int numberOfRows = _grid.GetLength(0);
        int numberOfColumns = _grid.GetLength(1);
        int rowAbove = row - 1;
        int rowBelow = row + 1;
        int colLeft = col - 1;
        int colRight = col + 1;
        for(var r = rowAbove; r <= rowBelow; r++)
        {
            for(var c = colLeft; c <= colRight; c++)
            {
                if (r == row && c == col) continue;
                if (r < 0 || r >= numberOfRows)
                {
                    wallCount++;
                    continue;
                }
                if(c < 0 || c >= numberOfColumns)
                {
                    wallCount++;
                    continue;
                }
                if (_grid[r, c] == Wall) wallCount++;
            }
        }
        return wallCount;
    }


    public override string ToString()
    {
        StringBuilder builder = new();
        for(int row = 0; row < _grid.GetLength(0); row++)
        {
            for(int col = 0; col < _grid.GetLength(1); col++)
            {
                builder.Append(_grid[row,col]);
            }
            builder.Append('\n');
        }
        return builder.ToString();
    }
}
