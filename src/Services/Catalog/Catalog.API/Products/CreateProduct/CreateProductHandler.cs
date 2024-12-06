using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Business logic to create a new product.
            throw new NotImplementedException();
        }
    }
}
