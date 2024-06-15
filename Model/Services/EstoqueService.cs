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

        // Busca uma Peca pelo Codigo do Sistema, se existir no banco vai modificar a Peca, se não existir vai criar uma
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
                Usuario = peca.Usuario,
                Saida = peca.Saida,
                Entrada = peca.Entrada,
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

        // Faz a filtragem de pecas na barra de pesquisa    
        public IReadOnlyList<Peca> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return dbContext.Set<Peca>().ToList();
            }

            query = query.ToLower();

            return dbContext.Set<Peca>()
                .Where(p => p.CodigoSistema.ToLower().Contains(query)
                         || p.Locacao.ToLower().Contains(query)
                         || p.Marca.ToLower().Contains(query)
                         || p.Modelo.ToLower().Contains(query)
                         || p.Observacao.ToLower().Contains(query)
                         || p.Usuario.ToLower().Contains(query)
                         || p.Entrada.ToLower().Contains(query)
                         || p.Saida.ToLower().Contains(query))
                .ToList();
        }

    }
}
