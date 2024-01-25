namespace Datalayer;
public class Utilizador{
        public string Email { get; set; } = "";
        public string Nome {get;set;} = "";
        public int? Telefone { get; set; }
        public string Rua { get; set; } = "";
        public string Localidade { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string CodigoPostal { get; set; } = "";
        public string PaisResidencia { get; set; } = "";
        public string NIG {get;set;} = "";
        public string IBAN { get; set; } = "";
        public string PalavraPasse { get; set; } = "";
        public List<int> CodPinturas { get; set; } = new List<int>();

        public Utilizador(){}

        public Utilizador(string email, string nome, int? telefone, string rua, string localidade,
        string cidade, string codigoPostal, string paisResidencia, string nig, string iban, string palavraPasse){
                Email = email;
                Nome = nome;
                Telefone = telefone;
                Rua = rua;
                Localidade = localidade;
                Cidade = cidade;
                CodigoPostal = codigoPostal;
                PaisResidencia = paisResidencia;
                NIG = nig;
                IBAN = iban;
                PalavraPasse = palavraPasse;
        }
}