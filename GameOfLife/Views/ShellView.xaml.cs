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
			// TODO: 1. Zobaczyć, co  jest nie tak z importem dużych mapek
			// TODO: 2. Naprawić importowanie przed chwilą eksportowanej mapy

			var dalog = new OpenFileDialog {Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki(*.*)|*.*"};
			if (dalog.ShowDialog() != true) return;
			var gameMap = File.ReadAllLines(dalog.FileName);
			_vm.Import(gameMap);
		}

		public void OnExportButtonClick(object sender, RoutedEventArgs args)
		{
			var dialog = new SaveFileDialog
			{
				Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki(*.*)|*.*",
			};

			if (dialog.ShowDialog() == true)
			{
				var exportedMap = _vm.Export();
				File.WriteAllText(dialog.FileName, exportedMap);
			}
		}

		private void OnAboutButtonClick(object sender, RoutedEventArgs e)
		{
			throw new System.NotImplementedException();
		}
	}
}