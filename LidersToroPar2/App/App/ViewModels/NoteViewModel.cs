

namespace App.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using global::App.Models;
    using global::App.Views;

    public class NoteViewModel : BaseViewModel
    {

       
        string notes;
        ApiService apiService;

        public string Notes
        {
            get
            {
                return this.notes;
            }
            set
            {
                SetValue(ref this.notes, value);
            }
        }
       

    public ICommand LoginCommand
    {
        get
        {
            return new RelayCommand(cmdLogin);
        }
    }
    private async void cmdLogin()
    {
        if (String.IsNullOrEmpty(Notes))
        {
            await App.Current.MainPage.DisplayAlert("Notes empty",
                            "Please enter your email",
                            "Accept");
            return;
        }

        var conexion = await this.apiService.CheckConnection();
        if (!conexion.IsSuccess)
        {
            
            await Application.Current.MainPage.DisplayAlert(
               "ERROR",
               conexion.Message,
               "Accept");
            return;
        }


        TokenResponse token = await this.apiService.Post<Response>(
          "https://notesplc.azurewebsites.net/",
          
          this.Notes);



            if (token == null)
            {
                
                await Application.Current.MainPage.DisplayAlert(
                   "ERROR",
                   "Todo la conexion no da, please try later.",
                   "Accept");
                return;
    }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                
                await Application.Current.MainPage.DisplayAlert(
                   "ERROR",
                   token.ErrorDescription,
                   "Accept");
                

                return;
    }
    MainViewModel mainViewModel = MainViewModel.GetInstance();
    mainViewModel.Token = token.AccessToken;
            mainViewModel.TokenType = token.TokenType;

            Application.Current.MainPage = new NavigationPage(new NotePage());
            

        }
        
        
    }
}
