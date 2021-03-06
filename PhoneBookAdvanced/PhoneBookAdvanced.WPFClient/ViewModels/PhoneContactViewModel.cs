﻿using Caliburn.Micro;
using PhoneBookAdvanced.Model;

namespace PhoneBookAdvanced.WPFClient.ViewModels
{
	public class PhoneContactViewModel : Screen
	{
		public readonly PhoneContact Contact;
		public string ContactName
		{
			get
			{
				return (Contact.name);
			}
		}

		public string ContactNumber
		{
			get
			{
				return (Contact.phone_number);
			}
		}

		public string ContactAddress
		{
			get
			{
				return (Contact.address);
			}
		}

		public PhoneContactViewModel(PhoneContact contact)
		{
			Contact = contact;
			NotifyOfPropertyChange(nameof(ContactName));
			NotifyOfPropertyChange(nameof(ContactNumber));
			NotifyOfPropertyChange(nameof(ContactAddress));
		}
	}
}
