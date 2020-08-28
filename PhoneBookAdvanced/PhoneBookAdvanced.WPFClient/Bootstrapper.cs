using Caliburn.Micro;
using PhoneBookAdvanced.Interfaces;
using PhoneBookAdvanced.Model;
using PhoneBookAdvanced.Services;
using PhoneBookAdvanced.WPFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhoneBookAdvanced.WPFClient
{
	public class Bootstrapper : BootstrapperBase
	{
		private SimpleContainer _container = new SimpleContainer();

		public Bootstrapper()
		{
			Initialize();
		}

		protected override void Configure()
		{
			base.Configure();

			_container.Singleton<IWindowManager, WindowManager>();
			_container.Singleton<IEventAggregator, EventAggregator>();
			_container.Instance<IPhoneContactService>(new PhoneContactService(new HttpClient()));
			_container.Singleton<PhoneBookViewModel>();
			_container.Singleton<MainWindowViewModel>();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			base.OnStartup(sender, e);

			IDictionary<string, object> settings = new Dictionary<string, object>();
			settings.Add("SizeToContent", SizeToContent.Manual);
			settings.Add("Width", 800);
			settings.Add("Height", 600);
			settings.Add("SnapToDevicePixels", true);

			DisplayRootViewFor<MainWindowViewModel>(settings);
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			return _container.GetInstance(serviceType, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.GetAllInstances(serviceType);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}

		public async void Test()
		{
			PhoneContactService bob = new PhoneContactService(new HttpClient());
			PhoneRoot contacts = await bob.GetContacts();
			foreach (PhoneContact contact in contacts.contacts)
			{
				System.Diagnostics.Debug.WriteLine(contact.name);
			}
		}
	}
}
