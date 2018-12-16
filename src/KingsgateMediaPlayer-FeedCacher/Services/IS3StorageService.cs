
using System.Threading.Tasks;

namespace KingsgateMediaPlayerFeedCacher.Services {
    public interface IS3StorageService {
        Task<bool> uploadAndReplaceFileToS3(string filename, string fileContents);
    }
}
