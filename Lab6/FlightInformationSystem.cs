namespace Lab6;

public class FlightInformationSystem
{

    public List<Flight> GetFlights()
    {
        const string filePath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/flights_data.json";

        try
        {
            using FileStream stream = File.OpenRead(filePath);
            var flightsWrapper = FlightsWrapper.RetrieveFromJson(stream);

            Console.WriteLine(flightsWrapper!.Flights.Count);
            return flightsWrapper.Flights;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return [];
    }

    public List<Flight> GetFlightsWithErrors()
    {
        const string filePath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/flights_data_with_errors.json";

        try
        {
            var contents = File.ReadAllText(filePath);
            var flightsWrapper = FlightsWrapper.RetrieveFromJson(contents);

            foreach (var flight in flightsWrapper.Flights)
            {
                flight.FillNullFields();
            }

            return flightsWrapper.Flights;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return [];
    }
}