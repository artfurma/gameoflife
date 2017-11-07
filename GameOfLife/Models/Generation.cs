using System;
using System.Text;

namespace GameOfLife.Models
{
	public class Generation
	{
		private readonly Cell[,] _cells;
		private readonly int _rows;
		private readonly int _columns;

		public Generation(int size)
		{
			_cells = new Cell[size, size];
			InitCells();
		}

		public Generation(int rows, int columns, string[] inputMap)
		{
			_rows = rows;
			_columns = columns;
			_cells = new Cell[_rows, _columns];
			ParseMap(inputMap);
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

		public Cell GetCell(int row, int column)
		{
			if (row < 0 || row >= _cells.GetLength(0) || column < 0 || column >= _cells.GetLength(0))
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

		public void ParseMap(string[] inputMap)
		{
			for (var row = 0; row < _rows; row++)
			{
				for (var column = 0; column < _columns; column++)
				{
					var cellChar = inputMap[row][column];
					CellState cellState;

					switch (cellChar)
					{
						case 'A':
							cellState = CellState.Alive;
							break;
						case 'D':
							cellState = CellState.Dead;
							break;
						default:
							cellState = CellState.Empty;
							break;
					}

					_cells[row, column] = new Cell(new Tuple<int, int>(row, column), cellState);
				}
			}
		}

		//		public override string ToString()
		//		{
		//			StringBuilder gridString = new StringBuilder();
		//
		//			for (int row = 0; row < ; row++)
		//			{
		//				for (int column = 0; column < UniverseSize; column++)
		//				{
		//					gridString.Append(
		//						string.Format("{0} ", GetCell(row, column).Alive ? "1" : "0")
		//					);
		//				}
		//
		//				gridString.AppendLine();
		//			}
		//
		//			return gridString.ToString();
		//		}
	}
}