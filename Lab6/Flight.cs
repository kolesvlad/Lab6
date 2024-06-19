using System.Text.Json;

namespace Lab6;

public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArriavalTime { get; set; }
    public FlightStatus Status { get; set; }
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }

    public string ObtainJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static List<Flight>? RetrieveFromJson(FileStream fileStream)
    {
        return JsonSerializer.Deserialize<List<Flight>>(fileStream);
    }
}