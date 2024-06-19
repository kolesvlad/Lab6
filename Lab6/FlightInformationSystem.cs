namespace Lab6;

public class FlightInformationSystem
{

    public List<Flight> GetFlights()
    {
        var filePath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/flights_data.json";

        using FileStream stream = File.OpenRead(filePath);
        var flightsWrapper = FlightsWrapper.RetrieveFromJson(stream);
        
        Console.WriteLine(flightsWrapper!.Flights.Count);

        return flightsWrapper.Flights;
    }

    public List<Flight> GetFlightsWithErrors()
    {
        var filePath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/flights_data_with_errors.json";
        var contents = File.ReadAllText(filePath);
        var flightsWrapper = FlightsWrapper.RetrieveFromJson(contents);
        
        foreach (var flight in flightsWrapper.Flights)
        {
            flight.FillNullFields();
        }

        return flightsWrapper.Flights;   
    }
}