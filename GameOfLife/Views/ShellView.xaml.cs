using System.IO;
using System.Windows;
using Caliburn.Micro;
using GameOfLife.ViewModels;
using Microsoft.Win32;

namespace GameOfLife.Views
{
	/// <summary>
	/// Interaction logic for TestView.xaml
	/// </summary>
	public partial class ShellView : Fluent.RibbonWindow
	{
		private readonly GameViewModel _gameViewModel;

		public ShellView()
		{
			InitializeComponent();
			_gameViewModel = IoC.Get<GameViewModel>();
		}


	}
}