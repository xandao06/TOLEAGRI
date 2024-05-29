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
            SaveEntityHistory();
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
            SaveEntityHistory();
        }

        public void Delete(int id)
        {
            Peca peca = Get(id);
            dbContext.Set<Peca>().Remove(peca);
            dbContext.SaveChanges();
        }

        public void BuscarModificar(Peca peca)
        {
            Peca existingPeca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == peca.CodigoSistema);

            if (existingPeca != null)
            {
                existingPeca.Locacao = peca.Locacao;
                existingPeca.Marca = peca.Marca;
                existingPeca.Modelo = peca.Modelo;
                existingPeca.Quantidade = peca.Quantidade;
                existingPeca.NotaOuPedido = peca.NotaOuPedido;
                existingPeca.Observacao = peca.Observacao;

                dbContext.Pecas.Update(existingPeca);
                dbContext.SaveChanges();
                SaveEntityHistory();
            }
        }

        public Peca GetByCodigoSistema(string codigoSistema)
        {
            return dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);
        }

        private void SaveEntityHistory()
        {
            var modifiedEntities = dbContext.ChangeTracker.Entries<Peca>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity).ToList();

            var historicoList = new List<Peca>();

            foreach (var entity in modifiedEntities)
            {

                var historico = new Peca
                {
                    Id = entity.Id,
                    CodigoSistema = entity.CodigoSistema,
                    Locacao = entity.Locacao,
                    Marca = entity.Marca,
                    Modelo = entity.Modelo,
                    Quantidade = entity.Quantidade,
                    Observacao = entity.Observacao,
                    Data = DateTime.Now // Data da operação atual
                };

                historicoList.Add(historico);
            }

            dbContext.Set<Peca>().AddRange(historicoList);
            dbContext.SaveChanges();
        }
        public IReadOnlyList<Peca> RegistrosList()
        {
            return dbContext.Set<Peca>()
                            .Where(p => p.Data != null)
                            .OrderByDescending(p => p.Data)
                            .ToList()
                            .AsReadOnly();
        }
    }
}