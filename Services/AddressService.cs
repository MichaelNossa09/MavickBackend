using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;

namespace MavickBackend.Services
{
    public class AddressService : IAddressService
    {

        public Address? GetAddress(long id)
        {
            using var db = new MavickDBContext();
            var oAddress = db.Addresses.Find(id);
            if (oAddress == null) return null;
            return oAddress;
        }

        public Address? AddAddress(AddressRequest oModel)
        {
            var db = new MavickDBContext();
            try
            {
                var us = db.Users.Find(oModel.IdUser);
                if (us == null) throw new Exception($"UsuarioID {oModel.IdUser} Inválido");

                Address oAddress = new()
                {
                    State = oModel.State,
                    City = oModel.City,
                    Country = oModel.Country,
                    Direction = oModel.Direction,
                    Postalcode = oModel.Postalcode,
                    IdUser = oModel.IdUser
                };
                var aux = db.Addresses.Where(d=> d.City == oAddress.City && d.Direction == oAddress.Direction && d.IdUser == oAddress.IdUser);
                if (aux == null) {
                    db.Addresses.Add(oAddress);
                    db.SaveChanges();
                    UserAddress oUser = new()
                    {
                        IdUser = oAddress.IdUser,
                        IdAddress = oAddress.Id
                    };
                    db.UserAddresses.Add(oUser);
                    db.SaveChanges();
                    return oAddress;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Address? EditAddress(AddressRequest oModel)
        {
            using var db = new MavickDBContext();
            var oAddress = db.Addresses.Find(oModel.Id);
            if (oAddress == null) return null;
            oAddress.Direction = oModel.Direction;
            oAddress.City = oModel.City;
            oAddress.State = oModel.State;
            oAddress.Country = oModel.Country;
            oAddress.Postalcode = oModel.Postalcode;
            db.Entry(oAddress).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return oAddress;
        }
        public Address? DeleteAddress(long id)
        {
            var db = new MavickDBContext();
            try
            {
                var oAddress = db.Addresses.Find(id);
                if (oAddress == null) return null;
                var oUserAdd = db.UserAddresses.Where(d => d.IdAddress == id).FirstOrDefault();
                if (oUserAdd == null) throw new Exception();
                db.UserAddresses.Remove(oUserAdd);
                db.Addresses.Remove(oAddress);
                db.SaveChanges();
                return oAddress;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
