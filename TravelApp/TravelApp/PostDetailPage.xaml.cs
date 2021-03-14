using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
            coordinatesLabel.Text = $"{selectedPost.Latitude}, {selectedPost.Longitude}";
            distanceLabel.Text = $"{selectedPost.Distance} m";
        }

        public PostDetailPage()
        {

        }

        

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;
            //pt Azure
            await App.postsTable.UpdateAsync(selectedPost);
            await DisplayAlert("Success", "Experience successfully updated", "Ok");
            await Navigation.PopAsync();
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
           
            await App.postsTable.DeleteAsync(selectedPost);
            await DisplayAlert("Success", "Experience successfully deleted", "Ok");

            await Navigation.PopAsync();
        }
    }
}