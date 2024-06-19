using System.Text;

namespace Lab6;

public class FlightQueryHandler
{

    public void PromptSelectTask()
    {
        DisplaySelectTask();
        var input = Console.ReadLine();
        try
        {
            var selectedTaskInt = Int32.Parse(input!);
            var selectedTask = (Task) Enum.Parse(typeof(Task), selectedTaskInt.ToString());
            switch (selectedTask)
            {
                case Task.Airline:
                {
                    Console.WriteLine("cast to 1");
                    break;
                }
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

    private void DisplaySelectTask()
    {
        var sb = new StringBuilder();
        sb.Append("Select task: ");
        var values = Enum.GetValues(typeof(Task)).Cast<Task>();
        foreach (var value in values)
        {
            sb.Append($"{value.ToString()} - {(int)value}");
            sb.Append(", ");
        }

        sb.Remove(sb.ToString().Length - 2, 2);
        
        Console.WriteLine(sb.ToString());   
    }

    private void ExecuteAirline()
    {
        
    }
    
}

enum Task
{
    Airline = 1, Delayed = 2, Day = 3, Time = 4, LastHour = 5
}