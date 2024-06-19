using System.Text;

namespace Lab6;

public class FlightQueryHandler
{

    public Task? PromptSelectTask()
    {
        DisplaySelectTask();
        var input = Console.ReadLine();
        Task? selectedTask = null;
        try
        {
            var selectedTaskInt = Int32.Parse(input!);
            selectedTask = (Task) Enum.Parse(typeof(Task), selectedTaskInt.ToString());
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }

        return selectedTask;
    }

    private void DisplaySelectTask()
    {
        var sb = new StringBuilder();
        sb.Append("Select task: ");
        var values = Enum.GetValues(typeof(Task)).Cast<Task>();
        foreach (var value in values)
        {
            sb.Append($"{value.ToString()} - {(int) value}");
            sb.Append(", ");
        }

        sb.Remove(sb.ToString().Length - 2, 2);
        
        Console.WriteLine(sb.ToString());   
    }

    public void ExecuteTask(Task task, List<Flight> flights)
    {
        switch (task)
        {
            case Task.Airline:
            {
                ExecuteAirline(flights);
                break;
            }
            case Task.Delayed:
            {
                ExecuteDelayed(flights);
                break;
            }
            case Task.Day:
            {
                ExecuteDay(flights);
                break;
            }
        }
    }

    private void ExecuteAirline(List<Flight> flights)
    {
        Console.WriteLine("Select company:");
        var input = Console.ReadLine();

        try
        {
            if (input != null)
            {
                var airlineFlights = new List<Flight>();

                foreach (var flight in flights)
                {
                    if (flight.Airline.Equals(input))
                    {
                        airlineFlights.Add(flight);
                    }
                }
                airlineFlights.Sort((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime));
                CreateReport(airlineFlights, Task.Airline);
            }
            else
            {
                throw new FormatException();
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void ExecuteDelayed(List<Flight> flights)
    {
        var delayedFlights = new List<Flight>();

        foreach (var flight in flights)
        {
            if (flight.Status.Equals(FlightStatus.Delayed))
            {
                delayedFlights.Add(flight);
            }
        }
        Console.WriteLine("Delayed count = " + delayedFlights.Count);
        delayedFlights.Sort((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime));
        CreateReport(delayedFlights, Task.Delayed);
    }
    
    private void ExecuteDay(List<Flight> flights)
    {
        Console.WriteLine("Select day:");
        var day = Console.ReadLine();
        
        Console.WriteLine("Select month:");
        var month = Console.ReadLine();
        
        Console.WriteLine("Select year:");
        var year = Console.ReadLine();
        
        try
        {
            if (day != null && month != null && year != null)
            {
                var sameDayFlights = new List<Flight>();
                var inputDateTime = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                
                foreach (var flight in flights)
                {
                    if (IsSameDay(flight.DepartureTime, inputDateTime))
                    {
                        sameDayFlights.Add(flight);
                    }
                }
                sameDayFlights.Sort((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime));
                CreateReport(sameDayFlights, Task.Day);
            }
            else
            {
                throw new FormatException();
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private bool IsSameDay(DateTime x, DateTime y)
    {
        return x.Day == y.Day && x.Month == y.Month && x.Year == y.Year;
    }

    private void CreateReport(List<Flight> flights, Task task)
    {
        var flightsWrapper = new FlightsWrapper
        {
            Flights = flights
        };
        var json = flightsWrapper.ObtainJson();
        
        string directoryPath = "/Users/valdemar/Склад/Драгопед/Обʼєктно-орієнтоване програмування/Готове/Лаб6/Reports";
        string filePath = directoryPath + "/" + task + ".json";
        
        Directory.CreateDirectory(directoryPath);
        
        using StreamWriter writer = new StreamWriter(filePath);
        try
        {
            writer.Write(json);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            writer.Close();
            Console.WriteLine("Report created successfully");
        }
    }
    
}

public enum Task
{
    Airline = 1, Delayed = 2, Day = 3, Time = 4, LastHour = 5
}