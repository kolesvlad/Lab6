using Lab6;

class Program
{

    static void Main()
    {
        // Ініціалізація обʼєктів
        var system = new FlightInformationSystem();
        var handler = new FlightQueryHandler();
        
        // Робота з данними з flights_data.json
        TestFlights(system.GetFlights(), handler);

        // Робота з данними з flights_data_with_errors.json
        //TestFlights(system.GetFlightsWithErrors(), handler);
    }

    static void TestFlights(List<Flight> flights, FlightQueryHandler handler)
    {
        // Обираємо завдання через консоль
        var selectedTask = handler.PromptSelectTask();
        if (selectedTask != null)
        {
            // Виконуємо завдання і генеруємо звіт
            handler.ExecuteTask(selectedTask.Value, flights);
        }
    }
    
}