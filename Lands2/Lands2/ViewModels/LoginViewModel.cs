namespace Lands2.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Views;
    using Xamarin.Forms;
    using Helpers;
    using System;

    class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService; //esto podría haberlo creado en al región de los atributos
        #endregion
        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }

            /*
            get
            {
                return this.password;
            }
            set
            {
                if(this.password != value) //significa que el valor de la propiedad cambió
                {
                    this.password = value;
                    //después de que la propiedad cambie hay que refrescar el atributo
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.Password)));
                }
            }
            */
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
            /*
            get
            {
                return this.isRunning;
            }
            set
            {
                if (this.isRunning != value) //significa que el valor de la propiedad cambió
                {
                    this.isRunning = value;
                    //después de que la propiedad cambie hay que refrescar el atributo
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsRunning)));
                }
            }
            */
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
            /*
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value) //significa que el valor de la propiedad cambió
                {
                    this.isEnabled = value;
                    //después de que la propiedad cambie hay que refrescar el atributo
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }
            }
            */
        }
        #endregion
        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            
            this.Email = "brusssli@hotmail.com";
            this.Password = "deutschland00";            
        }
        #endregion
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
            //set; le quitamos esta propiedad ya que este comando es una propiedad de solo lectura.
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept
                    /*"Error",
                    "You must enter an email",
                    "Accept"*/ );
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an password",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;
            /*
            if (this.Email != "jzuluaga55@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or Password incorrect",
                    "Accept");
                this.Password = string.Empty;
                return;
            }
            */

            //Antes de consumir un servicios tengo que comprobar si hay conexión o no
            var connection = await this.apiService.CheckConnection();
            
            if(!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                connection.Message,                
                "Accept");
                return;
            }
            //Si hay conexión, tenemos que validar que nos genere el Token
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await this.apiService.GetToken(
                apiSecurity,
                this.Email,
                this.Password);

            if(token == null) //por si el internet se cayó un milisegundo después
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Something was wrong, please try later.",
                "Accept");
                return;
            }
            //por si me equivoqué ingresando los datos
            if(string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                token.ErrorDescription,                
                "Accept");
                this.Password = string.Empty;
                return;
            }

            //si llegó hasta aquí es porque todo ocurrió normalmente
            var user = await this.apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                this.Email);

            var mainViewModel = MainViewModel.GetInstance(); //Éste es el apuntador
            mainViewModel.Token = token.AccessToken;
            mainViewModel.TokenType = token.TokenType;
            mainViewModel.User = user; //BR: es igual al objeto user que se logeó
            if (this.IsRemembered)
            {
                Settings.Token = token.AccessToken;
                Settings.TokenType = token.TokenType;
            }
            //MainViewModel.GetInstance().Lands = new LandsViewModel(); //vamos a crear un APUNTADOR, para no estarlo llamando constantemente
            mainViewModel.Lands = new LandsViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            Application.Current.MainPage = new MasterPage(); //esta es otra forma de navegar. La MainPage la cambiamos en tiempo de ejecución
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
            
        }

        private async void Register() //llamará a una nueva página
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        #endregion
    }
}
