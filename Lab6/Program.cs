using Lab6;

class Program
{

    static void Main()
    {
        new FlightInformationSystem().GetFlights();   
        new FlightQueryHandler().PromptSelectTask();
    }
    
}