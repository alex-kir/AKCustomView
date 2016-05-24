using System;

using Xamarin.Forms;

namespace AKCustomView.Demo
{
    public class App : Application
    {
        public App()
        {

            var content = new RelativeLayout();
            RoundBoxView roundBox;
            content.Children.Add(roundBox = new RoundBoxView()
                {
                    BorderColor = Color.Green,
                    BorderWidth = 2,
                    CornerRadius = 50,
                    FillColor = Color.Yellow,
                },
                Constraint.RelativeToParent(p => (p.Width - 200) / 2),
                Constraint.RelativeToParent(p => (p.Height - 300) / 2),
                Constraint.RelativeToParent(p => 200),
                Constraint.RelativeToParent(p => 300));

            content.Children.Add(new Button()
            {
                    Text = "Color",
                Command = new Command(it => {
                    double c = (DateTime.Now.Ticks % 1000) / 1000.0;
                    MainPage.BackgroundColor = Color.FromRgb(c, c, c);
                }),
            },
                Constraint.RelativeToParent(p => (p.Width - 200) / 2),
                Constraint.RelativeToParent(p => (p.Height - 100)),
                Constraint.RelativeToParent(p => 200),
                Constraint.RelativeToParent(p => 50)
            );
           
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = content,
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

