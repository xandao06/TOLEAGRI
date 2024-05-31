using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Collections.Generic;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;


namespace TOLEAGRI.Model.Services
{
    public class RegistroService
    {
        private readonly TOLEDbContext dbContext;

        public RegistroService(TOLEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Adicionar Peca 
        public Registro Add(Registro registro)
        {
            dbContext.Add(registro);
            dbContext.SaveChanges();
            return registro;
        }

        // Buscar Peca pelo Id
        public Registro Get(int id)
        {
            return dbContext.Set<Registro>().Find(id);
        }

        // Buscar uma lista de Pecas
        public IReadOnlyList<Registro> GetAll()
        {
            return dbContext.Set<Registro>().ToList();
        }

        // Atualizar uma Peca
        public void Update(Registro entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        // Deletar uma Peca
        public void Delete(int id)
        {
            Registro registro = Get(id);
            dbContext.Set<Registro>().Remove(registro);
            dbContext.SaveChanges();
        }

        // Cria registros para cada criação e atualização das Pecas
        private void SaveEntityRegistro()
        {
            var modifiedEntities = dbContext.ChangeTracker.Entries<Peca>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity).ToList();

            var registroList = new List<Registro>();

            foreach (var entity in modifiedEntities)
            {

                var registro = new Registro
                {
                    Id = entity.Id,
                    CodigoSistema = entity.CodigoSistema,
                    Locacao = entity.Locacao,
                    Marca = entity.Marca,
                    Modelo = entity.Modelo,
                    Quantidade = entity.Quantidade,
                    NotaOuPedido = entity.NotaOuPedido,
                    Observacao = entity.Observacao,
                    Data = DateTime.Now // Data da operação atual
                };

                registroList.Add(registro);
            }

            dbContext.Set<Registro>().AddRange(registroList);
            dbContext.SaveChanges();
        }

        // Lista os registros criados
        public IReadOnlyList<Registro> RegistroList()
        {
            return dbContext.Set<Registro>()
                            .Where(p => p.Data != null)
                            .OrderByDescending(p => p.Data)
                            .ToList()
                            .AsReadOnly();
        }
    }
}