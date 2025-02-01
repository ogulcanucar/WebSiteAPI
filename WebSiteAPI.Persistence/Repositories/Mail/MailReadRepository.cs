using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Contexts;
using a = WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Repositories
{
    public class MailReadRepository : ReadRepository<a.Mail>, IMailReadRepository
    {
        public MailReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
