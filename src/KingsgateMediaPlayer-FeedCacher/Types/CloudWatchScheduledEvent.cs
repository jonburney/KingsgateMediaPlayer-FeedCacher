
namespace KingsgateMediaPlayerFeedCacher.Types {
    public class CloudWatchScheduledTask {
        public string version { get; set; }
        public string id { get; set; }

        public string source { get; set; }

        public string account { get; set; }

        public string time { get; set; }

        public string region { get; set; }
        public string[] resources { get; set; }
    }
}


// {
//     "version": "0",
//     "id": "603a1faa-6222-8f9c-71bf-4d9ad3983295",
//     "detail-type": "Scheduled Event",
//     "source": "aws.events",
//     "account": "980006391148",
//     "time": "2018-12-16T09:56:28Z",
//     "region": "eu-west-1",
//     "resources": [
//         "arn:aws:events:eu-west-1:980006391148:rule/KingsgateMediaPlayer-FeedCacher"
//     ],
//     "detail": {}
// }