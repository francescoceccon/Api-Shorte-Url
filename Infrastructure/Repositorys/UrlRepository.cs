using Domain;
using Domain.DTO;
using Infrastructure.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositorys
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext _urlContext;
        private readonly ILogger<UrlRepository> _logger;
        
        public UrlRepository(UrlContext urlContext,ILogger<UrlRepository> logger)
        {
            _urlContext = urlContext;
            _logger = logger;
        }

        private string RandomizeId()
        {
            Random random = new Random();
            string numbers = string.Empty;

            for (int i = 0; i < 5; i++)
            {
                int randomNumber = random.Next(0, 10);
                numbers += randomNumber.ToString();
            }
            return numbers;
        }

        public async Task<MottuUrl> InsertAsync(Dictionary<string,string> dictionary, string longUrl)
        {
            try
            {
                var entity = new MottuUrl(dictionary,RandomizeId());
                _urlContext.Url.Add(entity);
                await _urlContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task InsertAsync(List<MottuUrl> mottuUrls)
        {
            try
            {
                await _urlContext.Url.AddRangeAsync(mottuUrls);
                await _urlContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<MottuUrl> ReadAsync(string url)
        {
            try
            {
                var entity = await _urlContext.Url.FirstOrDefaultAsync(x => x.ShortUrl == url);
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        public async Task<MottuUrl> ReadByIdAsync(string id)
        {
            try
            {
                var entity = await _urlContext.Url.FirstOrDefaultAsync(x => x.Id == id);
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                var entity = await this.ReadByIdAsync(id);
                _urlContext.Remove(entity);
                await _urlContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task UpdateAsync(MottuUrlDTO mottuUrl)
        {
            try
            {
                var entity = await this.ReadByIdAsync(mottuUrl.Id);
                if (entity != null)
                {
                    entity.Url = mottuUrl.Url;
                    entity.Hits = mottuUrl.Hits;
                    entity.ShortUrl = mottuUrl.ShortUrl;

                    _urlContext.Update(entity);
                    await _urlContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task UpdateAsync(MottuUrl mottuUrl)
        {
            try
            {
                var entity = await this.ReadByIdAsync(mottuUrl.Id);
                if (entity != null)
                {
                    entity.Hits += 1;
                    _urlContext.Update(entity);
                    await _urlContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
