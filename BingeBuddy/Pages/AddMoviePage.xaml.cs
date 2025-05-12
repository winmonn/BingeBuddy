using BingeBuddy.Models;
using BingeBuddy.ViewModels;

namespace BingeBuddy.Pages
{
    public partial class AddMoviePage : ContentPage
    {
        public AddMoviePage()
        {
            InitializeComponent();
            BindingContext = new MovieViewModel();
        }

        private void OnFilterClicked(object sender, EventArgs e)
        {
            if (BindingContext is MovieViewModel vm)
                vm.IsGenrePickerVisible = !vm.IsGenrePickerVisible;
        }
    }
}
