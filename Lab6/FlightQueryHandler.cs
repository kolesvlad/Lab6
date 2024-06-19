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
            case Task.Time:
            {
                ExecuteTime(flights);
                break;
            }
            case Task.LastHour:
            {
                ExecuteLastHour(flights);
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
    
    private void ExecuteTime(List<Flight> flights)
    {
        
        Console.WriteLine("Select departure second:");
        var departureSecond = Console.ReadLine();
        
        Console.WriteLine("Select departure minute:");
        var departureMinute = Console.ReadLine();
        
        Console.WriteLine("Select departure hour:");
        var departureHour = Console.ReadLine();
        
        Console.WriteLine("Select departure day:");
        var departureDay = Console.ReadLine();
        
        Console.WriteLine("Select departure month:");
        var departureMonth = Console.ReadLine();
        
        Console.WriteLine("Select departure year:");
        var departureYear = Console.ReadLine();
        
        Console.WriteLine("Select arrival second:");
        var arrivalSecond = Console.ReadLine();
        
        Console.WriteLine("Select arrival minute:");
        var arrivalMinute = Console.ReadLine();
        
        Console.WriteLine("Select arrival hour:");
        var arrivalHour = Console.ReadLine();
        
        Console.WriteLine("Select arrival day:");
        var arrivalDay = Console.ReadLine();
        
        Console.WriteLine("Select arrival month:");
        var arrivalMonth = Console.ReadLine();
        
        Console.WriteLine("Select arrival year:");
        var arrivalYear = Console.ReadLine();
        
        Console.WriteLine("Select destination:");
        var destination = Console.ReadLine();
        
        try
        {
            if (departureSecond != null && departureMinute != null && departureHour != null 
                && departureDay != null && departureMonth != null && departureYear != null
                && arrivalSecond != null && arrivalMinute != null && arrivalHour != null 
                && arrivalDay != null && arrivalMonth != null && arrivalYear != null
                && destination != null)
            {
                var timeRangeDayFlights = new List<Flight>();
                var departureDateTime = new DateTime(
                    int.Parse(departureYear), int.Parse(departureMonth), int.Parse(departureDay),
                    int.Parse(departureHour), int.Parse(departureMinute), int.Parse(departureSecond));
                var arrivalDateTime = new DateTime(
                    int.Parse(arrivalYear), int.Parse(arrivalMonth), int.Parse(arrivalDay),
                    int.Parse(arrivalHour), int.Parse(arrivalMinute), int.Parse(arrivalSecond));
                
                foreach (var flight in flights)
                {
                    if (flight.Destination.Equals(destination))
                    {
                        var departureMilliseconds = GetMilliseconds(flight.DepartureTime);
                        var arrivalMilliseconds = GetMilliseconds(flight.ArrivalTime);
                        var isAfterDeparture = departureMilliseconds >= GetMilliseconds(departureDateTime) ;
                        var isBeforeArrival = arrivalMilliseconds <= GetMilliseconds(arrivalDateTime);

                        if (isAfterDeparture && isBeforeArrival)
                        {
                            timeRangeDayFlights.Add(flight);
                        }
                    }
                }
                timeRangeDayFlights.Sort((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime));
                CreateReport(timeRangeDayFlights, Task.Time);
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
    
    private void ExecuteLastHour(List<Flight> flights)
    {
        
        Console.WriteLine("Select: Last hour - 1, Choose time range - 2");
        var input = Console.ReadLine();

        try
        {
            if (input != null)
            {
                var option = int.Parse(input);
                switch (option)
                {
                    case 1:
                    {
                        ExecuteLastHourOption(flights);
                        break;
                    }
                    case 2:
                    {
                        ExecuteTimeRangeOption(flights);
                        break;
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void ExecuteLastHourOption(List<Flight> flights)
    {
        var lastHourFlights = new List<Flight>();
        var lastHour = DateTime.Now.AddHours(-1);
        foreach (var flight in flights)
        {
            if (GetMilliseconds(flight.DepartureTime) >= GetMilliseconds(lastHour))
            {
                lastHourFlights.Add(flight);
            }
        }
        lastHourFlights.Sort((x, y) => DateTime.Compare(x.ArrivalTime, y.ArrivalTime));
        CreateReport(lastHourFlights, Task.LastHour);
    }

    private void ExecuteTimeRangeOption(List<Flight> flights)
    {
        Console.WriteLine("Select start second:");
        var startSecond = Console.ReadLine();
        
        Console.WriteLine("Select start minute:");
        var startMinute = Console.ReadLine();
        
        Console.WriteLine("Select start hour:");
        var startHour = Console.ReadLine();
        
        Console.WriteLine("Select start day:");
        var startDay = Console.ReadLine();
        
        Console.WriteLine("Select start month:");
        var startMonth = Console.ReadLine();
        
        Console.WriteLine("Select start year:");
        var startYear = Console.ReadLine();
        
        Console.WriteLine("Select end second:");
        var endSecond = Console.ReadLine();
        
        Console.WriteLine("Select end minute:");
        var endMinute = Console.ReadLine();
        
        Console.WriteLine("Select end hour:");
        var endHour = Console.ReadLine();
        
        Console.WriteLine("Select end day:");
        var endDay = Console.ReadLine();
        
        Console.WriteLine("Select end month:");
        var endMonth = Console.ReadLine();
        
        Console.WriteLine("Select end year:");
        var endYear = Console.ReadLine();
        
        Console.WriteLine("Select destination:");
        var destination = Console.ReadLine();
        
        try
        {
            if (startSecond != null && startMinute != null && startHour != null 
                && startDay != null && startMonth != null && startYear != null
                && endSecond != null && endMinute != null && endHour != null 
                && endDay != null && endMonth != null && endYear != null
                && destination != null)
            {
                var timeRangeDayFlights = new List<Flight>();
                var startDateTime = new DateTime(
                    int.Parse(startYear), int.Parse(startMonth), int.Parse(startDay),
                    int.Parse(startHour), int.Parse(startMinute), int.Parse(startSecond));
                var endDateTime = new DateTime(
                    int.Parse(endYear), int.Parse(endMonth), int.Parse(endDay),
                    int.Parse(endHour), int.Parse(endMinute), int.Parse(endSecond));
                
                foreach (var flight in flights)
                {
                    if (flight.Destination.Equals(destination) 
                        && IsBetweenInclusive(flight.DepartureTime, startDateTime, endDateTime))
                    {
                        timeRangeDayFlights.Add(flight);
                    }
                }
                timeRangeDayFlights.Sort((x, y) => DateTime.Compare(x.ArrivalTime, y.ArrivalTime));
                CreateReport(timeRangeDayFlights, Task.LastHour);
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
    
    private bool IsBetweenInclusive(DateTime input, DateTime a, DateTime b)
    {
        double inputMilliseconds = GetMilliseconds(input);
        return inputMilliseconds >= GetMilliseconds(a) && inputMilliseconds <= GetMilliseconds(b);
    }

    private double GetMilliseconds(DateTime dateTime)
    {
        return dateTime.ToUniversalTime().Subtract(
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        ).TotalMilliseconds;
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