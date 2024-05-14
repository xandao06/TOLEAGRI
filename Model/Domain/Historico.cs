﻿namespace TOLEAGRI.Model.Domain
{
    public class Historico
    {
        public int Quantidade { get; set; }
        public int Id { get; set; }
        public string? Observacao { get; set; }
        public string? CodigoSistema { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Locacao { get; set; }
        public int QuantidadeEntrada { get; set; }
        public int QuantidadeSaida {get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public DateTime DataSaida { get; set; } = DateTime.Now;

    }
}
