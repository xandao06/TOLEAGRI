using System.ComponentModel.DataAnnotations;
using System.Data;
    
    
    namespace TOLEAGRI.Model.Domain
{
    public class Estoque
    {
        public int Quantidade { get; set; }
        public int EstoqueId { get; set; }
        public string Observacao { get; set; }
        public string CodigoSistema { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Locacao { get; set; }
        public int QuantidadeEntrada { get; set; }
        public int QuantidadeSaida {get; set; }
        public string DataEntrada { get; set; }
        public string DataSaida { get; set; }

    }
}
