using System.ComponentModel;
using Xamarin.Forms;
using zad62.ViewModels;

namespace zad62.Views
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