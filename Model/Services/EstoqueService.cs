using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
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
            string acao;

            if (existingPeca != null)
            {
                // Atualiza a peça existente
                existingPeca.Locacao = peca.Locacao;
                existingPeca.Marca = peca.Marca;
                existingPeca.Modelo = peca.Modelo;
                existingPeca.Quantidade = peca.Quantidade;
                existingPeca.NotaOuPedido = peca.NotaOuPedido;
                existingPeca.Observacao = peca.Observacao;

                dbContext.Pecas.Update(existingPeca);
                acao = "Modificado";
            }
            else
            {
                // Adiciona a nova peça
                dbContext.Pecas.Add(peca);
                acao = "Adicionado";
            }

            dbContext.SaveChanges();

            var registro = new RegistroPeca
            {
                CodigoSistema = peca.CodigoSistema,
                Locacao = peca.Locacao,
                Marca = peca.Marca,
                Modelo = peca.Modelo,
                Quantidade = peca.Quantidade,
                NotaOuPedido = peca.NotaOuPedido,
                Observacao = peca.Observacao,
                Data = DateTime.Now, // Data da operação atual
                Acao = acao // Ação realizada
            };

            dbContext.Set<RegistroPeca>().Add(registro);
            dbContext.SaveChanges();

        }

        // Cria a string que retorna o Codigo do Sistema das Pecas gravado no banco
        public Peca GetByCodigoSistema(string codigoSistema)
        {
            return dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);
        }

        //public async Task<IActionResult> Index(string searchString)
        //{
        //    if (dbContext.Pecas == null)
        //    {
        //       throw new ArgumentOutOfRangeException("Entity set 'MvcMovieContext.Movie'  is null.");
        //    }

        //    var pecas = from p in dbContext.Pecas
        //                select p;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        pecas = pecas.Where(s => s.CodigoSistema!.Contains(searchString));
        //    }

        //    return 
        //}
    }
}

//    if (!string.IsNullOrEmpty(searchString))
//    {
//        pecas = pecas.Where(c => c.CodigoSistema.Contains(searchString));
//        return View(pecas);
//    }
//    return View(await dbContext.Include(c => c.CodigoSistema).ToList());