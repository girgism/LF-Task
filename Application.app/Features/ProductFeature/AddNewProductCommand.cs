using CSharpFunctionalExtensions;
using Domain.app.Entities;
using Domain.app.Interfaces;
using MediatR;

namespace Application.app.Features.ProductFeature
{
    public class AddNewProductCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        private class AddNewProductCommandHandler : IRequestHandler<AddNewProductCommand, Result<string>>
        {
            private readonly IElectronicsContext _context;

            public AddNewProductCommandHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(AddNewProductCommand command, CancellationToken cancellationToken)
            {
                var instance = Product.Instance(command.Name,
                                                command.Description,
                                                command.Price,
                                                command.CategoryId);
                try
                {
                    await _context.Products.AddAsync(instance);
                    await _context.SaveChangesAsync();
                    return Result.Success("Added Successfully");
                }
                catch (Exception)
                {
                    return Result.Failure<string>("Error while saving data");
                }
            }
        }
    }
}
