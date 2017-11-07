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
		private GameViewModel _vm;

		public ShellView()
		{
			InitializeComponent();
			_vm = IoC.Get<GameViewModel>();
		}

		public void OnImportButtonClick(object sender, RoutedEventArgs args)
		{
			var openFileDialog = new OpenFileDialog {Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki(*.*)|*.*"};
			if (openFileDialog.ShowDialog() != true) return;
			var gameMap = File.ReadAllLines(openFileDialog.FileName);
			_vm.Import(gameMap);
		}
	}
}