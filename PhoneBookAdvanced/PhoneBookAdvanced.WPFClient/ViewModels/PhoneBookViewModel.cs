using Caliburn.Micro;
using PhoneBookAdvanced.Interfaces;
using PhoneBookAdvanced.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class PhoneBookViewModel : Screen
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

		public PhoneBookViewModel(IPhoneContactService contactservice, IEventAggregator eventaggregator)
		{
			_ContactService = contactservice;
			_EventAggregator = eventaggregator;

			Contacts = new BindableCollection<PhoneContactViewModel>();
		}

		public async void LoadContacts()
		{
			contacts.Clear();
			PhoneRoot contactlist = await _ContactService.GetContacts();
			if (contactlist != null)
			{
				contacts = contactlist.contacts;
				FilterContacts();
			}
		}

		public void FilterContacts()
		{
			Contacts.Clear();
			foreach (PhoneContact contact in contacts)
			{
				Contacts.Add(new PhoneContactViewModel(contact));
			}
		}

		public void EditContact(PhoneContactViewModel model)
		{
			//TODO: Add PhoneContactEditViewModel that takes a PhoneContact object.
		}

		public void DeleteContact(PhoneContactViewModel model)
		{
			contacts.Remove(model.Contact);
			Contacts.Remove(model);
		}
	}
}
