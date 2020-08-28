using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookAdvanced.Model
{
	public class PhoneContact
	{
		public string name { get; set; }
		public string phone_number { get; set; }
		public string address { get; set; }
	}

	public class PhoneRoot
	{
		public List<PhoneContact> contacts { get; set; }
	}
}
