namespace Lands2.ViewModels
{
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private ObservableCollection<Land> lands;

        #endregion

        #region properties
        public ObservableCollection<Land> Lands //Por qué observable collection? Porque la voy a pintar en mi ListView
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiService = new ApiService(); //Aquí instancio el servicio y ya solo me queda consumirlo
            this.LoadLands();
        }
        #endregion

        #region Methods

        private async void LoadLands()
        {
            var connection = await this.apiService.CheckConnection();

            if(!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync(); //un back por código
                return;
            }
            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var list = (List<Land>)response.Result; //como devuelve un objeto, tenemos que castearlo
            this.Lands = new ObservableCollection<Land>(list);//Ahora pintamos la ObservableCollection en la vista
        }
        #endregion
    }
}
