﻿namespace MyFriends.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToTranslate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Translator());
        }
    }

}
