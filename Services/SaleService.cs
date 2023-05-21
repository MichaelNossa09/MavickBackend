using MavickBackend.Models;
using MavickBackend.Models.Request;

namespace MavickBackend.Services
{
    public class SaleService : ISaleService
    {
        public void Add(SaleRequest model)
        {
            using MavickDBContext db = new();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                var sale = new Sale
                {
                    Total = model.Concepts.Sum(c => c.Amount * c.UnitPrice),
                    Date = DateTime.Now,
                    IdUser = model.IdUser
                };
                db.Sales.Add(sale);
                db.SaveChanges();

                foreach (var modelConcept in model.Concepts)
                {
                    var concept = new Models.Concept
                    {
                        Amount = modelConcept.Amount,
                        IdProduct = modelConcept.IdProduct,
                        Import = modelConcept.Import,
                        UnitPrice = modelConcept.UnitPrice,
                        IdSale = sale.Id
                    };
                    db.Concepts.Add(concept);
                    db.SaveChanges();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("No se pudo insertar");
            }
        }
    }
}
