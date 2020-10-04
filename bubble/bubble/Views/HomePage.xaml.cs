using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace bubble.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    { 
        public HomePage(LoginObj user)
        {
            Console.WriteLine(user.user);
            InitializeComponent();
            welcome.Text = "Welcome " + user.user;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
