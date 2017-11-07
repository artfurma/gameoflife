using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLifeControls
{
	public partial class Cell : UserControl
	{
		public new static readonly DependencyProperty BackgroundProperty =
			DependencyProperty.Register("Background", typeof(Brush), typeof(Cell),
				new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));

		public new Brush Background
		{
			get => (Brush) GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}

		public Cell()
		{
			InitializeComponent();
		}
	}
}