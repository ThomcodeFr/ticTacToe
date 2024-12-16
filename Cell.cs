namespace TicTacToe;

using System;

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
        this.Row = row;
        this.Column = column;
        this.Value = value;
    }
    public Cell(int row, int column)
    {
        this.Row = row;
        this.Column = column;
        this.Value = null;
    }

    internal void UpdateValue(char value)
    {
        this.Value = value;
    }

    internal static Cell EmptyCell(int row, int column)
        => new Cell(row, column);
}
