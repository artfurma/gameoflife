using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using GameOfLife.Core;
using GameOfLife.Helpers;
using GameOfLife.Models;

namespace GameOfLife.ViewModels
{
	public class GameViewModel : Screen
	{
		private readonly GameEngine _gameEngine;

		#region Binded Properties

		private int _currentGenerationNumber;
		private int _populationCount;
		private bool _canReset;
		private bool _canPerformNextGeneration;
		private bool _canUpdateCellStatus;
		private bool _running;
		private int _mapWidth;
		private int _mapHeight;

		public int CurrentGenerationNumber
		{
			get => _currentGenerationNumber;
			set
			{
				_currentGenerationNumber = value;
				NotifyOfPropertyChange(() => CurrentGenerationNumber);
			}
		}

		public int PopulationCount
		{
			get => _populationCount;
			set
			{
				_populationCount = value;
				NotifyOfPropertyChange(() => PopulationCount);
			}
		}

		public bool CanReset
		{
			get => _canReset;
			set
			{
				_canReset = value;
				NotifyOfPropertyChange(() => CanReset);
			}
		}

		public bool CanPerformNextGeneration
		{
			get => _canPerformNextGeneration;
			set
			{
				_canPerformNextGeneration = value;
				NotifyOfPropertyChange(() => CanPerformNextGeneration);
			}
		}

		public bool CanUpdateCellState
		{
			get => _canUpdateCellStatus;
			set
			{
				_canUpdateCellStatus = value;
				NotifyOfPropertyChange(() => CanUpdateCellState);
			}
		}

		public bool Running
		{
			get => _running;
			set
			{
				_running = value;
				NotifyOfPropertyChange(() => Running);
			}
		}

		public int MapWidth
		{
			get => _mapWidth;
			set
			{
				_mapWidth = value;
				NotifyOfPropertyChange(() => MapWidth);
			}
		}

		public int MapHeight
		{
			get => _mapHeight;
			set
			{
				_mapHeight = value;
				NotifyOfPropertyChange(() => MapHeight);
			}
		}
		#endregion

		#region Commands

		public ICommand NextGenerationCommand { get; }
		public ICommand ResetCommand { get; }
		public ICommand UpdateCellStateCommand { get; }

		#endregion

		public GameViewModel()
		{
			var gameSize = 100;
			_gameEngine = new GameEngine(gameSize);

			CanReset = true;
			CanPerformNextGeneration = true;
			CanUpdateCellState = true;
			CurrentGenerationNumber = _gameEngine.GenerationNumber;


			NextGenerationCommand = new RelayCommand<object>(_ => NextGeneration(), _ => CanPerformNextGeneration);
			ResetCommand = new RelayCommand<object>(_ => Reset(), _ => CanReset);
			UpdateCellStateCommand =
				new RelayCommand<object>(UpdateCellState, _ => CanUpdateCellState);
		}

		public void NextGeneration()
		{
			_gameEngine.NextGeneration();
			CurrentGenerationNumber = _gameEngine.GenerationNumber;
		}

		public void Reset()
		{
			_gameEngine.Reset();
			CurrentGenerationNumber = 1;
		}

		public Cell GetCell(int row, int column)
		{
			return _gameEngine.GetCell(row, column);
		}

		public void UpdateCellState(object rowColumnTuple)
		{
			var rowColumn = (Tuple<int, int>) rowColumnTuple;
			_gameEngine.UpdateCellState(rowColumn.Item1, rowColumn.Item2);
		}

		public async void Start()
		{
			Running = true;

			while (Running)
			{
				await Task.Delay(50);
				NextGeneration();
			}
		}

		public void Stop()
		{
			Running = false;
		}

		public void Import(string[] gameMap)
		{
			var mapSize = gameMap[0].Split(',');
			var width = int.Parse(mapSize[0]);
			var height = int.Parse(mapSize[1]);

			var map = gameMap.Skip(1);
			_gameEngine.ImportGeneration(height, width, map);
		}
	}
}