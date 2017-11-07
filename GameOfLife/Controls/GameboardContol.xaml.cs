using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using GameOfLife.Helpers;
using GameOfLife.Models;
using GameOfLife.ViewModels;

namespace GameOfLife.Controls
{
	/// <summary>
	/// Interaction logic for GameboardContol.xaml
	/// </summary>
	public partial class GameboardContol : UserControl
	{
		private readonly GameViewModel _gameViewModel;

		public int Rows
		{
			get => (int) GetValue(RowsProperty);
			set => SetValue(RowsProperty, value);
		}

		public static readonly DependencyProperty RowsProperty =
			DependencyProperty.Register("Rows", typeof(int), typeof(GameboardContol), new PropertyMetadata(50));

		public int Columns
		{
			get => (int) GetValue(ColumnsProperty);
			set => SetValue(ColumnsProperty, value);
		}

		public static readonly DependencyProperty ColumnsProperty =
			DependencyProperty.Register("Columns", typeof(int), typeof(GameboardContol), new PropertyMetadata(100));

		public GameboardContol()
		{
			InitializeComponent();
			_gameViewModel = IoC.Get<GameViewModel>();
			InitializeGameboard();
		}

		private void InitializeGameboard()
		{
			for (var row = 0; row < Rows; row++)
			{
				GameGrid.RowDefinitions.Add(new RowDefinition());
			}

			for (var column = 0; column < Columns; column++)
			{
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}

			for (var row = 0; row < Rows; row++)
			{
				for (var column = 0; column < Columns; column++)
				{
					var cell = InitializeCell(_gameViewModel.GetCell(row, column));
					Grid.SetRow(cell, row);
					Grid.SetColumn(cell, column);
					GameGrid.Children.Add(cell);
				}
			}
		}

		private Border InitializeCell(Cell cell)
		{
			var cellControl = new Border {DataContext = cell, Style = Resources["CellControlStyle"] as Style};
			cellControl.InputBindings.Add(new InputBinding(_gameViewModel.UpdateCellStateCommand,
					new MouseGesture(MouseAction.LeftClick))
				{CommandParameter = new Tuple<int, int>(cell.Position.Item1, cell.Position.Item2)});
			cellControl.SetBinding(BackgroundProperty, new Binding
			{
				Path = new PropertyPath("State"),
				Mode = BindingMode.TwoWay,
				Converter = new CellStateToColorConverter(emptyColor: Brushes.White,
					aliveColor: new SolidColorBrush(Color.FromRgb(43, 62, 80)),
					deadColor: new SolidColorBrush(Color.FromRgb(45, 117, 163)))
			});

			return cellControl;
		}
	}
}