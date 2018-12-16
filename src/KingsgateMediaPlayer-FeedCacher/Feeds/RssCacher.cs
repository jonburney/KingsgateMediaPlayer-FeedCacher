
using System.Threading.Tasks;
using KingsgateMediaPlayerFeedCacher.Services;

namespace KingsgateMediaPlayerFeedCacher.Feeds {


    public class RssCacher : IRssCacher {
        const string FEED_URL = "https://www.kingsgateuk.com/Media/rss.xml";

        IHttpClient httpClient;

        public RssCacher() {
            this.httpClient = new HttpClient();
        }

        public RssCacher(IHttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<string> fetchParsedRssFeed() {
            return await httpClient.get(FEED_URL);
        }
    }

}