namespace WebSiteAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<string> Categories { get; set; }
    }
}