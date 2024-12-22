using CSharpFunctionalExtensions;
using Domain.app.Entities;
using Domain.app.Interfaces;
using MediatR;

namespace Application.app.Features.CategoryFeature
{
    public class AddCategoryCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result<string>>
        {
            private readonly IElectronicsContext _context;

            public AddCategoryCommandHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
            {
                var instance = Category.Instance(request.Name, request.Description);
                try
                {
                    await _context.Categories.AddAsync(instance);
                    await _context.SaveChangesAsync();

                    return Result.Success("Added Successfully");
                }
                catch (Exception)
                {
                    return Result.Failure<string>("Error while");
                }
            }
        }
    }
}
