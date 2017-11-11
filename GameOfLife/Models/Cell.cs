using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Annotations;

namespace GameOfLife.Models
{
	public enum CellState
	{
		[Description]
		Empty,
		Alive,
		Dead
	}

	public class Cell : INotifyPropertyChanged
	{
		private CellState _state;

		public event PropertyChangedEventHandler PropertyChanged;

		public Tuple<int, int> Position { get; set; }

		public CellState State
		{
			get => _state;
			set
			{
				_state = value;
				OnPropertyChanged(nameof(State));
			}
		}

		public Cell(Tuple<int, int> position, CellState state)
		{
			Position = position;
			State = state;
		}

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}