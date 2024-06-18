using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Data;

namespace TOLEAGRI.Model.Domain
{
    public class RegistroPeca
    {
        public int Quantidade { get; set; }
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Observacao { get; set; }
        public string NotaOuPedido { get; set; }
        public bool EntradaOuSaida { get; set; }
        public string CodigoSistema { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Locacao { get; set; }
        public string Acao { get; set; }
        public string Usuario { get; set; }

    }
}
