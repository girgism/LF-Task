using Application.app.Features.ProductFeature.Dtos;
using CSharpFunctionalExtensions;
using Domain.app.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.app.Features.ProductFeature
{
    public class GetAllProductsQuery : IRequest<Result<List<GetProductDto>>>
    {
        private class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<GetProductDto>>>
        {
            private readonly IElectronicsContext _context;
            public GetAllProductsQueryHandler(IElectronicsContext context)
            {
                _context = context;
            }
            public async Task<Result<List<GetProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                //Must Using Paging for large data skip > take
                var products = await _context.Products.Include(x => x.Category).Select(x => new GetProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                }).ToListAsync();

                return Result.Success(products);
            }
        }
    }
}
