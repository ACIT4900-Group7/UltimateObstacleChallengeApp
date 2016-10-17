using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UOCApp.Helpers;
using Xamarin.Forms;
using SQLite;

namespace UOCApp
{
	public class App : Application
	{

        //safe to put constants here?
        public const string password = "Cl1ffdr1ve";
        public const string API_URL = @"http://www.cliffdriveobstaclecourse.com/index.php/api/uocb/";
        public static SwearCheckerHelper swearHelper;
        public static DatabaseHelper databaseHelper;

        //TODO refactor App.x stuff into separate Singleton
        //for now try (Application)(App.Current).databaseHelper.db

		public App ()
		{
            swearHelper = new SwearCheckerHelper();
            databaseHelper = new DatabaseHelper();

            // The root page of your application
            MainPage = new NavigationPage(new UOCApp.StopwatchPage());
		}



		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
