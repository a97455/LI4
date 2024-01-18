namespace DataLayer;

public class Licitacao {
        public int? Id { get; set; }
        public int Valor { get; set; }
        public string? EmailLicitador { get; set; } 

        public Licitacao(int? id, int valor, string? emailLicitador){
                Id = id;
                Valor = valor;
                EmailLicitador = emailLicitador;
        }
}
