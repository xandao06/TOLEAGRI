using System.ComponentModel.DataAnnotations;
using System.Data;
    
    
    namespace TOLEAGRI.Model.Domain
{
    public class Estoque
    {
        public int Quantidade { get; set; }
        public int EstoqueId { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string PosicaoEstoque { get; set; }
        public int QuantidadeSaida {get; set; }
        public string Conferencia { get; set; }
        public string DataEnvio { get; set; }

    }
}
