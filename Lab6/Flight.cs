using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab6;

public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Gate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightStatus Status { get; set; }
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }

    public static FlightsWrapper? RetrieveFromJson(FileStream fileStream)
    {
        return JsonSerializer.Deserialize<FlightsWrapper>(fileStream);
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
}

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}