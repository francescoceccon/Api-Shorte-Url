using Domain;
using Domain.DTO;

namespace Infrastructure.Repositorys.Interfaces
{
    public interface IUrlRepository
    {
        Task<MottuUrl> InsertAsync(Dictionary<string,string> fifo,string longUrl);
        Task InsertAsync(List<MottuUrl> mottuUrls);
        Task<MottuUrl> ReadAsync(string url);
        Task<MottuUrl> ReadByIdAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(MottuUrlDTO mottuUrl);
        Task UpdateAsync(MottuUrl mottuUrl);
    }
}
