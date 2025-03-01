using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IBlobService
    {
        public Task<List<string>> GetAllBlobNamesAsync(string containerName);

        public Task<string> UploadImageFileAsync(IFormFile imageFile, string containerName);

        public Task DeleteFileAsync(string containerName, string uri);
    }
}
