using CSharpFunctionalExtensions;
using Domain.app.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.app.Features.CategoryFeature
{
    public class DeleteCategoryCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        private class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<string>>
        {
            private readonly IElectronicsContext _context;

            public DeleteCategoryCommandHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (existingCategory is null)
                {
                    return Result.Failure<string>("Category Not Found");
                }
                try
                {
                    _context.Categories.Remove(existingCategory);
                    await _context.SaveChangesAsync();

                    return Result.Success("Deleted Successfully");
                }
                catch (Exception)
                {
                    return Result.Failure<string>("Error while deleting date");
                }
            }
        }
    }
}
