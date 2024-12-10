using Microsoft.Maui.Controls;

namespace GPSApp
{
    public partial class SavedLocationsPage : ContentPage
    {
        public List<SavedLocation> SavedLocations { get; set; }

        public SavedLocationsPage()
        {
            InitializeComponent(); // Load the XAML components
            LoadSavedLocations();
            BindingContext = this; // Set the binding context
        }

        private async void LoadSavedLocations()
        {
            try
            {
                SavedLocations = await DatabaseHelper.GetLocationsAsync();
                
                foreach (var location in SavedLocations)
                {
                    Console.WriteLine($"Loaded location: Lat: {location.Latitude}, Lon: {location.Longitude}, Timestamp: {location.Timestamp}");
                }

                OnPropertyChanged(nameof(SavedLocations)); // Notify UI to refresh binding
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading saved locations: {ex.Message}");
            }
        }

        // Handle item selection
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is SavedLocation selectedLocation)
            {
                Console.WriteLine($"Selected Location: Latitude={selectedLocation.Latitude}, Longitude={selectedLocation.Longitude}, Timestamp={selectedLocation.Timestamp}");
                // Optionally, perform other actions here
            }

            // Deselect the item to avoid it remaining highlighted
            ((ListView)sender).SelectedItem = null;
        }
    }

    public class SavedLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        public string location => $"Lat: {Latitude}, Lon: {Longitude}";
        public string timestampString => Timestamp.ToString("g");
    }
}
