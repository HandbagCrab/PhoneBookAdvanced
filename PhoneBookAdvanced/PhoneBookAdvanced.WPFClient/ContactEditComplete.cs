using PhoneBookAdvanced.Model;

namespace PhoneBookAdvanced.WPFClient
{
	public class ContactEditComplete
	{
		public PhoneContact Contact { get; private set; }
		public bool IsNew { get; private set; }
		public bool EditCancelled { get; private set; }

		public ContactEditComplete(PhoneContact contact, bool isnew, bool editcancelled)
		{
			Contact = contact;
			IsNew = isnew;
			EditCancelled = editcancelled;
		}
	}
}
