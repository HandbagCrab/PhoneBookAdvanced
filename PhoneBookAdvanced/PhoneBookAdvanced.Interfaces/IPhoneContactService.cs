using PhoneBookAdvanced.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookAdvanced.Interfaces
{
	public interface IPhoneContactService
	{
		Task<PhoneRoot> GetContacts();
	}
}
