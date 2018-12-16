using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;
using KingsgateMediaPlayerFeedCacher.Exceptions;
using KingsgateMediaPlayerFeedCacher.Feeds;
using KingsgateMediaPlayerFeedCacher.Services;
using KingsgateMediaPlayerFeedCacher.Types;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace KingsgateMediaPlayerFeedCacher
{
    public class Function
    {
        IAmazonS3 S3Client { get; set; }
        IHttpClient httpClient { get; set; }
        IRssCacher rssCacher { get; set;}
        IS3StorageService s3StorageService { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            // @TODO - Move this over to a proper DI container
            S3Client = new AmazonS3Client();
            httpClient = new HttpClient();
            rssCacher = new RssCacher(httpClient);
            s3StorageService = new S3StorageService();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client, IHttpClient httpClient, IRssCacher rssCacher, IS3StorageService s3StorageService)
        {
            this.S3Client = s3Client;
            this.httpClient = httpClient;
            this.rssCacher = rssCacher;
            this.s3StorageService = s3StorageService;
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(CloudWatchScheduledTask evnt, ILambdaContext context)
        {
            if (evnt.source.Equals("aws.events")) {
                try {
                    context.Logger.LogLine("Event triggered, caching RSS Feed");
                    string rssFeedContent = await rssCacher.fetchParsedRssFeed();
                    
                    context.Logger.LogLine(rssFeedContent);

                    context.Logger.LogLine("Uploading RSS feed to S3");
                    if (await s3StorageService.uploadAndReplaceFileToS3("feeds/rss.xml", rssFeedContent)) {
                        context.Logger.LogLine("RSS Upload Completed");
                        return "RSS Upload Completed";
                    }

                } catch (Exception ex) {
                    context.Logger.LogLine(ex.Message);
                    context.Logger.LogLine(ex.StackTrace);
                    throw;
                }
            } 

            context.Logger.LogLine("Error: Unknown event trigger \"" + evnt.source + "\"");
            throw new UnhandedTrigger("Unknown event trigger \"" + evnt.source + "\"");
        }
    }
}
