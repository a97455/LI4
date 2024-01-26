namespace DataLayer;
public class Leilao {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int PrecoInicial { get; set; }
        public string? EmailComprador { get; set; }
        public int? CodPintura { get; set; }
        public int? CodEstado { get; set; }
        public List<int> CodLicitacoes { get; set; } = new List<int>();

        public Leilao(){}

        public Leilao(int id, DateTime dataInicio, DateTime dataFim, int precoInicial,
        string? emailComprador, int? codPintura, int? codEstado){
            Id = id;
            DataInicio = dataInicio;
            DataFim = dataFim;
            PrecoInicial = precoInicial;
            EmailComprador = emailComprador;
            CodPintura = codPintura;
            CodEstado = codEstado;
        }
}