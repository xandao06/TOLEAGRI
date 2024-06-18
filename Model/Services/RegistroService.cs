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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace TOLEAGRI.Model.Services
{
    public class RegistroService
    {
        private readonly TOLEDbContext dbContext;

        public RegistroService(TOLEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Adicionar Registro 
        public RegistroPeca Add(RegistroPeca registro)
        {
            dbContext.Add(registro);
            dbContext.SaveChanges();
            return registro;
        }

        // Buscar Registro pelo Id
        public RegistroPeca Get(int id)
        {
            return dbContext.Set<RegistroPeca>().Find(id);
        }

        // Buscar uma lista de Registros
        public IReadOnlyList<RegistroPeca> GetAll()
        {
            return dbContext.Set<RegistroPeca>().ToList();
        }

        // Atualizar um Registro
        public void Update(RegistroPeca entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        // Deletar um Registro
        public void Delete(int id)
        {
            RegistroPeca registroPeca = Get(id);
            dbContext.Set<RegistroPeca>().Remove(registroPeca);
            dbContext.SaveChanges();
        }

        // Lista os registros criados
        public IReadOnlyList<RegistroPeca> RegistroList()
        {
            return dbContext.Set<RegistroPeca>()
                            .Where(p => p.CodigoSistema != null)
                            .OrderByDescending(p => p.Data)
                            .ToList()
                            .AsReadOnly();
        }
        
        // Faz a filtragem de registros na barra de pesquisa
        public IReadOnlyList<RegistroPeca> SearchRegistros(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return dbContext.Set<RegistroPeca>().ToList();
            }

            query = query.ToLower();

            return dbContext.Set<RegistroPeca>()
                .Where(r => r.CodigoSistema.ToLower().Contains(query)

                         || r.Locacao.ToLower().Contains(query)
                         || r.Marca.ToLower().Contains(query)
                         || r.Modelo.ToLower().Contains(query)
                         || r.Observacao.ToLower().Contains(query)
                         || r.Acao.ToLower().Contains(query)
                         || r.Usuario.ToLower().Contains(query))
                .ToList();
        }

        public IReadOnlyList<RegistroPeca> GetRegistros()
        {
            return dbContext.Set<RegistroPeca>().ToList();
        }
    }
}
