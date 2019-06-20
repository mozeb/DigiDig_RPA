using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FirstTest
{
    public partial class Records : ContentPage
    {


        List<RecordData> allRecords;


        DataTemplate recordDataTemplate ;

        List<RecordViewModel> records1 = new List<RecordViewModel>();
        Xamarin.Forms.ListView list1;

            public Records()
        {


            allRecords = new List<RecordData>();



            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            var metrics = DeviceDisplay.MainDisplayInfo;
            var height = metrics.Height;

           GetData();



            recordDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = (int)((height * 0.2) / DeviceDisplay.MainDisplayInfo.Density) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength (1,GridUnitType.Star)});
                grid.VerticalOptions = LayoutOptions.FillAndExpand;
                grid.Margin = new Thickness (0, 10, 0, 10);
                grid.BackgroundColor = Color.FromHex("#f5f5f5");

                var imgFrame = new Frame();
                imgFrame.BackgroundColor = Color.White;
                imgFrame.HasShadow = false;
                imgFrame.CornerRadius = 0;
                imgFrame.Margin = 20;

                var embeddedImage = new Image { Aspect = Aspect.AspectFit };
                embeddedImage.SetBinding(Image.SourceProperty, "RecordImage");
                embeddedImage.Margin = 20;


                var typeLabel = new Label();
                typeLabel.BackgroundColor = Color.FromHex("#2112b7");
                typeLabel.WidthRequest = 60;
                typeLabel.SetBinding(Label.TextProperty, "Type");
                typeLabel.TextColor = Color.White;
                typeLabel.HeightRequest = 25;
                typeLabel.VerticalOptions = LayoutOptions.Start;
                typeLabel.HorizontalOptions = LayoutOptions.Center;
                typeLabel.HorizontalTextAlignment = TextAlignment.Center;
                typeLabel.VerticalTextAlignment = TextAlignment.Center;
                typeLabel.FontFamily = Device.RuntimePlatform == Device.iOS ? "Inconsolata-Bold" : null;
                typeLabel.FontSize = 15;

                var testGrid = new Grid();
                testGrid.Padding = new Thickness(0, 10, 0, 10);

                testGrid.RowDefinitions.Add(new RowDefinition { Height = 30 });
                testGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                testGrid.HorizontalOptions = LayoutOptions.Start;
                testGrid.Margin = new Thickness(0, 0, 0, 0);
                testGrid.BackgroundColor = Color.Transparent;

                testGrid.Children.Add(typeLabel, 0, 0);


                // Adding major record data to list item
                var recordGrid = new Grid();
                recordGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)});
                recordGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                recordGrid.VerticalOptions = LayoutOptions.FillAndExpand;
                recordGrid.Margin = new Thickness(0, 20, 0, 20);
                recordGrid.BackgroundColor = Color.FromHex("#f5f5f5");


                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                nameLabel.SetBinding(Label.TextProperty, "RecordName");
                nameLabel.FontSize = 20;
                nameLabel.FontFamily = Device.RuntimePlatform == Device.iOS ? "Inconsolata-Bold" : null;
                recordGrid.Children.Add(nameLabel, 0, 0);


                var artistLabel = new Label { FontAttributes = FontAttributes.Bold };

                artistLabel.SetBinding(Label.TextProperty, "Artist");
                artistLabel.FontSize = 15;
                artistLabel.FontFamily = Device.RuntimePlatform == Device.iOS ? "Inconsolata-Regular" : null;
                recordGrid.Children.Add(nameLabel, 0, 0);
                recordGrid.Children.Add(artistLabel, 0, 1);

                grid.Children.Add(embeddedImage, 0,0);
                grid.Children.Add(recordGrid, 1, 0);
                grid.Children.Add(testGrid, 0, 0);



                return new ViewCell { View = grid };
            });

            var firebase = new FirebaseClient("https://digidig-e203c.firebaseio.com/");
            var observable = firebase
                .Child("records")
                .OrderByKey()
                .StartAt("0")
                .AsObservable<RecordViewModel>()
                .Subscribe(d => Log(d.Object));


            list1 = new Xamarin.Forms.ListView { SeparatorVisibility = SeparatorVisibility.None, ItemsSource = records1, ItemTemplate = recordDataTemplate, RowHeight = (int)((DeviceDisplay.MainDisplayInfo.Height * 0.2) / DeviceDisplay.MainDisplayInfo.Density) + 20 };
            Content = new StackLayout
            {
                BackgroundColor = Color.White,
                Children = {
                    list1
            }
            };
        }


        void Log(RecordViewModel a)
        {

            //var found = records1.FirstOrDefault(x => x.RecordName == a.RecordName);
            //int i = records1.IndexOf(found);
            var result = records1.FirstOrDefault(x => x.RecordName == a.RecordName);
            var b = records1.Find(x => x.RecordName == a.RecordName);
            int i = records1.IndexOf(b);
            if(i >= 0){
                records1[i] = a;
            }

            Console.WriteLine("Object number: " + i);
        }


 

        async void GetData()
        {
        
            var firebase = new FirebaseClient("https://digidig-e203c.firebaseio.com/");
            var records = await firebase
              .Child("records")
                .OrderByKey()
              .StartAt("0")
              .LimitToFirst(10).OnceAsync<RecordData>();


            foreach (var record in records)
            {
                records1.Add(new RecordViewModel()
                {
                    RecordName = record.Object.RecordName,
                    Artist = record.Object.Artist + " | " + record.Object.ReleaseYear,
                    Type = record.Object.Type,
                    RecordImage = record.Object.RecordImage
                });
            }
            list1.ItemsSource = null;
            list1.ItemsSource = records1;

        }
    }
}
