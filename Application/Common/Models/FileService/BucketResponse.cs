using System.Net;

namespace Application.Common.Models.FileService;

public class BucketResponse
{
    public bool Success { get; private set; }
    public string FileKey { get; private set; }

    public BucketResponse(HttpStatusCode httpStatusCode, string fileKey)
    {
        Success = (int)httpStatusCode <= 299;
        FileKey = fileKey;
    }
}