using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;

namespace MavickBackend.Services
{
    public class CartService : ICartService
    {
        public List<Cart>? GetAllCartsUser(long id)
        {
            using var db = new MavickDBContext();
            var us = db.Users.Find(id);
            if (us == null) return null;
            var lst = db.Carts.Where(c => c.IdUser == id).ToList();
            return lst;
        }
        public Cart? GetCartById(long id)
        {
            using var db = new MavickDBContext();
            var cart = db.Carts.Find(id);
            if (cart == null) return null;
            return cart;
        }

        public Cart? AddCart(CartRequest oModel)
        {
            using var db = new MavickDBContext();
            var user = db.Carts.Where(d => d.IdProduct == oModel.IdProduct && d.IdUser == oModel.IdUser).FirstOrDefault();
            var prod = db.Products.Find(oModel.IdProduct);
            if(prod == null) return null;
            if (user == null)
            {
                var oCart = new Cart()
                {
                    Amount = 1,
                    IdProduct = oModel.IdProduct,
                    IdUser = oModel.IdUser
                };
                db.Carts.Add(oCart);
                db.SaveChanges();
                return oCart;
            }
            else
            {
                user.Amount += oModel.Amount;
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return user;
            }
        }
        public Cart? UpdateCart(CartRequest oModel)
        {
            using var db = new MavickDBContext();
            var oCart = db.Carts.Where(d => d.IdProduct == oModel.IdProduct && d.IdUser == oModel.IdUser).FirstOrDefault();
            if (oCart == null) return null;
            oCart.Amount = oModel.Amount;
            if (oModel.Amount == 0)
            {
                db.Carts.Remove(oCart);
                db.SaveChanges();
            }
            else
            {
                db.Entry(oCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return oCart;
        }
        public Cart? DeleteCart(long id)
        {
            using var db = new MavickDBContext();
            var oCart = db.Carts.Find(id);
            if (oCart == null) return null;
            db.Carts.Remove(oCart);
            db.SaveChanges();
            return oCart;
        }
        public List<Cart>? DeleteAll(long id)
        {
            using var db = new MavickDBContext();
            var user = db.Users.Find(id);
            if (user == null) return null;
            var oCart = db.Carts.Where(d => d.IdUser == id).ToList();
            db.Carts.RemoveRange(oCart);
            db.SaveChanges();
            return oCart;
        }
    }
}
