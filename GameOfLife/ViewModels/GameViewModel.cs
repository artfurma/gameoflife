using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using ControlzEx.Microsoft.Windows.Shell;
using GameOfLife.Core;
using GameOfLife.Helpers;
using GameOfLife.Models;

namespace GameOfLife.ViewModels
{
	public class GameViewModel : Screen
	{
		private readonly GameEngine _gameEngine;
		private readonly int _maxSpeed;

		#region Binded Properties

		private int _currentGenerationNumber;
		private int _populationCount;
		private bool _canReset;
		private bool _canPerformNextGeneration;
		private bool _canUpdateCellStatus;
		private bool _running;
		private int _gameRows;
		private int _gameColumns;
		private int _currentSpeed;
		private int _speedIndicator;
		private int _currentSize;

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

		public int GameRows
		{
			get => _gameRows;
			set
			{
				_gameRows = value;
				NotifyOfPropertyChange(() => GameRows);
			}
		}

		public int GameColumns
		{
			get => _gameColumns;
			set
			{
				_gameColumns = value;
				NotifyOfPropertyChange(() => GameColumns);
			}
		}

		public int CurrentSpeed
		{
			get => _currentSpeed;
			set
			{
				_currentSpeed = value;
				SpeedIndicator = value;
				NotifyOfPropertyChange(() => CurrentSpeed);
			}
		}

		public int SpeedIndicator
		{
			get => _speedIndicator;
			set
			{
				_speedIndicator = value;
				NotifyOfPropertyChange((() => SpeedIndicator));
			}
		}

		public int CurrentSize
		{
			get => _currentSize;
			set
			{
				_currentSize = value;
				ResizeGameMap();
				NotifyOfPropertyChange(() => CurrentSpeed);
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
			//TODO: Zamienić to na binding
			GameRows = 10;
			GameColumns = 20;

			NextGenerationCommand = new RelayCommand<object>(_ => NextGeneration(), _ => CanPerformNextGeneration);
			ResetCommand = new RelayCommand<object>(_ => Reset(), _ => CanReset);
			UpdateCellStateCommand = new RelayCommand<object>(UpdateCellState, _ => CanUpdateCellState);

			_gameEngine = new GameEngine(new Generation(GameRows, GameColumns));
			CurrentGenerationNumber = _gameEngine.GenerationNumber;

			CanReset = true;
			CanPerformNextGeneration = true;
			CanUpdateCellState = true;
			_maxSpeed = 120;
			CurrentSpeed = 50;
			CurrentSize = 20;
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
				var speed = _maxSpeed - CurrentSpeed;
				await Task.Delay(speed);
				NextGeneration();
			}
		}

		public void Stop()
		{
			Running = false;
		}

		public Generation ParseMap(int rows, int columns, string[] inputMap)
		{
			var resultCellMap = new Cell[rows, columns];
			for (var row = 0; row < rows; row++)
			{
				for (var column = 0; column < columns; column++)
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

					resultCellMap[row, column] = new Cell(new Tuple<int, int>(row, column), cellState);
				}
			}

			return new Generation(rows, columns, resultCellMap);
		}

		public void Import(string[] gameMap)
		{
			var mapSize = gameMap[0].Split(',');
			var columns = int.Parse(mapSize[0]);
			var rows = int.Parse(mapSize[1]);
			var map = gameMap.Skip(1).ToArray();

			var generation = ParseMap(rows, columns, map);
			_gameEngine.ImportGeneration(generation);
			GameRows = rows;
			GameColumns = columns;
		}

		public string Export()
		{
			return _gameEngine.ActiveGeneration.ToString();
		}

		public async void ResizeGameMap()
		{
			var newRows = 1 * CurrentSize;
			var newColumns = 2 * CurrentSize;

			await Task.Run(() =>
			{
				var generation = new Generation(newRows, newColumns);
				_gameEngine.ImportGeneration(generation);
				GameRows = newRows;
				GameColumns = newColumns;
			});

			Stop();
		}
	}
}