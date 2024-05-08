using Microsoft.EntityFrameworkCore;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;


namespace TOLEAGRI.Model.Services
{
    public class EstoqueService
    {
        private readonly TOLEDbContext dbContext;

        public EstoqueService(TOLEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Peca Add(Peca entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public Peca Get(int id)
        {
            return dbContext.Set<Peca>().Find(id);
        }

        public IReadOnlyList<Peca> GetAll()
        {
            return dbContext.Set<Peca>().ToList();
        }

        public void Update(Peca entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Peca peca = Get(id);
            dbContext.Set<Peca>().Remove(peca);
            dbContext.SaveChanges();
        }

        public Peca BuscarOuCriar(string codigoSistema)
        {
            Peca peca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);

            if (peca == null)
            {
                // Se o estoque não existir, crie um novo
                peca = new Peca { CodigoSistema = codigoSistema };
                dbContext.Pecas.Add(peca);
                dbContext.SaveChanges();
            }
            return peca;
        }
    }
}
