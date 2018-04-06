namespace Lands2
{
    using Xamarin.Forms;
    using Lands2.Views;
    using Helpers;
    using ViewModels;
    using Services;
    using Models;

    public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        #endregion
        #region Constructors
        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.Token))
            {
                //MainPage = new LoginPage();
                //this.MainPage = new MasterPage();
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var dataService = new DataService();
                var user = dataService.First<UserLocal>(false);
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;
                mainViewModel.User = user; //BR: igual al user que acabé de recuperar de base de datos
                mainViewModel.Lands = new LandsViewModel();
                this.MainPage = new MasterPage();
                //Application.Current.MainPage = new MasterPage(); //BR: Esto lo vi asi y simplemente lo copié y sustituí por el anterior
            }
            
        }
        #endregion
        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
