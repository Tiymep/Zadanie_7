using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener server = new HttpListener();
            // Эту штучку потом открываем
            server.Prefixes.Add("http://127.0.0.1:8888/connection/");
            server.Start();

            // Подслушивать не хорошо!
            Console.WriteLine("Подключение");

            while (true)
            {
                var context = await server.GetContextAsync();
                var response = context.Response;

                // Загрузка содержимого файла index.html
                string filePath = "index.html";
                string responseText = File.ReadAllText(filePath);
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);

                response.ContentLength64 = buffer.Length;
                using (var output = response.OutputStream)
                {
                    await output.WriteAsync(buffer, 0, buffer.Length);
                }

                Console.WriteLine("Запрос обработан");
            }
        }
    }
}
