using Caliburn.Micro;
using PhoneBookAdvanced.Model;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class PhoneBookEditViewModel : Screen
	{
		private readonly IEventAggregator _EventAggregator;

		public PhoneContact Contact { get; private set; }

		private string _ContactName;
		public string ContactName
		{
			get
			{
				return (_ContactName);
			}
			set
			{
				_ContactName = value;
				NotifyOfPropertyChange();
			}
		}

		private string _ContactAddress;
		public string ContactAddress
		{
			get
			{
				return (_ContactAddress);
			}
			set
			{
				_ContactAddress = value;
				NotifyOfPropertyChange();
			}
		}

		private string _ContactPhoneNumber;
		public string ContactPhoneNumber
		{
			get
			{
				return (_ContactPhoneNumber);
			}
			set
			{
				_ContactPhoneNumber = value;
				NotifyOfPropertyChange();
			}
		}

		public bool IsNew { get; private set; }

		public PhoneBookEditViewModel(PhoneContact contact, IEventAggregator eventaggregator)
		{
			_EventAggregator = eventaggregator;

			IsNew = string.IsNullOrWhiteSpace(contact.name) && string.IsNullOrWhiteSpace(contact.address) && string.IsNullOrWhiteSpace(contact.phone_number);

			Contact = contact;
			ContactName = contact.name;
			ContactAddress = contact.address;
			ContactPhoneNumber = contact.phone_number;
		}

		public void SaveContact()
		{
			Contact.name = ContactName;
			Contact.address = ContactAddress;
			Contact.phone_number = ContactPhoneNumber;

			_EventAggregator.PublishOnUIThread(new ContactEditComplete(Contact, IsNew, false));
		}

		public void CancelSave()
		{
			_EventAggregator.PublishOnUIThread(new ContactEditComplete(Contact, IsNew, true));
		}

	}
}
