namespace DataLayer;
public class Leilao {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float PrecoInicial { get; set; }
        public string EmailComprador { get; set; } = ""; // Default value of an empty string
        public int CodPintura { get; set; }
        public int CodEstado { get; set; }
}
