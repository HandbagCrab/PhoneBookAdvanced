using Newtonsoft.Json;
using PhoneBookAdvanced.Interfaces;
using PhoneBookAdvanced.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookAdvanced.Services
{
	public class PhoneContactService : IPhoneContactService
	{
		private readonly HttpClient client;
		public string Error { get; set; }
		public PhoneContactService(HttpClient httpclient)
		{
			client = httpclient;
		}

		public async Task<PhoneRoot> GetContacts()
		{
			try
			{
				PhoneRoot root = JsonConvert.DeserializeObject<PhoneRoot>(await client.GetStringAsync("http://www.mocky.io/v2/581335f71000004204abaf83"));
				return (root);
			}
			catch (HttpRequestException e)
			{
				Error = e.Message;
				return (null);
			}
		}
	}
}
