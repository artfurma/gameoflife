using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace GameOfLife.ViewModels
{
	class GameViewModel : Screen
	{

		private int _currentGeneration;

		public int CurrentGeneration
		{
			get => _currentGeneration;
			set
			{
				_currentGeneration = value;
				NotifyOfPropertyChange(nameof(CurrentGeneration));
			}
		}

		private void InitializeGameUI()
		{

		}
	}
}