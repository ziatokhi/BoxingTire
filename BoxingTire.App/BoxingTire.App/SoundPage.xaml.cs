using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoxingTire.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoundPage : ContentPage
    {
        public SoundPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var alertSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            alertSound.Load("sound/punch.wav");


            alertSound.Play();
        }
    }
}