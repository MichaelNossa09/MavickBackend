using MavickBackend.Models;
using MavickBackend.Models.Request;

namespace MavickBackend.Services
{
    public interface ICartService
    {
        public Cart? GetCartById(long id);
        public List<Cart>? GetAllCartsUser(long id);
        public Cart? AddCart(CartRequest oModel);
        public Cart? UpdateCart(CartRequest oModel);
        public Cart? DeleteCart(long id);
        public List<Cart>? DeleteAll(long id);
    }
}
