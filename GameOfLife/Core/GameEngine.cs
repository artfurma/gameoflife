using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfLife.Models;

namespace GameOfLife.Core
{
	public class GameEngine
	{
		public int UnderpopulationRule { get; set; }
		public int OverpopulationRule { get; set; }
		public int ReproductionRule { get; set; }

		public Generation ActiveGeneration { get; set; }

		public int GenerationNumber { get; set; }

		public GameEngine(Generation generation)
		{
			InitGameRules();
			ActiveGeneration = generation;
			GenerationNumber = 1;
		}

		public void InitGameRules()
		{
			UnderpopulationRule = 2;
			OverpopulationRule = 3;
			ReproductionRule = 3;
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

			for (var row = 0; row < ActiveGeneration.Rows; row++)
			{
				for (var column = 0; column < ActiveGeneration.Columns; column++)
				{
					var cell = ActiveGeneration.GetCell(row, column);
					var neighbours = GetNumberOfCellNeighbours(cell, ActiveGeneration);

					if (cell.State == CellState.Alive && (neighbours < UnderpopulationRule || neighbours > OverpopulationRule))
					{
						modifiedCells.Add(new Tuple<Tuple<int, int>, CellState>(cell.Position, CellState.Dead));
					}
					else if ((cell.State == CellState.Dead || cell.State == CellState.Empty) && neighbours == ReproductionRule)
					{
						modifiedCells.Add(new Tuple<Tuple<int, int>, CellState>(cell.Position, CellState.Alive));
					}
				}
			}

			if (modifiedCells.Any())
			{
				GenerationNumber++;

				Parallel.ForEach(modifiedCells,
					tuple => ActiveGeneration.SetCell(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2));
			}
		}

		public void Reset()
		{
			GenerationNumber = 1;
			ActiveGeneration.Reset();
		}

		public Cell GetCell(int row, int column)
		{
			return ActiveGeneration.GetCell(row, column);
		}

		public void SetCell(int row, int column, CellState state)
		{
			ActiveGeneration.SetCell(row, column, state);
		}

		public void UpdateCellState(int row, int column)
		{
			ActiveGeneration.UpdateCellState(row, column);
		}

		public void ImportGeneration(Generation generation)
		{
			ActiveGeneration = generation;
			GenerationNumber = 1;
		}
	}
}