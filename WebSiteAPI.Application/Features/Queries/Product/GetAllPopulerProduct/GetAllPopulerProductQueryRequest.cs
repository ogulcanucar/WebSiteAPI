using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Queries.Product.GetAllPopulerProduct
{
    public class GetAllPopulerProductQueryRequest : IRequest<GetAllPopulerProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
