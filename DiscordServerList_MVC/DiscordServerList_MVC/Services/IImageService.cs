/*
 * Based on Coder Foundry Blog series
 * 
 * Kyle Givler 2021
 */

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DiscordServerList_MVC.Services
{
    public interface IImageService
    {
        string ContentType(IFormFile file);
        string DecodeImage(byte[] data, string type);
        Task<byte[]> EncodeImageAsync(IFormFile file);
        Task<byte[]> EncodeImageAsync(string fileName);
        int Size(IFormFile file);
    }
}