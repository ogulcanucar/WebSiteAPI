using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface IMailService
    {
        public Task CreateAsync(Mail model);
        public Task<List<Mail>> GetAllAsync();
        public Task<Mail> GetByIdAsync(string id);
        public Task SendMailAsync(Mail model);
    }
}
