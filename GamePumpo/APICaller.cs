using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GamePumpo
{
    public static class APICaller
    {

        /// <summary>
        /// Make a GET request to a web API async
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static async Task<string> ApiCallAsync(string URL)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    using (HttpResponseMessage message = client.GetAsync(URL).Result)
                    {
                        using (HttpContent response = message.Content)
                        {
                            return await response.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "-1";
        }
    }
}
