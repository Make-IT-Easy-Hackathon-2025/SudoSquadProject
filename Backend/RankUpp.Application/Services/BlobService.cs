using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using RankUpp.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RankUpp.Core.Exceptions;
using RankUpp.Core.Configurations;

namespace RankUpp.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;


        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task<List<string>> GetAllBlobNamesAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (containerClient == null)
            {
                throw new InvalidIdException();
            }

            var items = new List<string>();

            foreach (var blobItem in containerClient.GetBlobs())
            {
                items.Add(blobItem.Name);
            }

            return Task.FromResult(items);
        }

        public async Task<string> UploadImageFileAsync(IFormFile imageFile, string containerName)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentNullException();
            }

            if (!IsImageFile(imageFile.ContentType))
            {
                throw new InvalidInputTypeException();
            }

            if (imageFile.Length > Constatns.MaxAllowedFileSize)
            {
                throw new ImageIsToLargeException();
            }

            await EnsureContainerExistsAsync(containerName);

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

            using (var stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString();
        }

        public async Task DeleteFileAsync(string containerName, string uri)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (!containerClient.GetBlobClient(uri).Exists())
            {
                throw new InvalidIdException();
            }

            await containerClient.DeleteBlobAsync(uri);
        }

        private async Task EnsureContainerExistsAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (!await containerClient.ExistsAsync())
            {
                await containerClient.CreateAsync(PublicAccessType.Blob);
            }
        }

        private bool IsImageFile(string contentType)
        {
            return contentType.StartsWith("image/");
        }
    }
}
