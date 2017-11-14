using System;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using GameOfLife.Models;
using Microsoft.Win32;

namespace GameOfLife.ViewModels
{
	public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
	{
		private Visibility _startVisibility;
		private int _underpopulationRule;
		private int _overpopulationRule;
		private int _birthRule;
		private int _step;

		public Visibility StartVisibility
		{
			get => _startVisibility;
			set
			{
				_startVisibility = value;
				NotifyOfPropertyChange(() => StartVisibility);
			}
		}

		private Visibility _stopVisibility;

		public Visibility StopVisibility
		{
			get => _stopVisibility;
			set
			{
				_stopVisibility = value;
				NotifyOfPropertyChange(() => StopVisibility);
			}
		}

		public int UnderpopulationRule
		{
			get => _underpopulationRule;
			set
			{
				_underpopulationRule = value;
				ChangeUnderpopulationRule();
				NotifyOfPropertyChange(() => UnderpopulationRule);
			}
		}

		public int OverpopulationRule
		{
			get => _overpopulationRule;
			set
			{
				_overpopulationRule = value;
				ChangeOverpopulationRule();
				NotifyOfPropertyChange(() => OverpopulationRule);
			}
		}

		public int BirthRule
		{
			get => _birthRule;
			set
			{
				_birthRule = value;
				ChangeBirthRule();
				NotifyOfPropertyChange(() => BirthRule);
			}
		}

		public int Step
		{
			get => _step;
			set
			{
				_step = value;
				((GameViewModel) activeSscreen).Step = Step;
				NotifyOfPropertyChange(() => Step);
			}
		}

		private readonly IWindowManager _windowManager;
		private IScreen activeSscreen;

		public ShellViewModel(IWindowManager windowManager)
		{
			activeSscreen = IoC.Get<GameViewModel>();
			_windowManager = windowManager;
			ActivateItem(activeSscreen);
			StopVisibility = Visibility.Collapsed;
			StartVisibility = Visibility.Visible;
			UnderpopulationRule = 2;
			OverpopulationRule = 3;
			BirthRule = 3;
			Step = 1;
		}

		public void NextGeneration()
		{
			(activeSscreen as GameViewModel)?.NextGeneration();
		}

		public void Start()
		{
			(activeSscreen as GameViewModel)?.Start();
			StartVisibility = Visibility.Collapsed;
			StopVisibility = Visibility.Visible;
		}

		public void Stop()
		{
			(activeSscreen as GameViewModel)?.Stop();
			StopVisibility = Visibility.Collapsed;
			StartVisibility = Visibility.Visible;
		}

		public void Reset()
		{
			(activeSscreen as GameViewModel)?.Reset();
			StopVisibility = Visibility.Collapsed;
			StartVisibility = Visibility.Visible;
			UnderpopulationRule = 2;
			OverpopulationRule = 3;
			BirthRule = 3;
			Step = 1;
		}

		public void Import()
		{
			// TODO: 1. Zobaczyæ, co  jest nie tak z importem du¿ych mapek
			// TODO: 2. Naprawiæ importowanie przed chwil¹ eksportowanej mapy

			var dalog = new OpenFileDialog {Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki(*.*)|*.*"};
			if (dalog.ShowDialog() != true) return;
			var gameMap = File.ReadAllLines(dalog.FileName);
			(activeSscreen as GameViewModel)?.Import(gameMap);
		}

		public void Export()
		{
			var dialog = new SaveFileDialog
			{
				Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki(*.*)|*.*",
			};

			if (dialog.ShowDialog() == true)
			{
				var exportedMap = (activeSscreen as GameViewModel)?.Export();
				File.WriteAllText(dialog.FileName, exportedMap);
			}
		}

		public void About()
		{
			_windowManager.ShowDialog(IoC.Get<AboutViewModel>());
		}

		public void GliderGun()
		{
			try
			{
				var path = Path.Combine(Environment.CurrentDirectory, @"Resources\Samples\glider-gun.txt");
				var gliderGun = File.ReadAllLines(path);
				(activeSscreen as GameViewModel)?.Import(gliderGun);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
				throw;
			}
		}

		public void Benchmark1()
		{
			try
			{
				var path = Path.Combine(Environment.CurrentDirectory, @"Resources\Samples\benchmark1.txt");
				var gliderGun = File.ReadAllLines(path);
				(activeSscreen as GameViewModel)?.Import(gliderGun);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
				throw;
			}
		}

		public void ChangeTheme()
		{
			(activeSscreen as GameViewModel)?.ChangeTheme();
		}

		public void ChangeUnderpopulationRule()
		{
			((GameViewModel) activeSscreen).UnderpopulationRule = UnderpopulationRule;
		}

		public void ChangeOverpopulationRule()
		{
			((GameViewModel) activeSscreen).OverpopulationRule = OverpopulationRule;
		}

		public void ChangeBirthRule()
		{
			((GameViewModel) activeSscreen).BirthRule = BirthRule;
		}
	}
}