using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUrlCryptography
    {
        Task<string> EncryptUrlAsync(string url);
    }
}
