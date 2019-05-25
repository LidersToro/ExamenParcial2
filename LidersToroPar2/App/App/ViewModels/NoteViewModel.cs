

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
       

    public ICommand AddCommand
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

            Note x = new Note();

        Response token = await this.apiService.Post<Note>(
          "https://notesplc.azurewebsites.net/","api/Notes",this.notes,x );
          

        }
        
        
    }
}
