using CSharpFunctionalExtensions;
using ticTacToe.Board;

namespace ticTacToe.Players
{
    public class StupidIA : IPlayer
    {
        public char Icon => 'O';

        public Result<PlayerMoves> GetNextMove(List<Cell> grid)
        {
            var firstEmptyCell = grid.FirstOrDefault(cell => cell.IsEmpty);

            if (firstEmptyCell == null)
            {
                return Result.Failure<PlayerMoves>("No valid moves available.");
            }

            // Retourner la ligne et la colonne correspondantes
            return Result.Success(new PlayerMoves(firstEmptyCell.Row, firstEmptyCell.Column));
        }
    }
}


