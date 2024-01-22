namespace DataLayer;
public class Leilao {
        public int? Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double? PrecoInicial { get; set; }
        public string? EmailComprador { get; set; }
        public int? CodPintura { get; set; }
        public int? CodEstado { get; set; }
        public List<int> CodLicitacoes { get; set; } = new List<int>();

<<<<<<< HEAD
        public Leilao(){}

        public Leilao(int? id, DateTime dataInicio, DateTime dataFim, float? precoInicial,
=======
        public Leilao(int? id, DateTime dataInicio, DateTime dataFim, double? precoInicial,
>>>>>>> 2aabcb3a52e75f550be16a2edd1ce9eec976b3c7
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