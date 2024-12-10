using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSApp;

public static class DatabaseHelper
{
    // Mock database initialization logic (for testing purposes)
    private static List<SavedLocation> mockDatabase = new List<SavedLocation>
    {
        new SavedLocation { Latitude = 37.7749, Longitude = -122.4194, Timestamp = DateTime.Now.AddMinutes(-10) },
        new SavedLocation { Latitude = 40.7128, Longitude = -74.0060, Timestamp = DateTime.Now.AddMinutes(-5) },
        new SavedLocation { Latitude = 34.0522, Longitude = -118.2437, Timestamp = DateTime.Now }
    };

    /// <summary>
    /// Simulate database retrieval for saved locations
    /// </summary>
    public static async Task<List<SavedLocation>> GetLocationsAsync()
    {
        // Simulate async database delay
        await Task.Delay(100);
        
        // Log the simulated data for debugging purposes
        foreach (var loc in mockDatabase)
        {
            Console.WriteLine($"Retrieved from database: Latitude={loc.Latitude}, Longitude={loc.Longitude}, Timestamp={loc.Timestamp}");
        }

        return mockDatabase;
    }

    /// <summary>
    /// Simulate saving a new location into the mock database
    /// </summary>
    public static async Task AddLocationAsync(double latitude, double longitude)
    {
        try
        {
            var newLocation = new SavedLocation
            {
                Latitude = latitude,
                Longitude = longitude,
                Timestamp = DateTime.Now
            };

            mockDatabase.Add(newLocation);

            Console.WriteLine($"Saved to mock database: Latitude={latitude}, Longitude={longitude}, Timestamp={DateTime.Now}");
            
            await Task.Delay(50); // Simulate delay
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add location to database: {ex.Message}");
        }
    }

}
