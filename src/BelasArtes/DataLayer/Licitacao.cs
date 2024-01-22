using System.Data;

namespace DataLayer;

public class Licitacao {
        public int? Id { get; set; }
        public double? Valor { get; set; }
        public string? EmailLicitador { get; set; }
        public DateTime? Data { get; set; }

        public Licitacao(){}

        public Licitacao(int? id, double? valor, string? emailLicitador, DateTime? data){
            Id = id;
            Valor = valor;
            EmailLicitador = emailLicitador;
            Data=data;
        }
}