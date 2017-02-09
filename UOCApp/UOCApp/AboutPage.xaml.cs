using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UOCApp
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();
		}

		private void ButtonContactClick(object sender, EventArgs args)
		{
			Device.OpenUri(new Uri("mailto:uocrepoowner@gmail.com"));
		}

		private void NavHome(object sender, EventArgs args)
		{
			Navigation.PopToRootAsync();
		}

		private void NavTimes(object sender, EventArgs args)
		{
			Navigation.PushAsync(new TimesPage());
		}

		private void NavAdmin(object sender, EventArgs args)
		{
			Navigation.PushAsync(new AdminPage());
		}

		private void NavLeaderboard(object sender, EventArgs args)
		{
			Navigation.PushAsync(new LeaderboardPage());
		}

		private void NavAbout(object sender, EventArgs args)
		{
			Navigation.PushAsync(new AboutPage());
		}
	}
}
