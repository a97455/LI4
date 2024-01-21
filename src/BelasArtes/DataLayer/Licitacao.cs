namespace DataLayer;

public class Licitacao {
        public int? Id { get; set; }
        public double? Valor { get; set; }
        public string? EmailLicitador { get; set; }

        public Licitacao(int? id, double? valor, string? emailLicitador){
            Id = id;
            Valor = valor;
            EmailLicitador = emailLicitador;
        }
}