﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Lands2
{
    using Xamarin.Forms;
    using Lands2.Views;
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            //MainPage = new LoginPage();
            this.MainPage = new NavigationPage(new LoginPage());
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
