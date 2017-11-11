using System;
using System.Text;
using GameOfLife.Helpers;

namespace GameOfLife.Models
{
	public class Generation
	{
		private readonly Cell[,] _cells;
		public int Rows { get; set; }
		public int Columns { get; set; }

		public Generation(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			_cells = new Cell[Rows, Columns];
			InitCells();
		}

		public Generation(int rows, int columns, Cell[,] cells)
		{
			Rows = rows;
			Columns = columns;
			_cells = cells;
			InitCells();
		}

		private void InitCells()
		{
			for (var row = 0; row < Rows; row++)
			{
				for (var column = 0; column < Columns; column++)
				{
					_cells[row, column] = new Cell(new Tuple<int, int>(row, column), CellState.Empty);
				}
			}
		}

		public Cell GetCell(int row, int column)
		{
			if (row < 0 || row >= Rows || column < 0 || column >= Columns)
			{
				return null;
			}

			return _cells[row, column];
		}

		public void SetCell(int row, int column, CellState state)
		{
			var cell = GetCell(row, column);

			if (cell == null)
			{
				throw new ArgumentOutOfRangeException();
			}

			cell.State = state;
		}

		public void Reset()
		{
			for (var row = 0; row < _cells.GetLength(0); row++)
			{
				for (var column = 0; column < _cells.GetLength(0); column++)
				{
					SetCell(row, column, CellState.Empty);
				}
			}
		}

		public void UpdateCellState(int row, int column)
		{
			var cell = GetCell(row, column);

			if (cell.State == CellState.Empty || cell.State == CellState.Dead)
			{
				cell.State = CellState.Alive;
			}
			else
			{
				cell.State = CellState.Dead;
			}
		}

		public override string ToString()
		{
			var gridString = new StringBuilder();
			gridString.AppendLine($"{Columns},{Rows}");

			for (var row = 0; row < Rows; row++)
			{
				for (var column = 0; column < Columns; column++)
				{
					gridString.Append(GetCell(row, column).State.ToNamePrefix());
				}

				gridString.AppendLine();
			}

			return gridString.ToString();
		}
	}
}