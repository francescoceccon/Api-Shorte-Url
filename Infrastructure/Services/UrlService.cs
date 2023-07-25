
using Domain;
using Domain.DTO;
using Infrastructure.Repositorys.Interfaces;
using Infrastructure.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class URLService : IURLService
    {
        private Dictionary<string, string> _urlDictionary { get; set; }
        private readonly IUrlRepository _urlRepository;
        public URLService(IUrlRepository urlRepository)
        {
            _urlDictionary = new Dictionary<string, string>();
            _urlRepository = urlRepository;
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
            var shortUrl = ProcessLongUrl(longUrl);
            _urlDictionary.Add(longUrl,shortUrl);
            Thread.Sleep(2000);

            return await _urlRepository.InsertAsync(_urlDictionary,longUrl);
        }

        private string ProcessLongUrl(string longUrl)
        {
            string hash = "aHR0cHM6Ly93d3cuYWJlcmNyb21iaWUuY29tL3Nob3Avd2Q=";
            StringBuilder shortUriModel = new StringBuilder("http://chr.dc/X");

            var random = new Random();
            string shortUrl = string.Empty;
            for (int i = 0; i < 5 ; i++)
            {
                shortUrl += (hash[random.Next(10)]);
            }
            var newUri = shortUriModel.Replace("X", shortUrl).ToString().ToLower();
            return newUri ;
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