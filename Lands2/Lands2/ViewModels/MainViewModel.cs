
namespace Lands2.ViewModels
{
    using Models;
    using Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    class MainViewModel
    {
        #region Propperties
        public List<Land> LandsList
        {
            get;
            set;
        }
        /*public TokenResponse Token
        {
            get;
            set;
        }
        */
        public string Token { get; set; }
        public string TokenType { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }
        #endregion
        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public LandsViewModel Lands
        {
            get;
            set;
        }
        public LandViewModel Land
        {
            get;
            set;
        }
        public RegisterViewModel Register
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settings",
                PageName = "MyProfilePage", //suponiendo que en el futuro habrá una página MyProfilePage
                Title = Languages.MyProfile
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_insert_chart",
                PageName = "StatisticsPage", //suponiendo que en el futuro habrá una página MyProfilePage
                Title = Languages.Statistics
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage", //suponiendo que en el futuro habrá una página MyProfilePage
                Title = Languages.LogOut
            });
        } 
        #endregion
    }
}
