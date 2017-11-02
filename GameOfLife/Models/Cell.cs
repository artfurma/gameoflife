using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Models
{
	public enum CellState
	{
		Empty,
		Alive,
		Dead
	}

	public class Cell
	{
		public Tuple<int, int> Position { get; set; }
		public CellState State { get; set; }

		public Cell(Tuple<int, int> position, CellState state)
		{
			Position = position;
			State = state;
		}
	}
}