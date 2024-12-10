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

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Category)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(command => command.ImageFile)
                .NotEmpty().WithMessage("ImageFile is required");

            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
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
