namespace DataLayer;

public class Licitacao {
        public int? Id { get; set; }
        public float Valor { get; set; }
        public string? EmailLicitador { get; set; } 

        public Licitacao(int? id, float valor, string? emailLicitador){
                Id = id;
                Valor = valor;
                EmailLicitador = emailLicitador;
        }
}
