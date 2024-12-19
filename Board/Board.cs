using CSharpFunctionalExtensions;
using ticTacToe.Players;
using TicTacToe;

namespace ticTacToe.Board
{
    internal class Board
    {
        public List<Cell> grid { get; private set; } = new List<Cell>();

        internal void Init()
        {
            grid = new List<Cell>()
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
        }

        internal void DisplayGameBoardAndHeader()
        {
            Console.Clear();
            DisplayGameHeader();
            DisplayGameBoard();
        }

        private void DisplayGameHeader()
        {
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine(".NET M2I".PadLeft(Console.WindowWidth / 2));
            Console.WriteLine(new string('=', Console.WindowWidth));
        }

        private void DisplayGameBoard()
        {
            Console.WriteLine($"|-----|-----|-----|");
            DisplayGameBoardLine(1);
            Console.WriteLine($"|-----|-----|-----|");
            DisplayGameBoardLine(2);
            Console.WriteLine($"|-----|-----|-----|");
            DisplayGameBoardLine(3);
            Console.WriteLine($"|-----|-----|-----|");
        }

        private void DisplayGameBoardLine(int row)
        {
            Console.WriteLine($"|  {GetCellContent(row, 1)}  |  {GetCellContent(row, 2)}  |  {GetCellContent(row, 3)}  |");
        }

        private char GetCellContent(int row, int column)
            => GetCell(row, column)?.Value ?? ' ';

        private Cell? GetCell(int row, int column)
            => grid
                .Where(cell => cell.Row == row)
                .Where(cell => cell.Column == column)
                .FirstOrDefault();

        internal bool PlayMoveOnBoard(PlayerMoves playerMoves, char currentPlayerIcon)
        {
            Cell? cell = GetCell(playerMoves.Row, playerMoves.Column);

            if (cell == null || cell.Value == Game.PlayerOneIcon || cell.Value == Game.PlayerTwoIcon)
                return false;

            cell.UpdateValue(currentPlayerIcon);
            return true;
        }

        internal Maybe<string> IsGameOver(IPlayer currentPlayer)
        {
            if (IsGameBoardWin())
            {
                return Maybe.From($"Player {currentPlayer} has won the game !!!!");
            }
            if (IsGameBoardFull())
            {
                return Maybe.From($"it's a draw!");
            }

            return Maybe.None;
        }


        public bool IsGameBoardWin()
        {
            IEnumerable<IGrouping<int, Cell>> rows = grid
                .GroupBy(cell => cell.Row);

            if (rows.Any(row =>
                row.All(cell => cell.Value == Game.PlayerOneIcon) ||
                row.All(cell => cell.Value == Game.PlayerTwoIcon)))
            {
                return true;
            }

            IEnumerable<IGrouping<int, Cell>> columns = grid
                .GroupBy(cell => cell.Column);

            if (columns.Any(column =>
                column.All(cell => cell.Value == Game.PlayerOneIcon) ||
                column.All(cell => cell.Value == Game.PlayerTwoIcon)))
            {
                return true;
            }

            IEnumerable<Cell> firstDiagonal = grid.Where(c => c.Row == c.Column);
            IEnumerable<Cell> secondDiagonal = grid.Where(c => c.Row + c.Column == 4);

            var diagonals = new List<IEnumerable<Cell>>
        {
            firstDiagonal,
            secondDiagonal
        };

            if (diagonals.Any(diagonal =>
                diagonal.All(cell => cell.Value == Game.PlayerOneIcon) ||
                diagonal.All(cell => cell.Value == Game.PlayerTwoIcon)))
            {
                return true;
            }

            return false;
        }

        private bool IsGameBoardFull()
            => grid.All(cell => cell.Value.HasValue);
    }
}