using BoxingTire.App.Models;
using BoxingTire.App.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class ChallengeListViewModel : BaseViewModel
    {
        public ICommand CategoryCommand { get; private set; }
        public ObservableCollection<ChallengeCategory> ChallengeCategoryList { get; }

        public ChallengeListViewModel()
        {
            //Title = "Challenges List";
            CategoryCommand = new Command(CategoryClick);
            ChallengeCategoryList = new ObservableCollection<ChallengeCategory>
            {
               new ChallengeCategory {  Id = 0 , ImgSrc ="category_bck_01", IsEnable =true, Name ="Punches x Time"},
               new ChallengeCategory {  Id = 1 , ImgSrc ="category_bck_02", IsEnable =true, Name ="Force"},
               new ChallengeCategory {  Id = 2 , ImgSrc ="category_bck_05", IsEnable =true, Name ="Speed x Time"},
               new ChallengeCategory {  Id = 3 , ImgSrc ="category_bck_04", IsEnable =true, Name ="Punch x Speed x Force"},
               //new ChallengeCategory {  Id = 4 , ImgSrc ="category_bck_04", IsEnable =true, Name ="Time x Speed x Punch"},
            };
        }

        private async void CategoryClick(object obj)
        {
            if (App.microbit == null)
            {
                //  await Application.Current.MainPage.Navigation.PushModalAsync(new DeviceListPage());
                //await Shell.Current.GoToAsync("DeviceListPage");
                //    await Shell.Current.Navigation.PushAsync(new DeviceListPage());

                await App.Current.MainPage.DisplayAlert("Microbit","Please go to Device tab and connect Microbit via bluetooth", "Ok");
            }
            else
            {
                int _ChallengeCategory_Id = int.Parse(obj.ToString());
                App.ChallengeCategory_Id = _ChallengeCategory_Id;
                switch (_ChallengeCategory_Id)
                {
                    case 0:
                        await Application.Current.MainPage.Navigation.PushModalAsync(new Challenge02());
                        break;

                    case 1:
                        await Application.Current.MainPage.Navigation.PushModalAsync(new Challenge03());
                        break;

                    case 2:
                        await Application.Current.MainPage.Navigation.PushModalAsync(new Challenge04());
                        break;

                    case 3:
                        await Application.Current.MainPage.Navigation.PushModalAsync(new Challenge01());
                        break;
                }
            }
        }

        /// <summary>
        /// used to instill two-way data binding between a viewmodel's properties and a given view object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}