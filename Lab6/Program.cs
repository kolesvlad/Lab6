using Lab6;

class Program
{

    static void Main()
    {
        var system = new FlightInformationSystem();
        var handler = new FlightQueryHandler();
        
        var flights = system.GetFlights();   
        
        var selectedTask = handler.PromptSelectTask();
        if (selectedTask != null)
        {
            handler.ExecuteTask(selectedTask.Value, flights);
        }
    }
    
}