using Domain;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IURLService
    {
        Task ProcessMottuUrlsAsync(List<MottuUrl> mottuUrls);
        Task<MottuUrl> ProcessShortUrlAsync(string longUrl);
        Task UpdateAsync(MottuUrlDTO mottuUrl);
        Task<MottuUrl> ReadUrlAsync(string url);
        Task UpdateAsync(MottuUrl mottuUrl);
        Task<MottuUrl> GetUrlAsync(string id);
        Task DeleteUrlAsync(string id);
    }
}
