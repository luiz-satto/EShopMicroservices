﻿namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProductHandler(
        IDocumentSession session,
        ILogger<CreateProductHandler> logger
    ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductHandler.Handle called with {@Command}", command);

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
