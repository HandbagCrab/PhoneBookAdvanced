using Caliburn.Micro;
using PhoneBookAdvanced.Interfaces;
using PhoneBookAdvanced.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class MainWindowViewModel : Conductor<Screen>.Collection.OneActive
	{
		private readonly IEventAggregator _EventAggregator;

		public MainWindowViewModel(PhoneBookViewModel phonebook, IEventAggregator eventaggregator)
		{
			_EventAggregator = eventaggregator;

			DisplayName = "Phonebook Advanced";

			Items.Add(phonebook);
			ActiveItem = phonebook;
		}
	}
}
