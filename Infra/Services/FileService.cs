using Amazon.S3.Model;
using Amazon.S3;
using Application.Common.Interfaces;
using Application.Common.Models.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Configs;

namespace Infra.Services;

[ExcludeFromCodeCoverage]
public class FileService : IFileService
{
    private readonly FileConfig _fileConfig;
    private readonly AmazonS3Client _amazonS3Client;

    public FileService(IOptions<FileConfig> fileConfig)
    {
        _fileConfig = fileConfig.Value ?? throw new ArgumentNullException(nameof(fileConfig));
        _amazonS3Client = new AmazonS3Client(
            _fileConfig.AccessKeyId,
            _fileConfig.SecretKey,
            new AmazonS3Config
            {
                ServiceURL = _fileConfig.ServiceURL,
                ForcePathStyle = false
            }
        );
    }

    public async Task DeleteObjectAsync(string fileKey)
    {
        await _amazonS3Client.DeleteObjectAsync(new DeleteObjectRequest()
        {
            BucketName = _fileConfig.BucketName,
            Key = fileKey
        });
    }

    public async Task<FileObject> GetObjectByKeyAsync(string fileKey, DateTime? expirationDate = null!, bool isPrivate = true)
    {
        if (!expirationDate.HasValue)
        {
            expirationDate = DateTime.Now.AddHours(4);
        }

        return isPrivate ? await GetPrivateObjectByKeyAsync(fileKey, expirationDate.Value) : await GetPublicObjectByKeyAsync(fileKey);
    }

    public async Task<FileObject> GetPrivateObjectByKeyAsync(string fileKey, DateTime expirationDate)
    {
        var storageObject = await _amazonS3Client.GetObjectAsync(new GetObjectRequest()
        {
            BucketName = _fileConfig.BucketName,
            Key = fileKey
        });

        var objectByKey = new FileObject(
            FileURL: _amazonS3Client.GetPreSignedURL(new GetPreSignedUrlRequest() { BucketName = _fileConfig.BucketName, Key = fileKey, Expires = expirationDate }),
            Information: new FileObjectInfo(
                Key: storageObject.Key,
                Size: storageObject.ResponseStream.Length,
                LastModified: storageObject.LastModified));

        return objectByKey;
    }



    public async Task<FileObject> GetPublicObjectByKeyAsync(string fileKey)
    {
        var storageObject = await _amazonS3Client.GetObjectAsync(new GetObjectRequest()
        {
            BucketName = _fileConfig.BucketName,
            Key = fileKey
        });

        var objectByKey = new FileObject(
            FileURL: _fileConfig.BucketUrl + fileKey,
            Information: new FileObjectInfo(
                Key: storageObject.Key,
                Size: storageObject.ResponseStream.Length,
                LastModified: storageObject.LastModified));

        return objectByKey;
    }

    public async Task<List<FileObjectInfo>> ListAllObjectsAsync()
    {
        var storageObjects = await _amazonS3Client.ListObjectsV2Async(new ListObjectsV2Request() { BucketName = _fileConfig.BucketName });

        return storageObjects.S3Objects.Select(sObj
            => new FileObjectInfo(
                Key: sObj.Key,
                Size: sObj.Size,
                LastModified: sObj.LastModified)).ToList();
    }

    public async Task<BucketResponse> UploadFile(FileStream file, bool isPrivate = true)
    {
        using var newMemoryStream = new MemoryStream();

        file.CopyTo(newMemoryStream);

        var fileExtension = Path.GetExtension(file.Name);
        var fileKey = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);

        var response = await _amazonS3Client.PutObjectAsync(new PutObjectRequest()
        {
            InputStream = newMemoryStream,
            Key = fileKey,
            BucketName = _fileConfig.BucketName,
            CannedACL = isPrivate ? S3CannedACL.Private : S3CannedACL.PublicRead
        });

        return new BucketResponse(response.HttpStatusCode, fileKey);
    }

    public async Task<BucketResponse> UploadFile(IFormFile file, bool isPrivate = true)
    {
        using var newMemoryStream = new MemoryStream();

        file.CopyTo(newMemoryStream);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileKey = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);

        var response = await _amazonS3Client.PutObjectAsync(new PutObjectRequest()
        {
            InputStream = newMemoryStream,
            Key = fileKey,
            BucketName = _fileConfig.BucketName,
            CannedACL = isPrivate ? S3CannedACL.Private : S3CannedACL.PublicRead
        });

        return new BucketResponse(response.HttpStatusCode, fileKey);
    }

    public async Task<BucketResponse> UploadPublicFile(IFormFile file)
    {
        return await UploadFile(file, isPrivate: false);
    }

    public string GetUrl(string fileName)
    {
        return $"{_fileConfig.ServiceURL}{_fileConfig.BucketName}/{fileName}";
    }
}