using LMS.Common.Constants;
using Minio;
using Minio.DataModel.Args;

namespace LMS.Service.MinioStorage
{
    public class MinioStorageService
    {
        private readonly MinioClient _client;

        public MinioStorageService(string endpoint, string accessKey, string secretKey)
        {
            _client = (MinioClient)new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();

        }


        public async Task UploadFileAsync(string objectName, Stream data, string contentType)
        {
            bool found = await _client.BucketExistsAsync(new Minio.DataModel.Args.BucketExistsArgs().WithBucket(Constants.BucketName));
            if (!found)
            {
                await _client.MakeBucketAsync(new Minio.DataModel.Args.MakeBucketArgs().WithBucket(Constants.BucketName));
            }


            await _client.PutObjectAsync(new Minio.DataModel.Args.PutObjectArgs()
                .WithBucket(Constants.BucketName)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));
        }


        public async Task<Stream> DownloadFileAsync(string objectName)
        {
            var ms = new MemoryStream();
            await _client.GetObjectAsync(new Minio.DataModel.Args.GetObjectArgs().WithObject(objectName).WithBucket(Constants.BucketName)
                .WithCallbackStream(async (stream) =>
                {
                    await stream.CopyToAsync(ms);
                }));
            ms.Position = 0;
            return ms;
        }

        public async Task DeleteFileAsync( string objectName)
        {
            await _client.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(Constants.BucketName)
                .WithObject(objectName));
        }
    }
}
