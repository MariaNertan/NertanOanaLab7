using Microsoft.Maui.Devices.Sensors;
using NertanOanaLab7.Models;
using Plugin.LocalNotification;
namespace NertanOanaLab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e) 
	{ 
		var shop = (Shop)BindingContext; 
		await App.Database.SaveShopAsync(shop); 
		await Navigation.PopAsync(); 
	}

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext; 
		var address = shop.Adress; 

		var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };

        var shoplocation= new Location(46.7492379, 23.5745597);//pentru Windows Machine */


            await Map.OpenAsync(shoplocation, options);
    }
}