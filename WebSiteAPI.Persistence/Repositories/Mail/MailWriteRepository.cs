using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Contexts;
using a=WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Repositories.Mail
{
    public class MailWriteRepository : WriteRepository<a.Mail>, IMailWriteRepository
    {
        public MailWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
