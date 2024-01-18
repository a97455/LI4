using Datalayer;

namespace DataLayer{
	public class BelasArtes : IBelasArtes  {
		public ILeilaoDAO leiloes;
		public IUtilizadorDAO utilizadores;
		public IPinturaDAO pinturas;
		public ILicitacaoDAO licitacao;
		public ISqlDataAccess _db;

		public BelasArtes(ISqlDataAccess db)
		{
			leiloes= new LeilaoDAO(db);
			utilizadores= new UtilizadorDAO(db);
			pinturas= new PinturaDAO(db);
			licitacao= new LicitacaoDAO(db);
			_db = db;
		}
	
	
		public bool AutenticarUtilizador(ref string email, ref string palavraPasse){
			throw new NotImplementedException("Not implemented");
		}
		
		public List<Leilao> FiltrarArtista(ref string artista) {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> GetHistoricoCompras(ref string email) {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> GetHistoricoVendas(ref string email) {
			throw new NotImplementedException("Not implemented");
		}
		public bool LicitarPintura(ref int codUtilizador, ref float valor) {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLeiloesDecorrer() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLeiloesTerminados() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLicitacoes() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroUtilizadores() {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> OrdenarLeiloesDecorrer(ref int codModoOrdenacao) {
			throw new NotImplementedException("Not implemented");
		}
		public bool RegistarLeilao(ref int codPintura, ref DateTime dataInicio, ref DateTime dataFim, ref float precoInicial) {
			throw new NotImplementedException("Not implemented");
		}
		public bool RegistarPintura(ref string nome, ref float altura, ref float largura, ref float peso, 
		ref string descricao, ref string artista, ref bool autenticidade, ref int anoCriacao, ref int codMovimentoArtistico) {
			throw new NotImplementedException("Not implemented");
		}
		public bool RegistarUtilizador(ref string nome, ref string rua, ref string codigoPostal, ref string cidade, 
		ref string localidade, ref string paisResidencia, ref string numeroIdentificacaoGovernamental, ref string email, 
		ref int numeroTelemovel, ref string iBAN, ref string palavraPasse) {
			Utilizador utilizador = new Utilizador(nome,numeroTelemovel,rua,localidade,cidade,codigoPostal,
			paisResidencia,iBAN,palavraPasse);
			return utilizadores.PutUtilizador(utilizador).Result;
		}
	}
}