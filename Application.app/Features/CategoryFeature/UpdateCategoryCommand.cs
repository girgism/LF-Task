using CSharpFunctionalExtensions;
using Domain.app.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.app.Features.CategoryFeature
{
    public class UpdateCategoryCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
        {
            private readonly IElectronicsContext _context;

            public UpdateCategoryCommandHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (existingCategory is null)
                {
                    return Result.Failure<string>("Category Not Found");
                }
                try
                {
                    existingCategory.Update(request.Name, request.Description);
                    await _context.SaveChangesAsync();

                    return Result.Success("Added Successfully");
                }
                catch (Exception)
                {
                    return Result.Failure<string>("Error while saving date");
                }
            }
        }
    }
}
