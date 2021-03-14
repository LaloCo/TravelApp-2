using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelApp.Model;
using TravelApp.ViewModel;
using TravelApp.Helpers;

namespace TravelApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
        HistoryVM viewModel;
        public HistoryPage()
        {
            InitializeComponent();

            viewModel = new HistoryVM();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.UpdatePosts();
            await AzureAppServiceHelper.SyncAsync();
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;
            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }

        //se activeaza la click and hold
        private  async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((MenuItem)sender).CommandParameter;
            viewModel.DeletePost(post);

            await viewModel.UpdatePosts();
        }

        private  async void postListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdatePosts();
            await AzureAppServiceHelper.SyncAsync();
            postListView.IsRefreshing = false; //s-a facut refresh-ul asa ca se poate opri animatia
            //aici fct tb neaparat sa fie awaited, deci si async pt ca altfel isRefreshing va trece pe false inainte ca lista sa se refreshuiasca. 
        }
    }
}