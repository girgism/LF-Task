using Application.app.Features.CategoryFeature.Dtos;
using CSharpFunctionalExtensions;
using Domain.app.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.app.Features.CategoryFeature
{
    public class GetAllCategoriesQuery : IRequest<Result<List<GetCategoryDto>>>
    {
        private class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<GetCategoryDto>>>
        {

            private readonly IElectronicsContext _context;

            public GetAllCategoriesQueryHandler(IElectronicsContext context)
            {
                _context = context;
            }

            public async Task<Result<List<GetCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                //Must Using Paging for large data skip > take
                var categories = await _context.Categories.Select(x => new GetCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

                return Result.Success(categories);
            }
        }
    }
}
