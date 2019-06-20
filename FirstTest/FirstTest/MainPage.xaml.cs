using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Essentials;


namespace FirstTest
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);


            var metrics = DeviceDisplay.MainDisplayInfo;
            var height = metrics.Height;
            Console.WriteLine(height);

            this.BarBackgroundColor = Color.FromRgba(0, 0, 0, 1);




            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();


        }

        protected override void OnAppearing(){
            base.OnAppearing();

           
        }
    }
}
