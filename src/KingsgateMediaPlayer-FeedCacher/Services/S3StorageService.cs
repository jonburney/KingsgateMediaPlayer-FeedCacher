using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace KingsgateMediaPlayerFeedCacher.Services {

    public class S3StorageService : IS3StorageService
    {
        IAmazonS3 s3Client;

        public S3StorageService() {
            this.s3Client = new AmazonS3Client();
        }

        public S3StorageService(IAmazonS3 s3Client) {
            this.s3Client = s3Client;
        }

        public async Task<bool> uploadAndReplaceFileToS3(string filename, string fileContents)
        {
            PutObjectRequest request = new PutObjectRequest {
                BucketName = "kingsgate-sermons",
                Key = filename,
                ContentBody = fileContents
            };

            var result = await this.s3Client.PutObjectAsync(request);

            return true;
        }
    }
}
