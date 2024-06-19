using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab6;

public class Flight
{
    public string FlightNumber { get; set; }
    [JsonConverter(typeof(AirlineConverter))]
    public string Airline { get; set; }
    public string Destination { get; set; }
    [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
    public DateTime DepartureTime { get; set; }
    [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
    public DateTime ArrivalTime { get; set; }
    public string Gate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightStatus Status { get; set; }
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }

    public void FillNullFields()
    {
        if (FlightNumber == null)
        {
            FlightNumber = String.Empty;
        }
        if (Airline == null)
        {
            Airline = String.Empty;
        }

        if (Destination == null)
        {
            Destination = String.Empty;
        }

        if (DepartureTime == null)
        {
            DepartureTime = new DateTime();
        }

        if (ArrivalTime == null)
        {
            ArrivalTime = new DateTime();
        }

        if (Gate == null)
        {
            Gate = String.Empty;
        }

        if (Status == null)
        {
            Status = FlightStatus.Cancelled;
        }

        if (Duration == null)
        {
            Duration = new TimeSpan();
        }

        if (AircraftType == null)
        {
            AircraftType = String.Empty;
        }

        if (Terminal == null)
        {
            Terminal = String.Empty;
        }
    }
}

public class FlightsWrapper
{
    [JsonPropertyName("flights")]
    public List<Flight> Flights { get; set; }
    
    public string ObtainJson()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public static FlightsWrapper? RetrieveFromJson(FileStream fileStream)
    {
        return JsonSerializer.Deserialize<FlightsWrapper>(fileStream);
    }
    
    public static FlightsWrapper? RetrieveFromJson(string contents)
    {
        return JsonSerializer.Deserialize<FlightsWrapper>(contents);
    }
}

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            var timespan = TimeSpan.Parse(reader.GetString() ?? string.Empty);
            return timespan;
        }
        catch (FormatException)
        {
            return TimeSpan.Parse("0:00:00");
        }
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeString = reader.GetString();
        try
        {
            var dateTime = DateTime.Parse(dateTimeString);
            return dateTime;
        }
        catch (FormatException)
        {
            var chunks = dateTimeString.Split("-");
            var month = int.Parse(chunks[1]);
            if (month > 12 || month < 1)
            {
                chunks[1] = "1";
                var sb = new StringBuilder();
                foreach (var chunk in chunks)
                {
                    sb.Append(chunk);
                    sb.Append("-");
                }

                sb.Remove(sb.ToString().Length - 1, 1);
                
                return DateTime.Parse(sb.ToString());
            }
        }
        
        return DateTime.Parse(dateTimeString);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        
        writer.WriteStringValue(value.ToString());
    }
    
}

public class AirlineConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() != null)
        {
            return reader.GetString().Trim();
        }
        return string.Empty;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}