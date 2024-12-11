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

        var shoplocation = new Location(46.7492379, 23.5745597);//pentru Windows Machine */


        var myLocation = await Geolocation.GetLocationAsync();       /*   var myLocation = new Location(46.7731796289, 23.6213886738); //pentru Windows Machine */
        var distance = myLocation.CalculateDistance(shoplocation, DistanceUnits.Kilometers);
        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(shoplocation, options);
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop == null)
        {
            await DisplayAlert("Eroare", "Magazinul nu a fost g?sit.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Confirmare", $"Sigur dore?ti s? ?tergi magazinul '{shop.ShopName}'?", "Da", "Nu");
        if (confirm)
        {
            await App.Database.DeleteShopAsync(shop);
            await DisplayAlert("Succes", "Magazinul a fost ?ters.", "OK");
            await Navigation.PopAsync();
        }
    }




}