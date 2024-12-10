using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace GPSApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Handle map click to dynamically add a new pin
        map.MapClicked += OnMapClicked;
    }

    /// <summary>
    /// Trigger saving manually entered latitude and longitude into database
    /// </summary>
    private async void OnSaveLocationClicked(object sender, EventArgs e)
    {
        try
        {
            // Parse latitude and longitude from input fields
            if (double.TryParse(entryLatitude.Text, out double latitude) &&
                double.TryParse(entryLongitude.Text, out double longitude))
            {
                Console.WriteLine($"Saving manually entered coordinates: Latitude={latitude}, Longitude={longitude}");

                // Save entered location into database
                await DatabaseHelper.AddLocationAsync(latitude, longitude);

                // Add a pin to the map
                var newPin = new Pin
                {
                    Label = "Manual Entry",
                    Location = new Location(latitude, longitude)
                };

                map.Pins.Add(newPin);

                await DisplayAlert("Success", "Location saved to database and map updated.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Please enter valid latitude and longitude values.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            Console.WriteLine($"Error saving manual location: {ex.Message}");
        }
    }

    private async void OnViewSavedLocationsClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new SavedLocationsPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load saved locations: {ex.Message}", "OK");
        }
    }

    private async void OnMapClicked(object? sender, MapClickedEventArgs e)
    {
        try
        {
            if (e?.Location != null)
            {
    
                
                var clickedLocation = new Location(e.Location.Latitude, e.Location.Longitude);

                var newPin = new Pin
                {
                    Label = "Dynamic Click Pin",
                    Location = clickedLocation
                };

                map.Pins.Add(newPin);

                await DatabaseHelper.AddLocationAsync(clickedLocation.Latitude, clickedLocation.Longitude);

                await DisplayAlert("Success", "Pin added dynamically at clicked location.", "OK");
            }
            else
            {
                Console.WriteLine("Invalid map click event.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during map click handling: {ex.Message}");
        }
    }
}
