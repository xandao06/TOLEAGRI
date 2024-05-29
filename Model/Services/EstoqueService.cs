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

        // Adicionar Peca 
        public Peca Add(Peca peca)
        {
            dbContext.Add(peca);
            dbContext.SaveChanges();
            SaveEntityRegistro();
            return peca;
        }

        // Buscar Peca pelo Id
        public Peca Get(int id)
        {
            return dbContext.Set<Peca>().Find(id);
        }

        // Buscar uma lista de Pecas
        public IReadOnlyList<Peca> GetAll()
        {
            return dbContext.Set<Peca>().ToList();
        }

        // Atualizar uma Peca
        public void Update(Peca entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
            SaveEntityRegistro();
        }

        // Deletar uma Peca
        public void Delete(int id)
        {
            Peca peca = Get(id);
            dbContext.Set<Peca>().Remove(peca);
            dbContext.SaveChanges();
        }

        // Buscar uma Peca pelo Codigo do Sistema, se existir no banco vai modificar a Peca, se não existir vai criar uma
        public void BuscarModificarCriar(Peca peca)
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
                SaveEntityRegistro();
            }
            else
            {
                dbContext.Pecas.Add(peca);
                dbContext.SaveChanges();
                SaveEntityRegistro();
            }
        }

        // Cria a string que retorna o Codigo do Sistema das Pecas gravado no banco
        public Peca GetByCodigoSistema(string codigoSistema)
        {
            return dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);
        }
         
        // Cria registros para cada criação e atualização das Pecas
        private void SaveEntityRegistro()
        {
            var modifiedEntities = dbContext.ChangeTracker.Entries<Peca>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity).ToList();

            var registroList = new List<Peca>();

            foreach (var entity in modifiedEntities)
            {

                var registro = new Peca
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

                registroList.Add(registro);
            }

            dbContext.Set<Peca>().AddRange(registroList);
            dbContext.SaveChanges();
        }

        // Lista os registros criados
        public IReadOnlyList<Peca> RegistroList()
        {
            return dbContext.Set<Peca>()
                            .Where(p => p.Data != null)
                            .OrderByDescending(p => p.Data)
                            .ToList()
                            .AsReadOnly();
        }
    }
}