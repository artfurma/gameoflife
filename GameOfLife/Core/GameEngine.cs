using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfLife.Models;

namespace GameOfLife.Core
{
	public class GameEngine
	{
		private Generation _activeGeneration;
		private readonly int _gameSize;
		private readonly int _gameWidth;
		private readonly int _gameHeight;
		private readonly int _underpopulationRule;
		private readonly int _overpopulationRule;
		private readonly int _reproductionRule;

		public int GenerationNumber { get; set; }

		public GameEngine(int gameSize)
		{
			_gameSize = gameSize;
			_underpopulationRule = 2;
			_overpopulationRule = 3;
			_reproductionRule = 3;
			_activeGeneration = new Generation(_gameSize);
			GenerationNumber = 1;
		}

		private int GetNumberOfCellNeighbours(Cell cell, Generation generation)
		{
			var neighbours = new List<Cell>
			{
				generation.GetCell(cell.Position.Item1 - 1, cell.Position.Item2 - 1),
				generation.GetCell(cell.Position.Item1 - 1, cell.Position.Item2),
				generation.GetCell(cell.Position.Item1 - 1, cell.Position.Item2 + 1),
				generation.GetCell(cell.Position.Item1, cell.Position.Item2 - 1),
				generation.GetCell(cell.Position.Item1, cell.Position.Item2 + 1),
				generation.GetCell(cell.Position.Item1 + 1, cell.Position.Item2 - 1),
				generation.GetCell(cell.Position.Item1 + 1, cell.Position.Item2),
				generation.GetCell(cell.Position.Item1 + 1, cell.Position.Item2 + 1)
			};

			var neighboursNo = neighbours.Count(c => c != null && c.State == CellState.Alive);
			return neighboursNo;
		}

		public void NextGeneration()
		{
			var modifiedCells = new List<Tuple<Tuple<int, int>, CellState>>();

			for (var row = 0; row < _gameSize; row++)
			{
				for (var column = 0; column < _gameSize; column++)
				{
					var cell = _activeGeneration.GetCell(row, column);
					var neighbours = GetNumberOfCellNeighbours(cell, _activeGeneration);

					if (cell.State == CellState.Alive && (neighbours < _underpopulationRule || neighbours > _overpopulationRule))
					{
						modifiedCells.Add(new Tuple<Tuple<int, int>, CellState>(cell.Position, CellState.Dead));
					}
					else if ((cell.State == CellState.Dead || cell.State  == CellState.Empty) && neighbours == _reproductionRule)
					{
						modifiedCells.Add(new Tuple<Tuple<int, int>, CellState>(cell.Position, CellState.Alive));
					}
				}
			}

			if (modifiedCells.Any())
			{
				GenerationNumber++;

				Parallel.ForEach(modifiedCells,
					tuple => _activeGeneration.SetCell(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2));
			}
		}

		public void Reset()
		{
			GenerationNumber = 1;
			_activeGeneration.Reset();
		}

		public Cell GetCell(int row, int column)
		{
			return _activeGeneration.GetCell(row, column);
		}

		public void SetCell(int row, int column, CellState state)
		{
			_activeGeneration.SetCell(row, column, state);
		}

		public void UpdateCellState(int row, int column)
		{
			_activeGeneration.UpdateCellState(row, column);
		}

		public void ImportGeneration(int rows, int columns, string[] map)
		{
			_activeGeneration = new Generation(rows, columns, map);
			GenerationNumber = 1;
		}
	}
}