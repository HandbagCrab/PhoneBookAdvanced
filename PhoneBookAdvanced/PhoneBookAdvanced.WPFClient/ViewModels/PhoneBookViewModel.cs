using Caliburn.Micro;
using PhoneBookAdvanced.Interfaces;
using PhoneBookAdvanced.Model;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class PhoneBookViewModel : Screen, IHandle<ContactEditComplete>
	{
		private IPhoneContactService _ContactService;
		private IEventAggregator _EventAggregator;
		private List<PhoneContact> contacts = new List<PhoneContact>();

		private BindableCollection<PhoneContactViewModel> _Contacts;
		public BindableCollection<PhoneContactViewModel> Contacts
		{
			get
			{
				return (_Contacts);
			}
			set
			{
				_Contacts = value;
				NotifyOfPropertyChange();
			}
		}

		private string _Filter = string.Empty;
		public string Filter
		{
			get
			{
				return (_Filter);
			}
			set
			{
				_Filter = value;
				NotifyOfPropertyChange();
				DisplayContacts();
			}
		}

		private SortType ContactSortType = SortType.None;

		public PhoneBookViewModel(IPhoneContactService contactservice, IEventAggregator eventaggregator)
		{
			_ContactService = contactservice;
			_EventAggregator = eventaggregator;
			_EventAggregator.Subscribe(this);

			Contacts = new BindableCollection<PhoneContactViewModel>();
		}

		public async void LoadContacts()
		{
			contacts.Clear();
			PhoneRoot contactlist = await _ContactService.GetContacts();
			if (contactlist != null)
			{
				contacts = contactlist.contacts;
				DisplayContacts();
			}
		}

		public void DisplayContacts()
		{
			Contacts.Clear();
			foreach (PhoneContact contact in FilterContacts())
			{
				Contacts.Add(new PhoneContactViewModel(contact));
			}
		}

		private List<PhoneContact> FilterContacts()
		{
			string filter = _Filter.ToLower();
			IEnumerable<PhoneContact> filtered = contacts.Where(c => c.name.ToLower().Contains(filter) ||
			c.address.ToLower().Contains(filter) ||
			c.phone_number.ToLower().Contains(filter));

			if (ContactSortType == SortType.Alphabetical)
				filtered = filtered.OrderBy(f => f.name);
			else if (ContactSortType == SortType.ReverseAlphabetical)
				filtered = filtered.OrderByDescending(f => f.name);

			return (filtered.ToList());
		}

		public void SortAlpha()
		{
			if (ContactSortType != SortType.Alphabetical)
			{
				ContactSortType = SortType.Alphabetical;
				DisplayContacts();
			}
		}

		public void SortReverseAlpha()
		{
			if (ContactSortType != SortType.ReverseAlphabetical)
			{
				ContactSortType = SortType.ReverseAlphabetical;
				DisplayContacts();
			}
		}

		public void DeleteContact(PhoneContactViewModel model)
		{
			contacts.Remove(model.Contact);
			Contacts.Remove(model);
		}

		public void AddContact()
		{
			_EventAggregator.PublishOnUIThread(new PhoneBookEditViewModel(new PhoneContact(), _EventAggregator));
		}

		public void EditContact(PhoneContactViewModel model)
		{
			_EventAggregator.PublishOnUIThread(new PhoneBookEditViewModel(model.Contact, _EventAggregator));
		}

		public void Handle(ContactEditComplete message)
		{
			if (!message.EditCancelled)
			{
				if (message.IsNew)
				{
					contacts.Add(message.Contact);
				}

				DisplayContacts();
			}
		}
	}
}
