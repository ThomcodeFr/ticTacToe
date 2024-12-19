using CSharpFunctionalExtensions;
using ticTacToe.Board;
using TicTacToe;

namespace ticTacToe.Players;

internal class Player : IPlayer
{
    public char Icon { get; private set; }

    public Player(char icon)
    {
        Icon = icon;
    }

    public Result<PlayerMoves> GetNextMove(List<Cell> grid)
    {
        Console.WriteLine($"Player {Icon} - Enter row (1-3) and column (1-3), separated by a space");
        string? input = Console.ReadLine();

        string[]? splittedInput = input?.Split(' ');

        if (int.TryParse(splittedInput?[0], out int targetRow) is false ||
            targetRow < 1 || targetRow > 3)
        {
            return Result.Failure<PlayerMoves>("Invalid target cell row must be betwen 1 and 3");
        }

        if (int.TryParse(splittedInput?[1], out int targetColumn) is false ||
            targetColumn < 1 || targetColumn > 3)
        {
            return Result.Failure<PlayerMoves>("Invalid target cell column must be betwen 1 and 3");
        }

        return Result.Success(new PlayerMoves(targetRow, targetColumn));
    }
}