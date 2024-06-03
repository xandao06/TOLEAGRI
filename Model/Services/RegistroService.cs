using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Collections.Generic;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model.Services;


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
        public RegistroPeca Add(RegistroPeca registro)
        {
            dbContext.Add(registro);
            dbContext.SaveChanges();
            return registro;
        }

        // Buscar Peca pelo Id
        public RegistroPeca Get(int id)
        {
            return dbContext.Set<RegistroPeca>().Find(id);
        }

        // Buscar uma lista de Pecas
        public IReadOnlyList<RegistroPeca> GetAll()
        {
            return dbContext.Set<RegistroPeca>().ToList();
        }

        // Atualizar uma Peca
        public void Update(RegistroPeca entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        // Deletar uma Peca
        public void Delete(int id)
        {
            RegistroPeca registro = Get(id);
            dbContext.Set<RegistroPeca>().Remove(registro);
            dbContext.SaveChanges();
        }

        // Lista os registros criados
        public IReadOnlyList<RegistroPeca> RegistroList()
        {
            return dbContext.Set<RegistroPeca>()
                            .Where(p => p.Data != null)
                            .OrderByDescending(p => p.Data)
                            .ToList()
                            .AsReadOnly();
        }
    }
}