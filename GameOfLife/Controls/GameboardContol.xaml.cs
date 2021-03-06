﻿using System;
using System.Collections.Generic;
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
		private IList<Border> _cells;

		#region Dependency Properties

		public static readonly DependencyProperty RowsProperty =
			DependencyProperty.Register("Rows", typeof(int), typeof(GameboardContol),
				new FrameworkPropertyMetadata(1, RowsPropertyChangedHandler));

		public static readonly DependencyProperty ColumnsProperty =
			DependencyProperty.Register("Columns", typeof(int), typeof(GameboardContol),
				new FrameworkPropertyMetadata(1, ColumnsPropertyChangedHandler));

		public static readonly DependencyProperty AliveColorProperty =
			DependencyProperty.Register("AliveColor", typeof(SolidColorBrush), typeof(GameboardContol),
				new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(43, 62, 80)), ThemeChangedHandler));

		public static readonly DependencyProperty DeadColorProperty =
			DependencyProperty.Register("DeadColor", typeof(SolidColorBrush), typeof(GameboardContol),
				new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(64, 204, 244)), ThemeChangedHandler));

		public int Rows
		{
			get => (int) GetValue(RowsProperty);
			set => SetValue(RowsProperty, value);
		}

		public int Columns
		{
			get => (int) GetValue(ColumnsProperty);
			set => SetValue(ColumnsProperty, value);
		}

		public SolidColorBrush AliveColor
		{
			get => (SolidColorBrush) GetValue(AliveColorProperty);
			set => SetValue(AliveColorProperty, value);
		}

		public SolidColorBrush DeadColor
		{
			get => (SolidColorBrush) GetValue(DeadColorProperty);
			set => SetValue(DeadColorProperty, value);
		}

		#endregion

		public GameboardContol()
		{
			InitializeComponent();
			_gameViewModel = IoC.Get<GameViewModel>();
			_cells = new List<Border>();
			InitializeGameGrid(_gameViewModel);
		}

		private static void RowsPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var gameboard = (GameboardContol) sender;
			gameboard.GameGrid.Children.Clear();
			gameboard._cells.Clear();

			for (var row = 0; row < gameboard._gameViewModel.GameRows; row++)
			{
				for (var column = 0; column < gameboard._gameViewModel.GameColumns; column++)
				{
					var cell = gameboard._gameViewModel.GetCell(row, column);
					if (cell == null)
						break;
					var cellControl = gameboard.InitializeCell(cell);
					gameboard.GameGrid.Children.Add(cellControl);
				}
			}
		}

		private static void ColumnsPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var gameboard = (GameboardContol) sender;
			gameboard.GameGrid.Children.Clear();
			gameboard._cells.Clear();

			for (var row = 0; row < gameboard._gameViewModel.GameRows; row++)
			{
				for (var column = 0; column < gameboard._gameViewModel.GameColumns; column++)
				{
					var cell = gameboard._gameViewModel.GetCell(row, column);
					if (cell == null)
						break;
					var cellControl = gameboard.InitializeCell(cell);
					gameboard.GameGrid.Children.Add(cellControl);
				}
			}
		}

		private static void ThemeChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var gameboard = (GameboardContol)sender;

			foreach (var cell in gameboard._cells)
			{
				cell.SetBinding(BackgroundProperty, new Binding
				{
					Path = new PropertyPath("State"),
					Mode = BindingMode.TwoWay,
					Converter = new CellStateToColorConverter(emptyColor: Brushes.White,
						aliveColor: gameboard.AliveColor,
						deadColor: gameboard.DeadColor)
				});
			}
		}
		private void InitializeGameGrid(GameViewModel gameViewModel)
		{
			for (var row = 0; row < gameViewModel.GameRows; row++)
			{
				for (var column = 0; column < gameViewModel.GameColumns; column++)
				{
					var cell = gameViewModel.GetCell(row, column);
					var cellControl = InitializeCell(cell);
					GameGrid.Children.Add(cellControl);
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
					aliveColor: AliveColor,
					deadColor: DeadColor)
			});

			_cells.Add(cellControl);
			return cellControl;
		}
	}
}