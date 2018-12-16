using System.Threading.Tasks;

namespace KingsgateMediaPlayerFeedCacher.Feeds {
    public interface IRssCacher {
        Task<string> fetchParsedRssFeed();
    }    
}