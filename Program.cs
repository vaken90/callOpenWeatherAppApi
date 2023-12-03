#nullable enable

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        await GetTemperatureAsync();
    }

    static async Task GetTemperatureAsync()
    {
        string apiKey = "insert your api key";
        string city = "London"; // Sostituisci con la città di interesse
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Deserializza il JSON in un oggetto, supponiamo che l'API restituisca un oggetto con una proprietà "main" contenente la temperatura
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonContent);

                    // Ora puoi accedere alla temperatura
                    double temperature = weatherResponse.Main.Temp;

                    Console.WriteLine($"La temperatura a {city} è: {temperature}°C");
                }
                else
                {
                    Console.WriteLine($"Errore nella chiamata API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la chiamata API: {ex.Message}");
            }
        }
    }
}

// Classe per deserializzare l'oggetto JSON restituito dall'API
public class WeatherResponse
{
    public MainData Main { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
    // Altre proprietà necessarie in base alla struttura effettiva dei dati restituiti dall'API
}
