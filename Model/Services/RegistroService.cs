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

        public void DeleteAll()
        {
            var registroList = dbContext.Set<RegistroPeca>().ToList();
            dbContext.Set<RegistroPeca>().RemoveRange(registroList);
            dbContext.SaveChanges();
        }

        public IReadOnlyList<RegistroPeca> SearchRegistros(string query, DateTime? startDate, DateTime? endDate)
        {
            var registros = dbContext.Set<RegistroPeca>().AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                registros = registros.Where(r => (r.CodigoSistema ?? "").ToLower().Contains(query)
                                                || (r.Locacao ?? "").ToLower().Contains(query)
                                                || (r.Marca ?? "").ToLower().Contains(query)
                                                || (r.Modelo ?? "").ToLower().Contains(query)
                                                || (r.NotaOuPedido ?? "").ToLower().Contains(query)
                                                || (r.Observacao ?? "").ToLower().Contains(query)
                                                || (r.Usuario ?? "").ToLower().Contains(query)
                                                || (r.EntradaOuSaida ?? "").ToLower().Contains(query));
            }

            if (startDate.HasValue)
            {
                registros = registros.Where(r => r.Data >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                registros = registros.Where(r => r.Data <= endDate.Value);
            }

            return registros.ToList();
        }

    }
}
