using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Helpers;
using UOCApp.Models;
using Xamarin.Forms;

namespace UOCApp
{
	public partial class LeaderboardPage : ContentPage
	{
		HttpClient client;
		GetResultsHelper resultsHelper;

		List<LeaderboardResult> baseResults = new List<LeaderboardResult>();
		ObservableCollection<LeaderboardResult> results = new ObservableCollection<LeaderboardResult>();

		bool official;

		public LeaderboardPage()
		{
			InitializeComponent();

			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;

			resultsHelper = new GetResultsHelper(client, App.API_URL);

			ListViewLeaderboard.ItemsSource = results;

			official = false;

		}

		protected override async void OnAppearing() //is this safe?
		{
			//initial get


			//baseResults = resultsHelper.ConvertLeaderboardResults(await resultsHelper.GetRawResults(""));

			//CopyResults();

			GetResults();
		}

		private async void GetResults()
		{
			//build the querystring with the help of the helper
			string selectedPeriod = !(PickerPeriod == null) ? PickerPeriod.Items[PickerPeriod.SelectedIndex] : "Include All";
			string selectedGrade = !(PickerGrade == null) ? PickerGrade.Items[PickerGrade.SelectedIndex] : "Include All";
			string selectedGender = !(PickerGender == null) ? PickerGender.Items[PickerGender.SelectedIndex] : "Include All";
			string selectedSchool = null;
			string query = resultsHelper.CreateQueryString(selectedPeriod, selectedGrade, selectedGender, selectedSchool, official);

			//try to get the count
			try
			{

				int count = await resultsHelper.GetCount(query);

				//Console.WriteLine(count);

				UpdateDescription(count);

			}
			catch (Exception) //pokemon exception handling
			{
				//Console.WriteLine("Caught exception " + e.Message);
				await DisplayAlert("Alert", "An unexpected error occurred while getting the count", "OK");
			}

			//try to get the results list
			try
			{
				//see how elegant using the helper makes this?

				//Console.WriteLine(await resultsHelper.GetCount(query));

				List<RawResult> rawresults = await resultsHelper.GetRawResults(query);

				this.baseResults = GetResultsHelper.ConvertLeaderboardResults(rawresults);

				//copy results
				CopyResults();


			}
			catch (Exception) //pokemon exception handling
			{
				//Console.WriteLine("Caught exception " + e.Message);
				await DisplayAlert("Alert", "An unexpected error occurred while getting the list", "OK");
			}

		}

		private void CopyResults()
		{
			this.results.Clear();

			foreach (LeaderboardResult result in this.baseResults)
			{
				this.results.Add(result);
			}
		}

		private void UpdateDescription(int count)
		{
			//update count and text
			if (!official)
			{
				LabelDescription.Text = String.Format("All results ({0} total)", count);
			}
			else
			{
				LabelDescription.Text = String.Format("Official results ({0} total)", count);
			}
		}

		//on filter change refresh, results
		private void FilterChange(object sender, EventArgs args)
		{
			//sanity check
			if (PickerGrade == null)
				return;

			GetResults();
		}

		private void ButtonOfficialClick(object sender, EventArgs args)
		{

			if (official)
			{
				ButtonOfficial.Text = "Show Official";
				//LabelDescription.Text = "All results";
				official = false;
			}
			else
			{
				ButtonOfficial.Text = "Show All";
				//LabelDescription.Text = "Official results";
				official = true;
			}

			GetResults();
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

		private void NavAbout(object sender, EventArgs args)
		{
			Navigation.PushAsync(new AboutPage());
		}

		private void NavLeaderboard(object sender, EventArgs args)
		{
			Navigation.PushAsync(new LeaderboardPage());
		}
	}
}
