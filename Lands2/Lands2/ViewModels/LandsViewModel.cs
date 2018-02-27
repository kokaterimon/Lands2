﻿namespace Lands2.ViewModels
{
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Linq;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        private string filter;
        private List<Land> landList;
        
        #endregion

        #region properties
        public ObservableCollection<Land> Lands //Por qué observable collection? Porque la voy a pintar en mi ListView
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set {
                    SetValue(ref this.filter, value); //para que después de actualizar el valor que ejecute la búsqueda agrego lo siguiente
                    this.Search();
                }
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
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if(!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            //No me interesa que list sea una variable local porque me complica la búsqueda (search)
            //var list = (List<Land>)response.Result; //como devuelve un objeto, tenemos que castearlo
            //Esto lo hacemos para mantener todo el tiempo en memoria la lista original
            this.landList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(this.landList);//Ahora pintamos la ObservableCollection en la vista
            this.IsRefreshing = false;
        }
        private void Search()
        {
            if(string.IsNullOrEmpty(this.Filter))//si el filtro esta vacío
            {
                this.Lands = new ObservableCollection<Land>(this.landList); //volvemos a cargar la lista original
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(this.landList.Where(
                    l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                         l.Capital.ToLower().Contains(this.Filter.ToLower()))); //Lo que hay dentro del Where es la expresión Lambda. Puedo poner cualquier nombre; pero, utilizo una simple "l" porque estoy filtrando un objeto Land. El Where es una expresión sql de Linq
            }
        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get //Borramos set porque es un comando de solo lectura
            {
                return new RelayCommand(LoadLands);
            }
        }
        public ICommand SearchCommand
        {
            //vamos a lanzar un nuevo método
            get
            {
                return new RelayCommand(Search);
            }
        }
        #endregion
    }
}
