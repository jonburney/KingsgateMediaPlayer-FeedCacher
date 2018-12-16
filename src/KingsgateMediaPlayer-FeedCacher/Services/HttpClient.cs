using System.Threading.Tasks;
using System.Net.Http;

namespace KingsgateMediaPlayerFeedCacher.Services {
    public class HttpClient : IHttpClient
    {
        public HttpClient() {

        }

        public async Task<string> get(string url)
        {
            // We use the system HTTP client here, but we've wrapped it in a class with an interface
            // to make it easier to unit test the rest of the code
            using (var httpClient = new System.Net.Http.HttpClient()) {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode) {
                    return await response.Content.ReadAsStringAsync();
                }
                
                throw new HttpRequestException("The returned status code is not a succes code");
            }
        }
    }

}