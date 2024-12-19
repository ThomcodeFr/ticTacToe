namespace ticTacToe.Board;

/// <summary>
/// Description for a Cell
/// </summary>
internal class Cell
{
    public int Row { get; private set; }
    public int Column { get; private set; }
    public char? Value { get; private set; }

    public Cell(int row, int column, char value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
        Value = null;
    }

    public bool IsEmpty => Value == null;

    internal void UpdateValue(char value)
    {
        Value = value;
    }

    internal static Cell EmptyCell(int row, int column)
        => new Cell(row, column);
}
