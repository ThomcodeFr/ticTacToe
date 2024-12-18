using CSharpFunctionalExtensions;
using TicTacToe;

namespace ticTacToe
{
    internal class StupidIA : IPlayer
    {
        public char icon => 'O';

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


