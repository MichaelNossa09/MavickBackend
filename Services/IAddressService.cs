using MavickBackend.Models;
using MavickBackend.Models.Request;

namespace MavickBackend.Services
{
    public interface IAddressService
    {
        public Address? GetAddress(long id);
        public Address? AddAddress(AddressRequest address);
        public Address? EditAddress(AddressRequest address);
        public Address? DeleteAddress(long id);
    }
}
