using Application.app.Features.CategoryFeature.Dtos;
using CSharpFunctionalExtensions;
using Domain.app.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.app.Features.CategoryFeature
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryDto>>
    {
        public int Id { get; set; }

        private class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryDto>>
        {

            private readonly IElectronicsContext _context;

            public GetCategoryByIdQueryHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<GetCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Select(x => new GetCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).FirstOrDefaultAsync(x => x.Id == request.Id);

                return category is null
                    ? Result.Failure<GetCategoryDto>("Category Not Found")
                    : Result.Success(category);
            }
        }
    }
}
