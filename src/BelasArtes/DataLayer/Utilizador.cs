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

        public Utilizador(string email, int telefone, string rua, string localidade, 
                string cidade, string codigoPostal, string paisResidencia,
                string iban, string palavraPasse){
                Email = email;
                Telefone = telefone;
                Rua = rua;
                Localidade = localidade;
                Cidade = cidade;
                CodigoPostal = codigoPostal;
                PaisResidencia = paisResidencia;
                IBAN = iban;
                PalavraPasse = palavraPasse;
                CodPinturas = new List<int>();
        }
}