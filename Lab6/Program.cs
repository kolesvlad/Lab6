using Lab6;

class Program
{

    static void Main()
    {
        var system = new FlightInformationSystem();
        var handler = new FlightQueryHandler();
        
        CheckFlights(system.GetFlights(), handler);

        //CheckFlights(system.GetFlightsWithErrors(), handler);
    }

    static void CheckFlights(List<Flight> flights, FlightQueryHandler handler)
    {
        if (flights.Count == 0) return;
        
        var selectedTask = handler.PromptSelectTask();
        if (selectedTask != null)
        {
            handler.ExecuteTask(selectedTask.Value, flights);
        }
    }
    
}