using Loginoperations.Context;
using Loginoperations.Dto.Product;
using Loginoperations.Dto.Response;
using Loginoperations.Model;

namespace Loginoperations.Dto;

public class DtoConverter
{
    private readonly DContext _context;

    public DtoConverter(DContext context)
    {
        _context = context;
    }

    public  UserDto convertUser(User? user)
    {
        if (user == null)
        {
            return null;
        }
        return new UserDto(user.Id,user.Username);
    }

    public ProductsResponse convertProduct(Model.Product product)
    {
       
        if (product == null)
        {
            return null;
        }
        User db = _context.Users.FirstOrDefault(x => x.Id == product.UserId);
        return new ProductsResponse(product.Id,product.Title,product.Description,product.Image,product.Price,product.Location,product.UserId,convertUser(db),product.DatePosted);
    }
}