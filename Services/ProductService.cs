using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Web.Http.Results;

namespace MavickBackend.Services
{
    public class ProductService : IProductService
    {
        public Product? GetProductById(int id)
        {
            using var db = new MavickDBContext();
            var prod = db.Products.FirstOrDefault(x => x.Id == id);
            if (prod == null) return null;
            return prod;
            
        }
        public List<Product> GetAll()
        {
            using var db = new MavickDBContext();
            var lst = db.Products.ToList();
            return lst;
        }
        public Product Add(ProductRequest model)
        {
            using var db = new MavickDBContext();
            var transaction = db.Database.BeginTransaction();
            try
            {
                var product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    Amount = model.Amount,
                    Photo = model.Photo,
                    Cost = model.Cost
                };
                db.Products.Add(product);
                db.SaveChanges();
                foreach (var modelSize in model.Sizes)
                {
                    var size = new Models.SizeProduct
                    {
                        IdProduct = product.Id
                    };
                    var data = db.Sizes.SingleOrDefault(c => c.Id == modelSize.Id)?.Id;
                    if (data == null)
                    {
                        throw new Exception("Categoría inválida");
                    }
                    size.IdSize = (int)data;
                    db.SizeProducts.Add(size);
                }
                foreach (var modelCategory in model.Categories)
                {
                    var category = new Models.CategoryProduct
                    {
                        IdProduct = product.Id
                    };
                    var data = db.Categories.SingleOrDefault(c => c.Id == modelCategory.Id)?.Id;
                    if (data == null)
                    {
                        throw new Exception("Categoría inválida");
                    }
                    category.IdCategory = (int)data;
                    db.CategoryProducts.Add(category);
                }
                db.SaveChanges();
                if (db.ChangeTracker.HasChanges())
                {
                    db.SaveChanges();
                }
                transaction.Commit();
                return product;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Error");
            }
        }

        public Product? Update(ProductRequest oModel)
        {
            var db = new MavickDBContext();
            var transaction = db.Database.BeginTransaction();
            try
            {
                var oProduct = db.Products.Include(x => x.CategoryProducts)
                                    .Include(x => x.SizeProducts)
                                    .FirstOrDefault(x => x.Id == oModel.Id);
                if (oProduct == null)
                {
                    throw new Exception("Producto Inválido");
                }
                oProduct.Name = oModel.Name;
                oProduct.Photo = oModel.Photo;
                oProduct.Amount = oModel.Amount;
                oProduct.Cost = oModel.Cost;
                oProduct.UnitPrice = oModel.UnitPrice;
                oProduct.Description = oModel.Description;

                db.CategoryProducts.RemoveRange(oProduct.CategoryProducts);
                db.SizeProducts.RemoveRange(oProduct.SizeProducts);

                foreach (var modelCategory in oModel.Categories)
                {
                    var category = new Models.CategoryProduct
                    {
                        IdProduct = oModel.Id
                    };
                    var categ = db.Categories.SingleOrDefault(c => c.Id == modelCategory.Id)?.Id;
                    if (categ == null)
                    {
                        throw new Exception("Categoría inválida");
                    }
                    category.IdCategory = (int)categ;
                    db.CategoryProducts.Add(category);
                }
                foreach (var modelSize in oModel.Sizes)
                {
                    var size = new Models.SizeProduct
                    {
                        IdProduct = oModel.Id
                    };
                    var siz = db.Sizes.Find(modelSize.Id)?.Id;
                    if (siz == null)
                    {
                        throw new Exception("Size Inválida");
                    }
                    size.IdSize = (int)siz;
                    db.SizeProducts.Add(size);
                }
                db.Entry(oProduct).State = EntityState.Modified;
                db.SaveChanges();
                if (db.ChangeTracker.HasChanges())
                {
                    db.SaveChanges();
                }
                transaction.Commit();
                return oProduct;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Error");
            }
        }
        public List<Product>? GetProductWithCategory(string cate)
        {
            var db = new MavickDBContext();
            var aux = db.Categories.AsEnumerable().FirstOrDefault(c => c.Name.Equals(cate, StringComparison.CurrentCultureIgnoreCase));
            if (aux == null) return null;

            var productsForCategory = db.CategoryProducts.AsNoTracking().Where(c => c.IdCategory == aux.Id).Join(db.Products, cp => cp.IdProduct, p => p.Id, (cp, p) => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Amount = p.Amount,
                UnitPrice = p.UnitPrice,
                Description = p.Description,
                Photo = p.Photo
            }).ToList();

            return productsForCategory;
        }
        public List<Product>? GetProductWithSize(string size)
        {
            var db = new MavickDBContext();
            var aux = db.Sizes.AsEnumerable().FirstOrDefault(c => c.Name.Equals(size, StringComparison.CurrentCultureIgnoreCase));
            if (aux == null) return null;

            var productsForSize = db.SizeProducts.AsNoTracking().Where(c => c.IdSize == aux.Id).Join(db.Products, cp => cp.IdProduct, p => p.Id, (cp, p) => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Amount = p.Amount,
                UnitPrice = p.UnitPrice,
                Description = p.Description,
                Photo = p.Photo
            }).ToList();

            return productsForSize;
        }
        public List<Product>? GetProductWithCategoryAndSize(string cate, string size)
        {
            var db = new MavickDBContext();
            var aux = db.Categories.AsEnumerable().FirstOrDefault(c => c.Name.Equals(cate, StringComparison.OrdinalIgnoreCase));
            var aux2 = db.Sizes.AsEnumerable().FirstOrDefault(s => s.Name.Equals(size, StringComparison.OrdinalIgnoreCase));
            if (aux == null || aux2 == null) return null;
            var sql = @"
                        SELECT p.Id, p.Name, p.Unit_Price, p.Amount, p.Description, p.Photo, p.Cost
                        FROM Product p
                        JOIN CategoryProduct cp ON p.Id = cp.IdProduct
                        JOIN Category c ON cp.IdCategory = c.Id
                        JOIN SizeProduct sp ON p.Id = sp.IdProduct
                        JOIN Size s ON sp.IdSize = s.Id
                        WHERE c.Name = {0} AND s.Name = {1}";

            var productsForCategoryAndSize = db.Products.FromSqlRaw(sql, cate, size).ToList();
            return productsForCategoryAndSize;
        }
        public Product? DeleteProduct(int id)
        {
            using var db = new MavickDBContext();
            var oProduct = db.Products.Find(id);
            if (oProduct == null) return null;
            var x = db.CategoryProducts.Where(d=> d.IdProduct == oProduct.Id);
            db.CategoryProducts.RemoveRange(x);
            var y = db.SizeProducts.Where(d => d.IdProduct == oProduct.Id);
            db.SizeProducts.RemoveRange(y);
            db.Products.Remove(oProduct);
            db.SaveChanges();
            return oProduct;
        }
    }
}
