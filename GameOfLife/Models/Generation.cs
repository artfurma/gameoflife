using System;

namespace GameOfLife.Models
{
	public class Generation
	{
		private readonly Cell[,] _cells;

		public Generation(int size)
		{
			_cells = new Cell[size, size];
			InitCells();
		}

		private void InitCells()
		{
			for (var row = 0; row < _cells.GetLength(0); row++)
			{
				for (var column = 0; column < _cells.GetLength(0); column++)
				{
					_cells[row, column] = new Cell(new Tuple<int, int>(row, column), CellState.Empty);
				}
			}
		}

		public Cell GetCell(Tuple<int, int> position)
		{
			if (position.Item1 < 0 || position.Item1 >= _cells.GetLength(0) || position.Item2 < 0 ||
			    position.Item2 >= _cells.GetLength(0))
			{
				return null;
			}

			return _cells[position.Item1, position.Item2];
		}

		public void SetCellState(Tuple<int, int> position, CellState state)
		{
			var cell = GetCell(position);

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
					SetCellState(new Tuple<int, int>(row, column), CellState.Empty);
				}
			}
		}
	}
}