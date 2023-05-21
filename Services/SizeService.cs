using MavickBackend.Models;

namespace MavickBackend.Services
{
    public class SizeService : ISizeService
    {
        public SizeService()
        {
        }
        public List<Size> Get()
        {
            using var db = new MavickDBContext();
            var lst = db.Sizes.ToList();

            return lst;
        }
    }
}
