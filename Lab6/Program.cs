using Lab6;

class Program
{

    static void Main()
    {
        var system = new FlightInformationSystem();
        var handler = new FlightQueryHandler();
        
        TestFlights(system.GetFlights(), handler);

        //TestFlights(system.GetFlightsWithErrors(), handler);
    }

    static void TestFlights(List<Flight> flights, FlightQueryHandler handler)
    {
        var selectedTask = handler.PromptSelectTask();
        if (selectedTask != null)
        {
            handler.ExecuteTask(selectedTask.Value, flights);
        }
    }
    
}