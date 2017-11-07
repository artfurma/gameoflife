using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using GameOfLife.Models;

namespace GameOfLife.Helpers
{
	internal class CellStateToColorConverter : IValueConverter
	{
		public SolidColorBrush EmptyColor { get; }
		public SolidColorBrush AliveColor { get; }
		public SolidColorBrush DeadColor { get; }

		public CellStateToColorConverter(SolidColorBrush emptyColor, SolidColorBrush aliveColor, SolidColorBrush deadColor)
		{
			EmptyColor = emptyColor;
			AliveColor = aliveColor;
			DeadColor = deadColor;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var state = CellState.Empty;

			if (value is CellState cellState)
				state = cellState;

			switch (state)
			{
				case CellState.Empty:
					return EmptyColor;
				case CellState.Alive:
					return AliveColor;
				case CellState.Dead:
					return DeadColor;
				default:
					return EmptyColor;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var color = EmptyColor;

			if (value is SolidColorBrush brush)
				color = brush;

			if (Equals(color, AliveColor)) return CellState.Alive;
			return Equals(color, DeadColor) ? CellState.Dead : CellState.Empty;
		}
	}
}