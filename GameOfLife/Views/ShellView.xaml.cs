using System.Windows;

namespace GameOfLife.Views
{
	/// <summary>
	/// Interaction logic for TestView.xaml
	/// </summary>
	public partial class ShellView : Fluent.RibbonWindow
	{
		public ShellView()
		{
			InitializeComponent();
		}


		private void Exit_OnClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}