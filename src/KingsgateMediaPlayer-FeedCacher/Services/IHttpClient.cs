using System.Threading.Tasks;

namespace KingsgateMediaPlayerFeedCacher.Services {

    public interface IHttpClient {
        Task<string> get(string url);
    }
}
