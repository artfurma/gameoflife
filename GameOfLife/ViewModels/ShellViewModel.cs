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

		private readonly IWindowManager _windowManager; 
		private IScreen activeSscreen;

		public ShellViewModel(IWindowManager windowManager)
		{
			activeSscreen = IoC.Get<GameViewModel>();
			_windowManager = windowManager;
			ActivateItem(activeSscreen);
			StopVisibility = Visibility.Collapsed;
			StartVisibility = Visibility.Visible;
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
		}
	}
}