﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;


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

        // Modificar uma Peca
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

        // Deletar todas as peças
        public void DeleteAll()
        {
            var pecaList = dbContext.Set<Peca>().ToList();
            dbContext.Set<Peca>().RemoveRange(pecaList);
            dbContext.SaveChanges();
        }

        // Busca uma Peca pelo Codigo do Sistema, se existir no banco vai modificar a Peca, se não existir vai criar uma nova
        public void BuscarModificarCriar(Peca peca)
        {
            Peca existingPeca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == peca.CodigoSistema);

            if (existingPeca != null)
            {
                existingPeca.Quantidade += peca.Quantidade;

                // Atualiza a peça existente
                existingPeca.Locacao = peca.Locacao;
                existingPeca.Marca = peca.Marca;
                existingPeca.Modelo = peca.Modelo;
                existingPeca.NotaOuPedido = peca.NotaOuPedido;
                existingPeca.Observacao = peca.Observacao;
                existingPeca.Usuario = peca.Usuario;


                dbContext.Pecas.Update(existingPeca);
            }
            else
            {
                dbContext.Pecas.Add(peca);
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
                Usuario = peca.Usuario,
                Data = DateTime.Now, // Data da operação atual
                EntradaOuSaida = peca.EntradaOuSaida,
            };

            dbContext.Set<RegistroPeca>().Add(registro);
            dbContext.SaveChanges();

        }


        // Buscar peça pelo codigo e aplicar a saida do estoque
        public void BuscarModificarSaida(Peca peca)
        {

            Peca existingPeca = dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == peca.CodigoSistema);

            if (existingPeca != null)
            {

                existingPeca.Quantidade -= peca.Quantidade;

                // Atualiza a peça existente
                existingPeca.Locacao = peca.Locacao;
                existingPeca.Marca = peca.Marca;
                existingPeca.Modelo = peca.Modelo;
                existingPeca.NotaOuPedido = peca.NotaOuPedido;
                existingPeca.Observacao = peca.Observacao;
                existingPeca.Usuario = peca.Usuario;

                dbContext.Pecas.Update(existingPeca);
                dbContext.SaveChanges();

            }

            var registro = new RegistroPeca
            {
                CodigoSistema = peca.CodigoSistema,
                Locacao = peca.Locacao,
                Marca = peca.Marca,
                Modelo = peca.Modelo,
                Quantidade = peca.Quantidade,
                NotaOuPedido = peca.NotaOuPedido,
                Observacao = peca.Observacao,
                Usuario = peca.Usuario,
                Data = DateTime.Now, // Data da operação atual
                EntradaOuSaida = peca.EntradaOuSaida,
            };

            dbContext.Set<RegistroPeca>().Add(registro);
            dbContext.SaveChanges();
        }


    // Cria a string que retorna o Codigo do Sistema das Pecas gravado no banco
    public Peca GetByCodigoSistema(string codigoSistema)
        {
            return dbContext.Pecas.FirstOrDefault(e => e.CodigoSistema == codigoSistema);
        }

        // Faz a filtragem de pecas na barra de pesquisa e por data
        public IReadOnlyList<Peca> SearchPecas(string query, DateTime? startDate, DateTime? endDate)
        {
            var pecas = dbContext.Set<Peca>().AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                pecas = pecas.Where(p => (p.CodigoSistema ?? "").ToLower().Contains(query)
                                                || (p.Locacao ?? "").ToLower().Contains(query)
                                                || (p.Marca ?? "").ToLower().Contains(query)
                                                || (p.Modelo ?? "").ToLower().Contains(query)
                                                || (p.NotaOuPedido ?? "").ToLower().Contains(query)
                                                || (p.Observacao ?? "").ToLower().Contains(query)
                                                || (p.Usuario ?? "").ToLower().Contains(query));
            }

            if (startDate.HasValue)
            {
                pecas = pecas.Where(p => p.Data >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                pecas = pecas.Where(p => p.Data <= endDate.Value);
            }

            return pecas.ToList();

        }

        // RELATORIO
        public IReadOnlyList<Peca> GetReportData(string query, DateTime? startDate, DateTime? endDate)
        {
            var pecas = dbContext.Set<Peca>().AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                pecas = pecas.Where(c => (c.CodigoSistema ?? "").ToLower().Contains(query)
                                                || (c.Marca ?? "").ToLower().Contains(query)
                                                || (c.Modelo ?? "").ToLower().Contains(query));
            }

            if (startDate.HasValue)
            {
                pecas = pecas.Where(c => c.Data >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                pecas = pecas.Where(c => c.Data <= endDate.Value);
            }

            return pecas.ToList();
        }


    }
}
