using System;
using GameOfLife.Models;

namespace GameOfLife.Helpers
{
	public static class CellStateExtension
	{
		public static string ToNamePrefix(this CellState me)
		{
			switch (me)
			{
				case CellState.Alive:
					return "A";
				case CellState.Dead:
					return "D";
				case CellState.Empty:
					return "E";
				default:
					return "E";
			}
		}
	}
}