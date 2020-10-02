using System.ComponentModel;
using Xamarin.Forms;
using bubble.ViewModels;

namespace bubble.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}