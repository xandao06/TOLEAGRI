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

        public Peca Add(Peca peca)
        {
            dbContext.Add(peca);
            dbContext.SaveChanges();
            return peca;
        }

        public Peca Get(int pecaId)
        {
            return dbContext.Set<Peca>().Find(pecaId);
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

        public void Delete(int pecaId)
        {
            Peca peca = Get(pecaId);
            dbContext.Set<Peca>().Remove(peca);
            dbContext.SaveChanges();
        }

        public void BuscarOuCriar(Peca peca)
        {
            Peca existingPeca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == peca.CodigoSistema);

            if (existingPeca == null)
            {
                dbContext.Pecas.Add(peca);
            }
            else
            {
                existingPeca.Locacao = peca.Locacao;
                existingPeca.Marca = peca.Marca;
                existingPeca.Modelo = peca.Modelo;
                existingPeca.QuantidadeEntrada = peca.QuantidadeEntrada;
                existingPeca.Observacao = peca.Observacao;

                dbContext.Pecas.Update(existingPeca);
            }

            dbContext.SaveChanges();
        }
    }
}



//public Peca BuscarOuCriar(string codigoSistema)
//{
//    Peca peca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);

//    if (peca == null)
//    {
//        // Se o estoque não existir, crie um novo
//        peca = new Peca { CodigoSistema = codigoSistema };
//        dbContext.Pecas.Add(peca);
//        dbContext.SaveChanges();
//    }
//    return peca;
//}