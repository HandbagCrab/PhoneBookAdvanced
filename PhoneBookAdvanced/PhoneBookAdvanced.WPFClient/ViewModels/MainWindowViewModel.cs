using Caliburn.Micro;
using System.Linq;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class MainWindowViewModel : Conductor<Screen>.Collection.OneActive, IHandle<PhoneBookEditViewModel>, IHandle<ContactEditComplete>
	{
		private readonly IEventAggregator _EventAggregator;
		private PhoneBookViewModel PhoneBookVM;

		public MainWindowViewModel(PhoneBookViewModel phonebook, IEventAggregator eventaggregator)
		{
			_EventAggregator = eventaggregator;
			_EventAggregator.Subscribe(this);

			DisplayName = "Phonebook Advanced";

			PhoneBookVM = phonebook;
			Items.Add(PhoneBookVM);
			ActiveItem = PhoneBookVM;
		}

		public void Handle(PhoneBookEditViewModel message)
		{
			Items.Add(message);
			ActiveItem = message;
		}

		public void Handle(ContactEditComplete message)
		{
			while (Items.OfType<PhoneBookEditViewModel>().Count() > 0)
			{
				Items.Remove(Items.OfType<PhoneBookEditViewModel>().First());
			}

			ActiveItem = PhoneBookVM;
		}
	}
}
