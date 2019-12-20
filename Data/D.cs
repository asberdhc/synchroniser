using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class D
    {
        DataProductsEntities db = new DataProductsEntities();

        public void Insert(Common.Prod p)
        {
            Console.WriteLine("8 insert");
            db.Products.Add(new Product() {
                IdType = p.IdType,
                IdColor = p.IdColor,
                IdBrand = p.IdBrand,
                IdProvider = p.IdProvider,
                IdCatalog = p.IdCatalog,
                Title = p.Title,
                Nombre = p.Nombre,
                Description = p.Description,
                Observations = p.Observations,
                PriceDistributor = p.PriceDistributor,
                PriceClient = p.PriceClient,
                PriceMember = p.PriceMember,
                IsEnabled = p.IsEnabled,
                Keywords = p.Keywords,
                DateUpdate = p.DateUpdate
            });
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Products.Remove(
                db.Products.FirstOrDefault(p => p.Id == id)
            );
            db.SaveChanges();
        }

        public void Update(Common.Prod p)
        {
            if (p.Id.HasValue)
            {
                var r = db.Products.FirstOrDefault(x => x.Id == p.Id);
                if (r != null)
                {
                    r.IdType = p.IdType;
                    r.IdColor = p.IdColor;
                    r.IdBrand = p.IdBrand;
                    r.IdProvider = p.IdProvider;
                    r.IdCatalog = p.IdCatalog;
                    r.Title = p.Title;
                    r.Nombre = p.Nombre;
                    r.Description = p.Description;
                    r.Observations = p.Observations;
                    r.PriceDistributor = p.PriceDistributor;
                    r.PriceClient = p.PriceClient;
                    r.PriceMember = p.PriceMember;
                    r.IsEnabled = p.IsEnabled;
                    r.Keywords = p.Keywords;
                    r.DateUpdate = p.DateUpdate;
                    db.SaveChanges();
                }
            }
            else
            {
                Insert(p);
            }
        }

        public ChangeOnProduct GetLastRecordOn(string tableName)
        {
            if (tableName == Common.Strings.ChangesOnProductTableName)
            {
                int id = db.ChangesOnProducts.OrderByDescending(p => p.IdLog).Select(p => p.IdLog).First();
                ChangesOnProduct last = db.ChangesOnProducts.Where(p => p.IdLog == id).FirstOrDefault();
                return new ChangeOnProduct() {
                    IdLog = last.IdLog,
                    IdProduct = last.IdProduct,
                    ActionMade = last.ActionMade
                };
            }
            return new ChangeOnProduct();
        }

        public List<ChangeOnProduct> GetAllRecordsFrom(string tableName, int id)
        {
            if (tableName == Common.Strings.ChangesOnProductTableName)
            {
                List<ChangeOnProduct> a = new List<ChangeOnProduct>();
                var list = (from o in db.ChangesOnProducts where o.IdLog >= id select o).ToList();
                foreach (var product in list)
                {
                    a.Add(new ChangeOnProduct
                    {
                        IdLog = product.IdLog,
                        IdProduct = product.IdProduct,
                        ActionMade = product.ActionMade
                    });
                }
                return a;
            }
            return new List<ChangeOnProduct>();
        }

        public Prod GetProductByID(int id)
        {
            var product = (from o in db.Products where o.Id == id select o).ToList();
            return new Prod()
            {
                Id = product[0].Id,
                IdType = product[0].IdType,
                IdBrand = product[0].IdBrand,
                IdCatalog = product[0].IdCatalog,
                IdColor = product[0].IdColor,
                IdProvider = product[0].IdProvider,
                Nombre = product[0].Nombre,
                Title = product[0].Title,
                Description = product[0].Description,
                Observations = product[0].Observations,
                PriceDistributor = product[0].PriceDistributor.GetValueOrDefault(),
                PriceClient = product[0].PriceClient,
                PriceMember = product[0].PriceMember,
                IsEnabled = product[0].IsEnabled,
                Keywords = product[0].Keywords,
                DateUpdate = product[0].DateUpdate
            };
        }
    }
}
