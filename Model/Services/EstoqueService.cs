using TOLEAGRI.Model.Domain;

namespace TOLEAGRI.Model.Services
{
    public class EstoqueService
    {
        private readonly TOLEAGRIDbContext dbContext;

        public EstoqueService(TOLEAGRIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Estoque Add(Estoque entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public Estoque Get(int id)
        {
            return dbContext.Set<Estoque>().Find(id);
        }
        
        public IReadOnlyList<Estoque> GetAll()
        {
            return dbContext.Set<Estoque>().ToList();
        }

        public void Update(Estoque entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Estoque estoque = Get(id);
            dbContext.Set<Estoque>().Remove(estoque);
            dbContext.SaveChanges();
        }
    }
}
