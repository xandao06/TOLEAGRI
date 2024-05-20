using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Collections.Generic;
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
                existingPeca.Quantidade = peca.Quantidade;
                existingPeca.Observacao = peca.Observacao;

                dbContext.Pecas.Update(existingPeca);
            }

            dbContext.SaveChanges();


        }

        public Peca GetByCodigoSistema(string codigoSistema)
        {
            return dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);
        }

        //        public IActionResult HistoricoList(List<Peca> pecaList)
        //        {
        //            var modifiedEntities = dbContext.ChangeTracker.Entries()
        //             .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
        //             .Select(e => e.Entity)
        //             .ToList();

        //            foreach (Peca peca in pecaList)
        //            {
        //                // Crie um objeto de histórico baseado na entidade
        //                var historico = new Peca
        //                {
        //                    Id = peca.Id,
        //                    CodigoSistema = peca.CodigoSistema,
        //                    Locacao = peca.Locacao,
        //                    Marca = peca.Marca,
        //                    Modelo = peca.Modelo,
        //                    QuantidadeEntrada = peca.QuantidadeEntrada,
        //                    QuantidadeSaida = peca.QuantidadeSaida,
        //                    Observacao = peca.Observacao,
        //                    DataEntrada = DateTime.Now, // Data da operação atual
        //                    DataSaida = DateTime.Now
        //                };

        //                pecaList.Add(historico);
        //            }

        //        }
        //    }
        //}





        //public IReadOnlyList<Peca> HistoricoList()
        //{
        //    // Obtenha as entidades que foram adicionadas ou modificadas
        //    var modifiedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

        //    // Crie uma lista para armazenar os históricos
        //    var historicoList = new List<Peca>();

        //    foreach (var entityEntry in modifiedEntities)
        //    {
        //        var entity = entityEntry.Entity;

        //        // Crie um objeto de histórico baseado na entidade
        //        var historico = new Peca
        //        {
        //            Id = entity.Id,
        //            CodigoSistema = entity.CodigoSistema,
        //            Locacao = entity.Locacao,
        //            Marca = entity.Marca,
        //            Modelo = entity.Modelo,
        //            QuantidadeEntrada = entity.QuantidadeEntrada,
        //            QuantidadeSaida = entity.QuantidadeSaida,
        //            Observacao = entity.Observacao,
        //            DataEntrada = DateTime.Now, // Data da operação atual
        //            DataSaida = DateTime.Now
        //        };

        //        // Adicione o histórico à lista
        //        historicoList.Add(historico);
        //    }

        //    // Retorne a lista como uma lista somente leitura
        //    return historicoList.AsReadOnly();
        //}


        //public void AddEntityHistory<T>(T entity) where T : class, IEntityHistory
        //{
        //     Lógica para adicionar o histórico da entidade
        //    var historico = new Peca
        //    {
        //        Id = entity.Id,
        //        CodigoSistema = entity.CodigoSistema,
        //        Locacao = entity.Locacao,
        //        Marca = entity.Marca,
        //        Modelo = entity.Modelo,
        //        QuantidadeEntrada = entity.QuantidadeEntrada,
        //        Observacao = entity.Observacao,
        //        DataEntrada = DateTime.Now // Ou outro campo que marque a data da operação
        //    };

        //    this.Set<Historico>().Add(historico);
        //}
        //protected void AddHistorico()
        //{
        //    var modifiedEntities = ChangeTracker.Entries<IEntityHistory>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        //    foreach (var entityEntry in modifiedEntities)
        //    {
        //        var entity = entityEntry.Entity;
        //        var addEntityHistoryMethod = this.GetType().GetMethod(nameof(Add)).MakeGenericMethod(entity.GetType());
        //        addEntityHistoryMethod.Invoke(this, new object[] { entity });
        //    }
        //}




        //public Peca BuscarOuCriar(string codigoSistema)
        //{
        //    Peca peca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);

        //    if (peca == null)
        //    {
        //         Se o estoque não existir, crie um novo
        //        peca = new Peca { CodigoSistema = codigoSistema };
        //        dbContext.Pecas.Add(peca);
        //        dbContext.SaveChanges();
        //    }
        //    return peca;
        //}

    }
}