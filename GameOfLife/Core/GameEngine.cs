using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Models;

namespace GameOfLife.Core
{
	public class GameEngine
	{
		private Generation _activeGeneration;
		private int _gameSize;

		public int GenerationNumber { get; set; }

		public GameEngine(int gameSize)
		{
			_gameSize = gameSize;
			_activeGeneration = new Generation(_gameSize);
		}
	}
}