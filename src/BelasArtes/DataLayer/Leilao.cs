namespace DataLayer;
public class Leilao {
        public int? Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float PrecoInicial { get; set; }
        public string? EmailComprador { get; set; }
        public int CodPintura { get; set; }
        public int CodEstado { get; set; }
        public List<int> CodLicitacoes { get; set; } = new List<int>();
}