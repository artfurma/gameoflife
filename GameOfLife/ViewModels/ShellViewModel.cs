using Caliburn.Micro;
using GameOfLife.Models;

namespace GameOfLife.ViewModels
{
	public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
	{
		private readonly IWindowManager _windowManager;

		public ShellViewModel(IWindowManager windowManager)
		{
			_windowManager = windowManager;
			ActivateItem(new GameViewModel());
		}
	}
}