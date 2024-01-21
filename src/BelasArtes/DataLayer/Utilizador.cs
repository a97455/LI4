namespace Datalayer;
public class Utilizador{
        public string Email { get; set; } = "";
        public int Telefone { get; set; }
        public string Rua { get; set; } = "";
        public string Localidade { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string CodigoPostal { get; set; } = "";
        public string PaisResidencia { get; set; } = "";
        public string IBAN { get; set; } = "";
        public string PalavraPasse { get; set; } = "";
        public List<int> CodPinturas { get; set; } = new List<int>();
}