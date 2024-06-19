namespace Lab6;

public class FlightInformationSystem
{

    public List<Flight> GetFlights()
    {
        var result = new List<Flight>();
        var filePath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/flights_data.json";

        using FileStream stream = File.OpenRead(filePath);
        var flightsWrapper = Flight.RetrieveFromJson(stream);
        
        Console.WriteLine(flightsWrapper!.Flights.Count);
        
        return result;
    }
}