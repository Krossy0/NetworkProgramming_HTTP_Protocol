
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        HttpClient client = new HttpClient();

        while (true)
        {
            Console.Write("Seher: ");
            string city = Console.ReadLine();

            if (city.ToLower().Trim() == "exit")
                break;

            try
            {
                string url = "https://wttr.in/" + city + "?format=j1";

                string json = await client.GetStringAsync(url);

                JsonDocument data = JsonDocument.Parse(json);

                JsonElement weather = data.RootElement// haava meelumatini cekir bu
                    .GetProperty("current_condition")[0];//jsondaki bunu tapir ve burda cari hava seraiti yerllesir

                Console.WriteLine("Seher: " + city);
                Console.WriteLine("Temperatur: " + weather.GetProperty("temp_C").GetString() + " C");
                Console.WriteLine("Hiss edilen: " + weather.GetProperty("FeelsLikeC").GetString() + " C");
                Console.WriteLine("Hava: " + weather.GetProperty("weatherDesc")[0].GetProperty("value").GetString());
                Console.WriteLine("Rutubet: " + weather.GetProperty("humidity").GetString() + "%");
                Console.WriteLine("Kulek: " + weather.GetProperty("windspeedKmph").GetString() + " km/saat");
            }
            catch (Exception)
            {
                Console.WriteLine("Xeta bas verdi! Seheri ve interneti yoxla.");
            }
        }

        client.Dispose();
    }
}
