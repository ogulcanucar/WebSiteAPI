using MediatR;

namespace WebSiteAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool Populer { get; set; }
        public List<string> Categories { get; set; }
        public string? Material { get; set; }
        public string? Material2 { get; set; }
        public string? Material3 { get; set; }
    }
}