using BoxingTire.App.Views;
using BoxingTire.App.Models;

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
               new ChallengeCategory {  Id = 0 , ImgSrc ="category_bck_01", IsEnable =true, Name ="Training"},
               new ChallengeCategory {  Id = 1 , ImgSrc ="category_bck_02", IsEnable =true, Name ="Work Out"},
               new ChallengeCategory {  Id = 2 , ImgSrc ="category_bck_05", IsEnable =true, Name ="Speed x Time"},
               new ChallengeCategory {  Id = 3 , ImgSrc ="category_bck_03", IsEnable =true, Name ="Combo 1 - 2 - 4"},
               new ChallengeCategory {  Id = 3 , ImgSrc ="category_bck_04", IsEnable =true, Name ="Count Fight 4"},
            };
        }

        private async void CategoryClick(object obj)
        {
            if (App.microbit == null)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new DeviceListPage());
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new Challenge01());
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