using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameOfLife.Models;

namespace GameOfLife.Controls
{
	/// <summary>
	/// Interaction logic for CellControl.xaml
	/// </summary>
	public partial class CellControl : UserControl
	{
		public static readonly DependencyProperty BackgroundProperty =
			DependencyProperty.Register("Background", typeof(Brush), typeof(CellControl),
				new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));

		public Brush Background
		{
			get => (Brush)GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}

		public CellControl()
		{
			InitializeComponent();
		}
	}
}
