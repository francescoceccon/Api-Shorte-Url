using Application.Services.Interfaces;
using Infrastructure.Repositorys.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Cryptography
{
    public class UrlCryptography : IUrlCryptography
    {
        private int _randomic 
        { 
            get
            {
                return new Random().Next();
            }
        }

        private readonly IUrlRepository _urlRepository;

        public UrlCryptography(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<string> EncryptUrlAsync(string url)
        {
            StringBuilder shortUriModel = new StringBuilder("http://chr.dc/X");
            string hash;
            byte[] randomBytes = BitConverter.GetBytes(_randomic);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(randomBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            string shortRoute = hash.Substring(0, 5);
            shortUriModel = shortUriModel.Replace("X", shortRoute);

            var urlDb = await _urlRepository.ReadAsync(shortUriModel.ToString());
            if (urlDb is not null) 
                await EncryptUrlAsync(urlDb.Url);
            return shortUriModel.ToString();
        }
    }
}
