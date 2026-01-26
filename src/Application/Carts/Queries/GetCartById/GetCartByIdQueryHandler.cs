using AutoMapper;
using MediatR;
using ShoppingProject.Application.Carts.Specifications;
using ShoppingProject.Application.Common.Exceptions;
using ShoppingProject.Application.Common.Interfaces;
using ShoppingProject.Application.DTOs;

namespace ShoppingProject.Application.Carts.Queries.GetCartById;

public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartDto>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetCartByIdQueryHandler(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = CartByIdSpecification.Create(request.Id);
        var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Cart with Id {request.Id} was not found.");
        }

        return _mapper.Map<CartDto>(entity);
    }
}
