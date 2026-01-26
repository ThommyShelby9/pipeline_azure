using AutoMapper;
using MediatR;
using ShoppingProject.Application.Common.Interfaces;
using ShoppingProject.Application.Products.Specifications;
using ShoppingProject.Application.DTOs;

namespace ShoppingProject.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(
        IProductRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = ActiveProductsSpecification.Create();
        var products = await _repository.ListAsync(spec, cancellationToken);
        return products.Select(p => _mapper.Map<ProductDto>(p));
    }
}
