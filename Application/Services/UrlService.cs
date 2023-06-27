
using Infrastructure.Repositorys.Interfaces;

namespace Application
{
    public class URLService : IURLService
    {
        private Queue<string> _QueueSimulator { get; set; } = new Queue<string>();
        private readonly IUrlRepository _urlRepository;
        public URLService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public void PersistMottuJson()
        {
            //persistir o json
        }

        public void ProcessShortUrl(string longUrl)
        {
            var shortUrl = ProcessLongUrl(longUrl);
            _QueueSimulator.Enqueue(shortUrl);
            Thread.Sleep(10);

            _urlRepository.InsertAsync(_QueueSimulator);
        }

        private string ProcessLongUrl(string longUrl)
        {
            //encurtar url
            return "";

        }
    }
}