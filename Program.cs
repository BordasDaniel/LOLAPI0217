using LOLAPI0217.Model;
using System.Text.Json;

namespace LOLAPI0217
{
    internal class Program
    {
        static string version;
        static List<Champion> champions = [];

        public static async Task LoadVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    string url = "https://ddragon.leagueoflegends.com/api/versions.json";
                    var responseAPI = await client.GetStringAsync(url);

                    var response = JsonSerializer.Deserialize<string[]>(responseAPI);

                    version = response[0];
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP error while loading version: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON parsing error while loading version: {jsonEx.Message}");
            }
            

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading version: {ex.Message}");
            }
        }

        static async Task LoadChampions()
        {
            await LoadVersion();
                try
                {
                    using (HttpClient client = new())
                    {
    
                        string url = $"https://ddragon.leagueoflegends.com/cdn/{version}/data/en_US/champion.json";
                        var responseAPI = await client.GetStringAsync(url);
    
                        var response = JsonSerializer.Deserialize<ChampionDatas>(responseAPI);

                        champions = response.Data.Values.ToList();
                        Console.WriteLine(champions.Count);
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine($"HTTP error while loading champions: {httpEx.Message}");
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"JSON parsing error while loading champions: {jsonEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading champions: {ex.Message}");
            }
        }

        static async Task Main(string[] args)
        {
            await LoadChampions();
            Console.ReadKey();
        }
    
    }
}
