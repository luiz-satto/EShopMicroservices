using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct.Handler
{
    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResult>;
}
