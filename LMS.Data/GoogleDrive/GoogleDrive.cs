using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using LMS.Common.Constants;
using LMS.Data.Exceptions;

namespace LMS.Data.GoogleDrive
{
    public class GoogleDrive
    {
        private readonly DriveService _driveService;
        public GoogleDrive()
        {
            string[] scopes = { DriveService.Scope.Drive };
            var credential = GoogleCredential.FromFile(Constants.CredentialJson).CreateScoped(scopes);

            _driveService = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyApp"
            });
        }


        public async Task<string> UploadFile(Stream stream, string fileName, string contentType)
        {
            var fileMetaData = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName
            };

            var request = _driveService.Files.Create(fileMetaData, stream, contentType);
            request.Fields = "Id";

            var uploadResult = await request.UploadAsync(CancellationToken.None);
            if (uploadResult.Status != Google.Apis.Upload.UploadStatus.Completed)
                throw new FailedToUploadException();

            return request.ResponseBody.Id;


        }

    }
}
