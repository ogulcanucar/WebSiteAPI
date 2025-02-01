using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Repositories
{
    public interface ICategoryReadRepository:IReadRepository<Category>
    {
    }
}
