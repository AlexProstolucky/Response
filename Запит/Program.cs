using System.Net.Http;
using System.Threading.Tasks;
internal class Program
{
    static async Task Main()
    {
        string url = "https://philpapers.org/";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    string[] lines = htmlContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        if (line.Contains("<title>"))
                        {
                            int startIndex = line.IndexOf("<title>") + "<title>".Length;
                            int endIndex = line.IndexOf("</title>");
                            string title = line.Substring(startIndex, endIndex - startIndex);
                            Console.WriteLine("Заголовок: " + title);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка при виконанні запиту. Код статусу: " + (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Виникла помилка: " + ex.Message);
            }
        }
    }
}