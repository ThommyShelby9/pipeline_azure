using ShoppingProject.Application.Common.Specifications;
using ShoppingProject.Domain.Entities;

namespace ShoppingProject.Application.Carts.Specifications;

public sealed class CartByIdSpecification : BaseSpecification<Cart>
{
    private CartByIdSpecification(int id)
        : base(c => c.Id == id)
    {
    }

    public static CartByIdSpecification Create(int id)
    {
        return new CartByIdSpecification(id);
    }
}
