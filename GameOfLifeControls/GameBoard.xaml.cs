using System.Windows;
using System.Windows.Controls;

namespace GameOfLifeControls
{
	/// <summary>
	/// Interaction logic for GameBoard.xaml
	/// </summary>
	public partial class GameBoard : UserControl
	{
		public static readonly DependencyProperty ColumnsCountProperty =
			DependencyProperty.Register("ColumnsCount", typeof(int), typeof(GameBoard), new FrameworkPropertyMetadata(50));

		public static readonly DependencyProperty RowsCountProperty =
			DependencyProperty.Register("RowsCount", typeof(int), typeof(GameBoard), new FrameworkPropertyMetadata(50));

		public int ColumnsCount
		{
			get => (int) GetValue(ColumnsCountProperty);
			set => SetValue(ColumnsCountProperty, value);
		}

		public int RowsCount
		{
			get => (int) GetValue(RowsCountProperty);
			set => SetValue(RowsCountProperty, value);
		}

		public GameBoard()
		{
			InitializeComponent();
			InitGameBoard();
		}

		private void InitGameBoard()
		{
			for (var row = 0; row < RowsCount; row++)
			{
				GameGrid.RowDefinitions.Add(new RowDefinition());
			}

			for (var column = 0; column < ColumnsCount; column++)
			{
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}

			for (var row = 0; row < RowsCount; row++)
			{
				for (var column = 0; column < ColumnsCount; column++)
				{
					var cell = new Cell(); // Może tu przekazać cell? Wtedy biblioteka nie będzie resuable, ale w sumie chuj z tym
					Grid.SetRow(cell, row);
					Grid.SetColumn(cell, column);
					GameGrid.Children.Add(cell);
				}
			}
		}
	}
}