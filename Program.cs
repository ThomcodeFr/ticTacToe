namespace TicTacToe;

internal class Program
{
    const char PlayerOne = 'O';
    const char PlayerTwo = 'X';

    static void Main(string[] args)
    {

        List<Cell> grid = new List<Cell>()
        {
            Cell.EmptyCell(1, 1),
            Cell.EmptyCell(1, 2),
            Cell.EmptyCell(1, 3),
            Cell.EmptyCell(2, 1),
            Cell.EmptyCell(2, 2),
            Cell.EmptyCell(2, 3),
            Cell.EmptyCell(3, 1),
            Cell.EmptyCell(3, 2),
            Cell.EmptyCell(3, 3),
        };
        char currentPlayer = PlayerOne;

        DisplayGameBoard(grid);
        while (true)
        {
            string[]? splittedInput = PlayerMoveInstruction(currentPlayer);
            if (splittedInput == null)
                break;

            if (!InvalidTagedCell(grid, currentPlayer, splittedInput))
                continue;

            Console.Clear();

            EndingGame(grid, currentPlayer);

            currentPlayer = currentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
        }
    }

    /// <summary>
    /// Ending the game. 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    private static bool EndingGame(List<Cell> grid, char currentPlayer)
    {
        DisplayGameBoard(grid);

        if (IsGameBoardWin(grid))
        {
            Console.WriteLine($"Player {currentPlayer} has won the game !!!!");
            return false;
        }
        if (IsGameBoardFull(grid))
        {
            Console.WriteLine($"It's a draw");
            return false;
        }
        return true;
    }

    /// <summary>
    /// What's happen if player choose wrong cell.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="currentPlayer"></param>
    /// <param name="splittedInput"></param>
    /// <returns></returns>
    private static bool InvalidTagedCell(List<Cell> grid, char currentPlayer, string[]? splittedInput)
    {
        if (int.TryParse(splittedInput?[0], out int targetRow) is false ||
            targetRow < 1 || targetRow > 3)
        {
            Console.WriteLine("Invalid target cell row must be betwen 1 and 3");
            return false;
        }

        if (int.TryParse(splittedInput?[1], out int targetColumn) is false || targetColumn < 1 || targetColumn > 3)
        {
            Console.WriteLine("Invalid target cell column must be betwen 1 and 3");
            return false;
        }

        bool movePlayedSuccessfully = PlayOnGameBoard(grid, targetRow, targetColumn, currentPlayer);

        if (movePlayedSuccessfully is false)
        {
            Console.WriteLine("Invalid move");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Instruction for player to move.
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    private static string[]? PlayerMoveInstruction(char currentPlayer)
    {
        Console.WriteLine($"Player {currentPlayer} - Enter row (1-3) and column (1-3), separated by a space, or 'q' to quit...");
        string? input = Console.ReadLine();

        if (string.Compare(input, "q", StringComparison.OrdinalIgnoreCase) == 0)
            return null; 
        return input?.Split(' ');
    }

    /// <summary>
    /// Display the game board.
    /// </summary>
    /// <param name="grid"></param>
    private static void DisplayGameBoard(List<Cell> grid)
    {
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.WriteLine(".NET MAUI".PadLeft(Console.WindowWidth / 2));
        Console.WriteLine(new string('=', Console.WindowWidth));

        Console.WriteLine($"|-----------------|");
        DisplayGameBoardLine(GetCellContent(grid, 1, 1), GetCellContent(grid, 1, 2), GetCellContent(grid, 1, 3));
        Console.WriteLine($"|-----|-----|-----|");
        DisplayGameBoardLine(GetCellContent(grid, 2, 1), GetCellContent(grid, 2, 2), GetCellContent(grid, 2, 3));
        Console.WriteLine($"|-----|-----|-----|");
        DisplayGameBoardLine(GetCellContent(grid, 3, 1), GetCellContent(grid, 3, 2), GetCellContent(grid, 3, 3));
        Console.WriteLine($"|-----------------|");
    }

    /// <summary>
    /// Get the cell.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    private static Cell? GetCell(List<Cell> grid, int row, int column)
        => grid
            .Where(cell => cell.Row == row)
            .Where(cell => cell.Column == column)
            .FirstOrDefault();

    /// <summary>
    /// Get the content of the cell.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    private static char GetCellContent(List<Cell> grid, int row, int column)
        => GetCell(grid, row, column)?.Value ?? ' ';

    /// <summary>
    /// Display the game board line.
    /// </summary>
    /// <param name="leftCell"></param>
    /// <param name="middleCell"></param>
    /// <param name="rightCell"></param>
    private static void DisplayGameBoardLine(char leftCell, char middleCell, char rightCell)
    {
        Console.WriteLine($"|  {leftCell}  |  {middleCell}  |  {rightCell}  |");
    }
    /// <summary>
    /// Play on the game board.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="targetRow"></param>
    /// <param name="targetColumn"></param>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    private static bool PlayOnGameBoard(List<Cell> grid, int targetRow, int targetColumn, char currentPlayer)
    {
        Cell? cell = GetCell(grid, targetRow, targetColumn);

        if (cell == null || cell.Value == PlayerOne || cell.Value == PlayerTwo)
            return false;

        cell.UpdateValue(currentPlayer);
        return true;
    }

    /// <summary>
    /// Check if the game board is full.
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static bool IsGameBoardWin(List<Cell> grid)
    {
        IEnumerable<IGrouping<int, Cell>> rows = grid
            .GroupBy(cell => cell.Row);

        if (rows.Any(row =>
            row.All(cell => cell.Value == PlayerOne) ||
            row.All(cell => cell.Value == PlayerTwo)))
        {
            return true;
        }

        IEnumerable<IGrouping<int, Cell>> columns = grid
            .GroupBy(cell => cell.Column);

        if (columns.Any(column =>
            column.All(cell => cell.Value == PlayerOne) ||
            column.All(cell => cell.Value == PlayerTwo)))
        {
            return true;
        }

        IEnumerable<Cell> firstDiagonal = grid.Where(c => c.Row == c.Column);
        IEnumerable<Cell> secondDiagonal = grid.Where(c => (c.Row + c.Column) == 4);

        var diagonals = new List<IEnumerable<Cell>>
        {
            firstDiagonal,
            secondDiagonal
        };

        if (diagonals.Any(diagonal =>
            diagonal.All(cell => cell.Value == PlayerOne) ||
            diagonal.All(cell => cell.Value == PlayerTwo)))
        {
            return true;
        }

        return false;
    }

    private static bool IsGameBoardFull(List<Cell> grid)
        => grid.All(cell => cell.Value.HasValue);
}
