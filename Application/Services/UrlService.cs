
using Application.Services.Interfaces;
using Domain;
using Domain.DTO;
using Infrastructure.Repositorys.Interfaces;
using System.Text.Json;

namespace Application.Services
{
    public class URLService : IURLService
    {
        private Dictionary<string, string> _urlDictionary { get; set; }
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlCryptography _urlCryptography;
        public URLService(IUrlRepository urlRepository,IUrlCryptography urlCryptography)
        {
            _urlDictionary = new Dictionary<string, string>();
            _urlRepository = urlRepository;
            _urlCryptography = urlCryptography;
        }

        public async Task ProcessMottuUrlsAsync(List<MottuUrl>? mottuUrls = null)
        {
            if (mottuUrls == null)
            {
                string jsonFilePath = $"{Path.GetDirectoryName(Directory.GetCurrentDirectory())}\\Infrastructure\\Json\\MottuInitJson.json";
                string json;
                using (StreamReader file = File.OpenText(jsonFilePath))
                {
                    json = file.ReadToEnd();
                }
                var jsonObject = JsonSerializer.Deserialize<List<MottuUrl>>(json); 
                await _urlRepository.InsertAsync(jsonObject);
                return;
            }
            await _urlRepository.InsertAsync(mottuUrls);
        }

        public async Task<MottuUrl> ProcessShortUrlAsync(string longUrl)
        {
            var shortUrl = await ProcessLongUrl(longUrl);
            _urlDictionary.Add(longUrl,shortUrl);
            Thread.Sleep(2000);

            return await _urlRepository.InsertAsync(_urlDictionary,longUrl);
        }

        private async Task<string> ProcessLongUrl(string longUrl)
        {
            return await _urlCryptography.EncryptUrlAsync(longUrl);
        }

        public async Task<MottuUrl> ReadUrlAsync(string url)
        {
            return await _urlRepository.ReadAsync(url);
        }

        public async Task<MottuUrl> GetUrlAsync(string id)
        {
            return await _urlRepository.ReadByIdAsync(id);
        }

        public async Task DeleteUrlAsync(string id)
        {
            await _urlRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(MottuUrlDTO mottuUrl)
        {
            await _urlRepository.UpdateAsync(mottuUrl);
        }

        public async Task UpdateAsync(MottuUrl mottuUrl)
        {
            await _urlRepository.UpdateAsync(mottuUrl);
        }
    }
}